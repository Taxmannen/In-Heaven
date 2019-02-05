using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson, Edited By: Vidar M
/// </summary>
public class Parrybox : MonoBehaviour
{

    //Serialize
    [SerializeField] public LayerMask lm = 0;

    //Private
    private PlayerController playerController;
    internal BoxCollider collider;

    private void Start()
    {
        playerController = transform.parent.GetComponent<PlayerController>();
        collider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Boss Parryable Bullet")
        {
            
            Statistics.instance.numberOfSuccessfulParrys++;
            AudioController.instance.PlayerSuccessfulParry();
            Destroy(other.gameObject);
            playerController.superChargeResource.IncreaseSuperCharge();
            GetComponent<Collider>().enabled = false;
        }

        if (other.tag == "TutorialBullet")
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