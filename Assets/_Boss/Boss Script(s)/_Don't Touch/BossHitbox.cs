﻿using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson, planned by: Filip Nilsson + Jesper Uddefors
/// </summary>
public class BossHitbox : MonoBehaviour
{
    //Read Only
    [SerializeField] [ReadOnly] private bool weakpoint;
    [SerializeField] [ReadOnly] private float maxHP;
    [SerializeField] [ReadOnly] private float hP;
    [SerializeField] [ReadOnly] private int activePhase;

    //VFXs
    [SerializeField]
    private GameObject sparks;
    [SerializeField]
    private ParticleSystem explosion;

    //Private
    private Boss boss;
    private ShaderManager shaderManager;


    //Main
    private void Start()
    {
        boss = GetComponentInParent<Boss>();
    }

    public bool Damagable()
    {
        if (!weakpoint)
        {
            return false;
        }

        if (activePhase != boss.GetActivePhase())
        {
            return false;
        }

        return true;

    }
    public void Receive(float amt)
    {

        if (hP - amt <= 0)
        {
            Die();
            ActivateSparksVFX();
            ActivateExplosionVFX();
        }

        else
        {
            Hit(amt);
            shaderManager.HitEffect(0.4f, false);
        }
    }

    private void Die()
    {
        boss.Receive(hP);
        hP = 0;
        weakpoint = false;
        gameObject.GetComponent<Renderer>().enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        AudioController.instance.BossDestruction();
    }

    private void ActivateSparksVFX() {
        if (sparks != null)
        {
            sparks.SetActive(true);
        }
        else
        {
            Debug.Log("Add sparks VFXs to the destroyed object!");
        }
    }

    private void ActivateExplosionVFX() {
        if (explosion != null)
        {
            explosion.Play();
        }
        else
        {
            Debug.Log("Add explosion VFXs to the destroyed object!");
        }
    }

    private void Hit(float amt)
    {
        hP -= amt;
        boss.Receive(amt);
    }


    //Getters & Setters
    public bool GetWeakpoint()
    {
        return weakpoint;
    }

    public void SetWeakpoint(bool weakpoint)
    {
        this.weakpoint = weakpoint;

        if (this.weakpoint)
        {
            GetComponent<Renderer>().material.color = Color.red;
            shaderManager = gameObject.AddComponent<ShaderManager>();
        }
    }

    public float GetMaxHP()
    {
        return maxHP;
    }

    public void SetMaxHP(float maxHP)
    {
        this.maxHP = maxHP;
    }

    public float GetHP()
    {
        return hP;
    }

    public void SetHP(float hP)
    {
        this.hP = hP;
    }

    public int GetActivePhase()
    {
        return activePhase;
    }

    public void SetActivePhase(int activePhase)
    {
        this.activePhase = activePhase;
    }
}