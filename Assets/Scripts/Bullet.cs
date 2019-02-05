using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson
/// </summary>
public class Bullet : MonoBehaviour
{

    [SerializeField] private float damage = 1;
    [SerializeField] private bool fromPlayer;
    public void SetDamage(float damage)
    {
        this.damage = damage;
    }
    public void SetFromPlayer(bool fromPlayer)
    {
        this.fromPlayer = fromPlayer;
    }

    private void OnTriggerEnter(Collider other)
    {

        BossHitbox bhb;
        TargetDummy td;
        if (other.tag == "Boss Hitbox" && fromPlayer)
        {

            if (bhb = other.GetComponent<BossHitbox>())
            {

                if (bhb.Damagable())
                {
                    Debug.Log("Did Damage");
                    other.GetComponent<BossHitbox>().Receive(damage);
                    AudioController.instance.BossHitRecieveDamage();
                    Destroy(gameObject);
                }

                else
                {
                    AudioController.instance.BossHitRecieveNoDamage();
                    Destroy(gameObject);
                }

            }
            if(td = other.GetComponent<TargetDummy>())
            {
                td.Receive(damage);
                Destroy(gameObject);
            }

            
        }
        if (other.tag == "Player Hitbox" && !fromPlayer)
        {
            other.GetComponentInParent<PlayerController>().Receive(damage);
            Destroy(gameObject);
        }

    }

}
