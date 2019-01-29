using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementDummy : MonoBehaviour
{
    //public GameObject movementDummy;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player Hitbox")
        {
            gameObject.SetActive(false);
            TutorialController.instance.CheckMovementGoal();

        }
    }
}
