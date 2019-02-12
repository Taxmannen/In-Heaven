using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : MonoBehaviour
{
    float damage;

    [SerializeField]
    internal ParticleSystem vfx;

    private void Start()
    {
        if (vfx == null)
        {
            vfx = GetComponentInChildren<ParticleSystem>();
        }

        if (vfx != null)
        {
            vfx.Play();
        }
    }

    public void SetDamage(float damage)
    {
        this.damage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Player Hitbox")
        {
            other.GetComponentInParent<PlayerController>().Receive(damage);
        }

    }

}