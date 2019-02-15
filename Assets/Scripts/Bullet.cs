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
    public bool isParrayable;
    [Header("Impact Effect")]
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private Transform rayFrom;
    [SerializeField] private LayerMask layermask;

    private Vector3 hitPoint;
    private Coroutine coroutine;
    private float UISpawnTime;

    //Temp fix
    private Vector3 patternPos;
    private bool pattern;


    private void Update()
    {
        if(transform.position.z < -20)
        {
            transform.position = new Vector3(100, 0, 0);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public void SetBulletOverlay(Vector3 originTemp, Vector3 targetTemp, float speedTemp)
    {   
        coroutine = StartCoroutine(InstantiateBulletOverlay(originTemp, targetTemp, speedTemp));
        patternPos = targetTemp;
        pattern = true;
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
                    transform.position = new Vector3(100, 0, 0);
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                }

                else
                {
                    AudioController.instance.BossHitRecieveNoDamage();
                    transform.position = new Vector3(100, 0, 0);
                    GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }

            if (td = other.GetComponent<TargetDummy>())
            {
                td.Receive(damage);
                transform.position = new Vector3(100, 0, 0);
                GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        if (other.tag == "Ground" || other.tag == "Player Hitbox") CheckVFXPosition();
        if (other.tag == "Player Hitbox" && !fromPlayer)
        {
            other.GetComponentInParent<PlayerController>().Receive(damage);
            AudioController.instance.PlayerTakingDamage();
            transform.position = new Vector3(100, 0, 0);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
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

    private void CheckVFXPosition()
    {
        if (pattern && impactEffect != null) SpawnEffect(patternPos, 3);
        else
        {
            if (rayFrom != null)
            {
                Vector3 fwd = rayFrom.TransformDirection(Vector3.back);
                //Debug.DrawRay(rayFrom.position, fwd * 10, Color.green, 5);
                if (Physics.Raycast(rayFrom.position, fwd, out RaycastHit hit, 50, layermask) && impactEffect != null)
                {
                    SpawnEffect(hit.point, 3);
                }
            }
        }
    }

    private void SpawnEffect(Vector3 pos, float destroyTime)
    {
        GameObject effect = Instantiate(impactEffect, pos, impactEffect.transform.rotation, transform.parent);
        Destroy(effect, destroyTime);
    }
}