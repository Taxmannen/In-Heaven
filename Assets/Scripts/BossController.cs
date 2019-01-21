using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    //Design
    [SerializeField] [Range(0, 100000)] private int maxHP = 1000;
    [SerializeField] [Range(0, 1)] private float bossBulletFireRate;

    //Debug
    [SerializeField] [ReadOnly] private int hP;
    [SerializeField] [ReadOnly] Global.BossState bossState = Global.BossState.Default;
    [SerializeField] [Range(0, 10)] private float moveSpeed;

    [SerializeField] private Vector3 rightStop = new Vector3 (36, 0 , 0);
    [SerializeField] private Vector3 leftStop = new Vector3 (-36, 0 , 0);
    [SerializeField] [Range (0, 500)]private float bossBulletSpeed;
    //Debug
    [SerializeField] private bool canMove;
    // Private
    private Coroutine bossShootCoroutine;
    private bool moveRight = true;
    private bool moveLeft;
    public Transform bossBulletSpawn1;
    public Transform bossBulletSpawn2;
    public GameObject bossBullet;

    private void Start()
    {
        hP = maxHP;

        InterfaceController.instance.UpdateBossHP(hP, maxHP);
        InterfaceController.instance.UpdateBossState(bossState);
    }

    private void Update()
    {
        if (canMove)
        {
            if (bossShootCoroutine == null)
            {               
                bossShootCoroutine = StartCoroutine(bossShoot());
            }

            if (moveRight)
            {
                gameObject.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);                
                if (gameObject.transform.position.x > rightStop.x)
                {
                    moveRight = false;
                    moveLeft = true;
                }
            }
            if (moveLeft)
            {
                gameObject.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
                if(gameObject.transform.position.x < leftStop.x)
                {
                    moveLeft = false;
                    moveRight = true;
                }
            }
            //gameObject.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            //if (gameObject.transform.position.x > rightStop.x)
            //{
            //    gameObject.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
            //    if (gameObject.transform.position.x > leftStop.x)
            //    {

            //    }
            //}
        }
        
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Bullet")
        {

            if (bossState == Global.BossState.Default)
            {
                if (hP - other.GetComponent<Bullet>().GetDamage() <= 0)
                {
                    Die();
                    AudioController.instance.BossHit();
                }

                else
                {
                    hP -= other.GetComponent<Bullet>().GetDamage();
                    InterfaceController.instance.UpdateBossHP(hP, maxHP);
                    AudioController.instance.BossHit();
                }
            }

        }
        
    }

    private void Die()
    {
        bossState = Global.BossState.Dead;
        AudioController.instance.BossDeath();
        hP = 0;
        InterfaceController.instance.UpdateBossHP(hP, maxHP);
        //bossState = PHASE
        //gameState = COMPLETE (if last phase died)
        InterfaceController.instance.Fail(); //winScreen
    }

    //Inspector
    [ExecuteInEditMode]
    void OnValidate()
    {
        hP = maxHP;
    }

    private IEnumerator bossShoot()
    {
        GameObject  bossBulletClone = Instantiate(bossBullet, bossBulletSpawn1.position, bossBulletSpawn1.rotation);
        Destroy(bossBulletClone, 3f);
        bossBulletClone.GetComponent<Rigidbody>().velocity = new Vector3(0, -0.4f*bossBulletSpeed, -bossBulletSpeed);

        bossBulletClone.GetComponent<Bullet>().SetDamage(25);

        bossBulletClone = Instantiate(bossBullet, bossBulletSpawn2.position, bossBulletSpawn2.rotation);
        Destroy(bossBulletClone, 3f);
        bossBulletClone.GetComponent<Rigidbody>().velocity = new Vector3(0, -0.4f * bossBulletSpeed, -bossBulletSpeed);

        bossBulletClone.GetComponent<Bullet>().SetDamage(25);

        yield return new WaitForSeconds(bossBulletFireRate);
        bossShootCoroutine = null;
        yield break;
    }
}
