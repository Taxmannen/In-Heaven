using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    float damage;

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Boss Hitbox")
        {
            other.GetComponentInParent<BossController>().Receive(damage);
            Destroy(gameObject);
        }

    }

}
