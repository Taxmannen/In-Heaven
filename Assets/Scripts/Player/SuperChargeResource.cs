﻿using System.Collections;
using UnityEngine;

/// <summary>
/// Made By: Jesper Uddefors and Filip Nilsson
/// </summary>
[RequireComponent(typeof(PlayerController))]
public class SuperChargeResource : MonoBehaviour
{
    private PlayerController player;

    [SerializeField] [Range(0, 100)] internal float superChargeMax = 1f;
    
    [SerializeField] [Range(0, 100)] private float superChargeIncrease = 1f;
    private Coroutine superChargeCoroutine = null;
    [SerializeField] internal float durationOfFireRateIncrease = 1f;
    [SerializeField] internal float fireRateMultiplier = 2f;
    [SerializeField] private ParticleSystem effect;

    [Header("DEBUG")]
    [SerializeField] [ReadOnly] internal float superCharge;

    void Start()
    {
        player = GetComponent<PlayerController>();
        
    }

    public void SuperCharge()
    {
        if (superCharge == superChargeMax)
        {
            if (superChargeCoroutine == null)
            {
                superChargeCoroutine = StartCoroutine(SuperChargeCoroutine());
            }
        }
    }

    private IEnumerator SuperChargeCoroutine()
    {
        superCharge = 0;
        Statistics.instance.numberOfSuperChargesUnleashed++;
        player.shootAction.playerBulletsPerSecond *= fireRateMultiplier;
        effect.Play();
        yield return new WaitForSeconds(durationOfFireRateIncrease);
        effect.Stop();
        player.shootAction.BulletPerSecondReset();
        superChargeCoroutine = null;
        yield break;
    }

    public void IncreaseSuperCharge()
    {
        if (superCharge + superChargeIncrease >= superChargeMax)
        {
            superCharge = superChargeMax;
        }

        else
        {
            superCharge += superChargeIncrease;
        }
    }

    public void IncreaseSuperCharge(float incres)
    {
        if (superCharge + superChargeMax/incres >= superChargeMax)
        {
            superCharge = superChargeMax;
        }

        else
        {
            superCharge += superChargeMax / incres;
        }
    }
}