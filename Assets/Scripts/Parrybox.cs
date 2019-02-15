﻿using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson, Edited By: Vidar M
/// </summary>
public class Parrybox : MonoBehaviour
{
    private PlayerController playerController;

    [SerializeField] private ParticleSystem parryActivation;
    [SerializeField] private ParticleSystem parryEffect;

    private void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss Parryable Bullet")
        {
            Statistics.instance.numberOfSuccessfulParrys++;
            AudioController.instance.PlayerSuccessfulParry();
            parryActivation.Stop();
            parryActivation.gameObject.SetActive(false);
            parryEffect.Play();
            Destroy(other.gameObject);
            if(other.GetComponent<Bullet>().isParrayable)
            {
                playerController.superChargeResource.IncreaseSuperCharge();
            }
            
            GetComponent<Collider>().enabled = false;
        }

        else if (other.tag == "TutorialBullet")
        {
            Statistics.instance.numberOfSuccessfulParrys++;
            AudioController.instance.PlayerSuccessfulParry();
            Destroy(other.gameObject);   
            playerController.superChargeResource.IncreaseSuperCharge(3);
            GetComponent<Collider>().enabled = false;
            TutorialController.instance.CheckParryGoal();
        }
    }
}