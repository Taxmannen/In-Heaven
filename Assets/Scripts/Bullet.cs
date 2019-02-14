using System.Collections;
using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson
/// </summary>
public class Bullet : MonoBehaviour
{
    [SerializeField] private float damage = 1;
    [SerializeField] private bool fromPlayer;
    [SerializeField] private float UIAnimationTime = 2;

    [Header("Effect")]
    [SerializeField] private GameObject collisionEffect;
    [SerializeField] private GameObject collisionEffect2; //Skall byta namn
    [SerializeField] private Transform rayFrom;
    [SerializeField] private LayerMask layermask;

    private Vector3 hitPoint;
    private float UISpawnTime;
    private Coroutine coroutine;
    private Vector3 effectPoint;

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
        if (other.tag == "Ground" || other.tag == "Player Hitbox") SpawnEffect();
        if (other.tag == "Player Hitbox" && !fromPlayer)
        {
            other.GetComponentInParent<PlayerController>().Receive(damage);
            AudioController.instance.PlayerTakingDamage();
            Destroy(gameObject);
        }
    }

    public void SetPoint(Vector3 point)
    {
        hitPoint = point;
    }


    private IEnumerator InstantiateBulletOverlay(Vector3 originTemp, Vector3 targetTemp, float speedTemp)
    {
        UISpawnTime = (Vector3.Distance(originTemp, targetTemp)) / speedTemp - UIAnimationTime;
        yield return new WaitForSeconds(UISpawnTime);
        InterfaceController.instance.BossBulletOverlay(targetTemp);
        yield return null;
    }

    private void SpawnEffect()
    {
        if (rayFrom != null)
        {
            Vector3 fwd = rayFrom.TransformDirection(Vector3.back);
            Debug.DrawRay(rayFrom.position, fwd * 10, Color.green, 5);

            if (Physics.Raycast(rayFrom.position, fwd, out RaycastHit hit, 50, layermask))
            {
                if (collisionEffect != null)
                {
                    effectPoint = hit.point;
                    GameObject effect = Instantiate(collisionEffect, hit.point, collisionEffect.transform.rotation, transform.parent);
                    Destroy(effect, 3);
                    if (collisionEffect2 != null) Invoke("SpawnNextEffect", 0.2f);
                }
            }
        }
    }

    private void SpawnNextEffect()
    {
        GameObject effect2 = Instantiate(collisionEffect2, effectPoint, collisionEffect.transform.rotation, transform.parent);
        Destroy(effect2, 3);
    }
}