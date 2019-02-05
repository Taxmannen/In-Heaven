using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made By: Vidar M
/// </summary>
public class JumpDummy : MonoBehaviour
{
    private void Start()
    {
        //HideGameobject();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player Hitbox")
        {
            HideGameobject();
            TutorialController.instance.CheckJumpingGoal();
        }
    }

    public void HideGameobject()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
