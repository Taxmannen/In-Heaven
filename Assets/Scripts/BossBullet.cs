using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson
/// </summary>
public class BossBullet : MonoBehaviour
{

    float damage;

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        /*
        if (other.tag == "Player Hitbox")
        {
            other.GetComponentInParent<PlayerController>().Receive(damage);
            Destroy(gameObject);
        }
        */
    }

}
