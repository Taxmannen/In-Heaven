using System.Collections;
using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 1;
    [SerializeField] private bool fromPlayer;

    [Header("Effect")]
    [SerializeField] private GameObject collisionEffect;

    Vector3 hitPoint;

    private float UISpawnTime;
    [SerializeField]
    private float UIAnimationTime;
    private Coroutine coroutine;
    public void SetBulletOverlay(Vector3 originTemp, Vector3 targetTemp, float speedTemp)
    {
        coroutine = StartCoroutine(InstantiateBulletOverlay(originTemp, targetTemp, speedTemp));
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
            if (collisionEffect != null)
            {
                GameObject effect = Instantiate(collisionEffect, hitPoint, collisionEffect.transform.rotation, transform.parent);
                Destroy(effect, 1);
            }

            if (bhb = other.GetComponent<BossHitbox>())
            {
                if (bhb.Damagable())
                {
                    //Debug.Log("Did Damage");
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

            if (td = other.GetComponent<TargetDummy>())
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

    public void SetPoint(Vector3 point)
    {
        hitPoint = point;
    }


    private IEnumerator InstantiateBulletOverlay(Vector3 originTemp, Vector3 targetTemp, float speedTemp)
    {
        UISpawnTime = speedTemp * (Vector3.Distance(originTemp, targetTemp)) - UIAnimationTime;
        yield return new WaitForSeconds(UISpawnTime);
        InterfaceController.instance.BossBulletOverlay(targetTemp);
        yield return null;
    }

}