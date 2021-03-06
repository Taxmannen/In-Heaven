﻿using System.Collections;
using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson, Edited by Daniel
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 1;
    [SerializeField] private bool fromPlayer;
    [SerializeField] private float UIAnimationTime = 2;
    public bool isParrayable;
    [SerializeField] private TrailRenderer trail;

    [Header("Impact Effect")]
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private LayerMask layermask;


    private Vector3 direction;
    private Vector3 hitPoint;
    private Coroutine coroutine;
    private float UISpawnTime;
    internal ParticleSystem particleEffect;
    internal Vector3 savedLocalScale;

    private void FixedUpdate()
    {
        if (transform.position.z < -20 || transform.position.y < -10)
        {
            ResetBullet();
        }

        if (fromPlayer && transform.position.z > 500)
        {
            ResetBullet();
            fromPlayer = false;
        }
    }

    public void SetBulletOverlay(Vector3 originTemp, Vector3 targetTemp)
    {
        coroutine = StartCoroutine(InstantiateBulletOverlay(originTemp, targetTemp));
    }

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
            if (impactEffect != null) SpawnEffect(hitPoint, 1);

            if (bhb = other.GetComponent<BossHitbox>())
            {
                if (bhb.Damagable())
                {
                    //Debug.Log("Did Damage");
                    other.GetComponent<BossHitbox>().Receive(damage);
                    AudioController.instance.BossHitRecieveDamage();
                    ResetBullet();
                }
                else
                {
                    AudioController.instance.BossHitRecieveNoDamage();
                    ResetBullet();
                }
            }

            if (td = other.GetComponent<TargetDummy>())
            {
                td.Receive(damage);

            }
        }
        if (other.tag == "Ground" || other.tag == "Player Hitbox") CheckVFXPosition();
        if (other.tag == "Player Hitbox" && !fromPlayer)
        {
            other.GetComponentInParent<PlayerController>().Receive(damage);
            ResetBullet();
        }
    }

    private IEnumerator InstantiateBulletOverlay(Vector3 originTemp, Vector3 targetTemp)
    {


        float speedTemp = GetComponent<Rigidbody>().velocity.magnitude;

        UISpawnTime = (Vector3.Distance(originTemp, targetTemp)) / speedTemp - InterfaceController.instance.targetOverlay.GetComponent<ParticleSystem>().main.startLifetime.constant;

        yield return new WaitForSeconds(UISpawnTime);

        InterfaceController.instance.BossBulletOverlay(targetTemp);

        yield return null;

    }

    private void CheckVFXPosition()
    {
        if (impactEffect != null)
        {
            Vector3 startPos = transform.position + (direction * -10);
            //Debug.DrawRay(startPos, direction * 10, Color.red, 2);
            if (Physics.Raycast(startPos, direction, out RaycastHit hit, 20, layermask))
            {
                SpawnEffect(hit.point, 3);
            }
        }
    }

    private void SpawnEffect(Vector3 pos, float destroyTime)
    {
        GameObject effect = Instantiate(impactEffect, pos, impactEffect.transform.rotation, transform.parent);
        Destroy(effect, destroyTime);
    }

    public void ResetBullet()
    {
        SetTrailRenderer(false);
        transform.position = new Vector3(0, -1000, 0);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void SetPoint(Vector3 point)
    {
        hitPoint = point;
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }

    public void SetTrailRenderer(bool state)
    {
        if (trail != null)
        {
            trail.Clear();
            trail.enabled = state;
        }
    }
}