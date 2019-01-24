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
            AudioController.instance.BossHitRecieveNoDamage();
            other.GetComponentInParent<BossController>().Receive(1);
            Destroy(gameObject);
        }

        else if (other.tag == "Boss Damage Hitbox")
        {
            AudioController.instance.BossHitRecieveDamage();
            other.GetComponentInParent<BossController>().Receive(damage);
            Destroy(gameObject);
        }

    }

}
