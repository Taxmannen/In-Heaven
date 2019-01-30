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
            other.GetComponentInParent<Character>().Receive(1);
            Destroy(gameObject);
        }

        else if (other.tag == "Boss Damage Hitbox")
        {
            AudioController.instance.BossHitRecieveDamage();
            other.GetComponentInParent<Character>().Receive(damage);
            Destroy(gameObject);
        }

    }

}
