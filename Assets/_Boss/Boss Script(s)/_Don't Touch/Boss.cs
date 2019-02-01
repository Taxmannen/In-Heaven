﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson, planned by: Filip Nilsson + Jesper Uddefors
/// </summary>
public class Boss : Character
{

    //Serialized

    [SerializeField] private Transform phaseParent;
    [SerializeField] private Transform movementParent;
    [SerializeField] private Transform attackParent;
    [SerializeField] private List<BossHitboxElement> bossHitboxElements;
    [SerializeField] private int activePhase = 0;
    [SerializeField] private List<BossPhase> phasePrefabs;



    //Private

    private List<BossPhase> phases = new List<BossPhase>();
    private Coroutine bossRoutine = null;



    //Main

    private void OnValidate()
    {

        foreach (BossHitboxElement bossHitboxElement in bossHitboxElements)
        {

            if (bossHitboxElement.GetWeakpoint())
            {

                if (bossHitboxElement.GetMaxHP() < 0)
                {
                    bossHitboxElement.SetMaxHP(0);
                }

                if (bossHitboxElement.GetActivePhase() < 1)
                {
                    bossHitboxElement.SetActivePhase(1);
                }

                else if (bossHitboxElement.GetActivePhase() > phases.Count)
                {
                    bossHitboxElement.SetActivePhase(phasePrefabs.Count);
                }

            }

            else
            {
                bossHitboxElement.SetMaxHP(0);
                bossHitboxElement.SetActivePhase(0);
            }

        }

        if (activePhase < 1)
        {
            activePhase = 1;
        }

        else if (activePhase > phasePrefabs.Count)
        {
            activePhase = phasePrefabs.Count;
        }

    }

    private new void Start()
    {

        SetupPhases();

        maxHP = 0;

        if (bossHitboxElements.Count > GetComponentsInChildren<BossHitbox>().Length)
        {
            Debug.LogError("[Custom Error] : Update \"BossHitboxElement(s)\"");
            return;
        }

        foreach (BossHitboxElement bossHitboxElement in bossHitboxElements)
        {

            bossHitboxElement.SetName(bossHitboxElement.GetHitbox().name);
            maxHP += bossHitboxElement.GetMaxHP();

            bossHitboxElement.GetHitbox().SetMaxHP(bossHitboxElement.GetMaxHP());
            bossHitboxElement.GetHitbox().SetHP(bossHitboxElement.GetHitbox().GetMaxHP());
            bossHitboxElement.GetHitbox().SetWeakpoint(bossHitboxElement.GetWeakpoint());

            if (bossHitboxElement.GetHitbox().GetWeakpoint())
            {
                bossHitboxElement.GetHitbox().SetActivePhase(bossHitboxElement.GetActivePhase() - 1);
            }

        }

        base.Start();

        StartBoss();

    }

    public void SetupPhases()
    {

        if (!phaseParent)
        {

            GameObject phaseObject;

            if (phaseObject = GameObject.FindWithTag("Phase Parent"))
            {

                if (phaseParent = phaseObject.transform)
                {

                }

            }
        }

        if (phaseParent)
        {

            if (phasePrefabs.Count > 0)
            {
                
                phases.Clear();

                foreach (BossPhase phasePrefab in phasePrefabs)
                {
                    phases.Add(Instantiate(phasePrefab, phaseParent));
                }

            }

            else
            {
                Debug.LogError("[Custom Error] : List of \"phasePrefabs\" for a boss is empty.");
            }

        }

        else
        {
            Debug.LogError("[Custom Error] : Couldn't find the \"phaseParent\" transform, check the \"phaseParent\" parameter on the boss script in the hierarchy or check the tag on the \"phaseParent\" GameObject.");
        }

    }

    private void StartBoss()
    {
        bossRoutine = StartCoroutine(BossRoutine());
    }

    //Unused method, Implement a way to stop the boss for future.
    private void StopBoss()
    {
        StopCoroutine(bossRoutine);
        bossRoutine = null;
    }

    private IEnumerator BossRoutine()
    {

        for (int i = (activePhase - 1); i < phases.Count; i++)
        {

            //Devlog
            Debug.Log("Started Phase: " + i);
            phases[i].StartPhase(this, movementParent, attackParent);

            yield return new WaitUntil(() => (activePhase - 1) != i);

            phases[i].StopPhase();

            if ((activePhase - 1) > i)
            {

                if (i + (activePhase - 1) - (i + 1) < (phases.Count - 1))
                {
                    i += (activePhase - 1) - (i + 1);
                }

                else
                {
                    i = phases.Count - 1;
                    //Implement Reset To Phase Health @ Last Phase ...
                }

            }

            else
            {
                i = (activePhase - 2);
                //Implement Reset To Phase Health @ Newly Set Phase ...
            }

        }

        BossDie();
        bossRoutine = null;
        yield break;

    }

    private void BossDie()
    {
        //Devlog
        Debug.Log("Boss died aka done with phases.");
        //Boss Dies Here, Animations & More... Tobe Coroutine
    }



    //Getters & Setters

    public void GetHitboxes()
    {

        for (int i = (bossHitboxElements.Count - 1); i >= GetComponentsInChildren<BossHitbox>().Length; i--)
        {
            bossHitboxElements.RemoveAt(i);
        }

        for (int i = 0; i < GetComponentsInChildren<BossHitbox>().Length; i++)
        {

            BossHitbox bhb = GetComponentsInChildren<BossHitbox>()[i];

            if (i > (bossHitboxElements.Count - 1))
            {
                bossHitboxElements.Add(new BossHitboxElement(bhb.name, bhb, bhb.GetWeakpoint(), bhb.GetMaxHP(), 0));
            }

            else
            {
                bossHitboxElements[i] = new BossHitboxElement(bhb.name, bhb, bossHitboxElements[i].GetWeakpoint(), bossHitboxElements[i].GetMaxHP(), 0);
            }

        }

        maxHP = 0;

        foreach (BossHitboxElement bossHitboxElement in bossHitboxElements)
        {

            bossHitboxElement.SetName(bossHitboxElement.GetHitbox().name);
            maxHP += bossHitboxElement.GetMaxHP();

            bossHitboxElement.GetHitbox().SetMaxHP(bossHitboxElement.GetMaxHP());
            bossHitboxElement.GetHitbox().SetHP(bossHitboxElement.GetHitbox().GetMaxHP());
            bossHitboxElement.GetHitbox().SetWeakpoint(bossHitboxElement.GetWeakpoint());

        }

    }

    public int GetActivePhase()
    {
        return (activePhase - 1);
    }

}