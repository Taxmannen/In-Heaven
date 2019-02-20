﻿using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson, Edited By: Vidar M
/// </summary>
public class Parrybox : MonoBehaviour
{
    public bool bulletParried = false;

    [Range(1f, 100f)] public float bulletRapidFireCooldown;

    private float rapidFireCooldownTimer;

    private PlayerController playerController;

    [SerializeField] private ParticleSystem parryActivation;
    [SerializeField] private ParticleSystem parryEffect;

    private void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    //private void Update()
    //{
    //    rapidFireCooldownTimer += Time.deltaTime;

    //    if (rapidFireCooldownTimer >= bulletRapidFireCooldown)
    //    {
    //        bulletParried = false;
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss Parryable Bullet")
        {
           // bulletParried = true;

            Statistics.instance.numberOfSuccessfulParrys++;
            AudioController.instance.PlayerSuccessfulParry();
            parryActivation.Stop();
            parryActivation.gameObject.SetActive(false);
            parryEffect.Play();
            other.GetComponent<Bullet>().ResetBullet();
            if (other.GetComponent<Bullet>().isParrayable)
            {
                playerController.superChargeResource.IncreaseSuperCharge();
            }
            
            GetComponent<Collider>().enabled = false;

         //   rapidFireCooldownTimer = 0;
        }

        else if (other.tag == "TutorialBullet")
        {
            Statistics.instance.numberOfSuccessfulParrys++;
            AudioController.instance.PlayerSuccessfulParry();
            other.GetComponent<Bullet>().ResetBullet();
            playerController.superChargeResource.IncreaseSuperCharge(3);
            GetComponent<Collider>().enabled = false;
            TutorialController.instance.CheckParryGoal();
        }
    }
}