using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{

    float damage;

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player Hitbox")
        {
            other.GetComponentInParent<PlayerController>().Receive(damage);
            Destroy(gameObject);
        }

    }

}
