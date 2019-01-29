using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDummy : MonoBehaviour
{
    DashAction dashAction;
    private void Start()
    {
        dashAction = FindObjectOfType<DashAction>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player Hitbox" && dashAction.velocity != 0)
        {
            gameObject.SetActive(false);
            TutorialController.instance.CheckDashingGoal();
            
        }
    }
}
