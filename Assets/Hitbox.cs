using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {

        Debug.Log("123918239812931");

        //Collider myCollider = collision.contacts[0].thisCollider;
        // Now do whatever you need with myCollider.
        // (If multiple colliders were involved in the collision, 
        // you can find them all by iterating through the contacts)
    }

}
