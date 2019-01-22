using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    //Serialized
    [SerializeField] private Transform bullets;
    [SerializeField] private Rigidbody rigi;
    [SerializeField] private Transform bossBulletSpawn1;
    [SerializeField] private Transform bossBulletSpawn2;
    [SerializeField] private GameObject bossBullet;

    // Private
    private Coroutine bossShootCoroutine;
    private bool moveRight = true;
    private bool moveLeft;
    private Coroutine newMoveCoroutine;
    private Vector3 spawn;
    private bool spawned;

    //Design
    [SerializeField] [Range(0, 10)] private float movementSpeed;
    [SerializeField] [Range(0, 100000)] private int maxHP = 1000;
    [SerializeField] [Range(0, 500)] private float bossBulletSpeed;
    [SerializeField] [Range(0, 1)] private float bossBulletFireRate;

    //Debug
    [SerializeField] [ReadOnly] private int hP;
    [SerializeField] [ReadOnly] Global.BossState bossState = Global.BossState.Default;
    [SerializeField] private Vector3 rightStop = new Vector3 (36, 0 , 0);
    [SerializeField] private Vector3 leftStop = new Vector3 (-36, 0 , 0);
    [SerializeField] private bool canMove;



    [ExecuteInEditMode]
    private void OnValidate()
    {
        hP = maxHP;
    }



    public void Start()
    {

        if (!spawned)
        {
            spawn = rigi.position;
            spawned = true;
        }

        else
        {
            rigi.position = spawn;
        }

        hP = maxHP;
        InterfaceController.instance.UpdateBossHP(hP, maxHP);

        bossState = Global.BossState.Default;
        InterfaceController.instance.UpdateBossState(bossState);

    }


    /// <summary>
    /// Applies velocity to the boss.
    /// </summary>
    public void Move()
    {

        if (moveRight)
        {

            gameObject.transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);

            if (gameObject.transform.position.x > rightStop.x)
            {
                moveRight = false;
                moveLeft = true;
            }

        }

        if (moveLeft)
        {

            gameObject.transform.Translate(Vector3.left * movementSpeed * Time.deltaTime);

            if (gameObject.transform.position.x < leftStop.x)
            {
                moveLeft = false;
                moveRight = true;
            }

        }

    }




    public void NewMove()
    {

        if (newMoveCoroutine == null)
        {
            newMoveCoroutine = StartCoroutine(NewMoveCoroutine());
        }

    }

    private IEnumerator NewMoveCoroutine()
    {

        rigi.velocity = Vector3.right * movementSpeed;
        yield return new WaitUntil(() => rigi.position.x >= 36);
        rigi.velocity = Vector3.left * movementSpeed;
        yield return new WaitUntil(() => rigi.position.x <= -36);
        newMoveCoroutine = null;
        yield break;

    }

    

    public void Freeze()
    {

        rigi.velocity = Vector3.zero;

        if (newMoveCoroutine != null)
        {
            StopCoroutine(newMoveCoroutine);
            newMoveCoroutine = null;
        }

        if (bossShootCoroutine != null)
        {
            StopCoroutine(bossShootCoroutine);
            bossShootCoroutine = null;
        }

        foreach (Transform bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }

    }



    /// <summary>
    /// (WIP) Shoots towards the direction the boss is aiming towards.
    /// </summary>
    public void Shoot()
    {
        if (bossShootCoroutine == null)
        {
            bossShootCoroutine = StartCoroutine(ShootCoroutine());
        }
    }

    private IEnumerator ShootCoroutine()
    {

        GameObject bossBulletClone = Instantiate(bossBullet, bossBulletSpawn1.position, bossBulletSpawn1.rotation, bullets);
        Destroy(bossBulletClone, 3f);
        bossBulletClone.GetComponent<Rigidbody>().velocity = new Vector3(0, -0.4f * bossBulletSpeed, -bossBulletSpeed);

        bossBulletClone.GetComponent<Bullet>().SetDamage(25);

        bossBulletClone = Instantiate(bossBullet, bossBulletSpawn2.position, bossBulletSpawn2.rotation, bullets);
        Destroy(bossBulletClone, 3f);
        bossBulletClone.GetComponent<Rigidbody>().velocity = new Vector3(0, -0.4f * bossBulletSpeed, -bossBulletSpeed);

        bossBulletClone.GetComponent<Bullet>().SetDamage(25);

        yield return new WaitForSeconds(bossBulletFireRate);
        bossShootCoroutine = null;
        yield break;
    }



    /// <summary>
    /// Event for entering triggers.
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Bullet")
        {

            if (bossState == Global.BossState.Default)
            {
                Receive(other.GetComponent<Bullet>().GetDamage());
                AudioController.instance.BossHit();
            }

        }
        
    }



    /// <summary>
    /// Checks whether the boss should die or get hit by the source depending on the amount sent as a parameter.
    /// </summary>
    /// <param name="amt"></param>
    public void Receive(int amt)
    {

        if (bossState == Global.BossState.Default)
        {
            if (hP - amt <= 0)
            {
                Die();
            }

            else
            {
                Hit(amt);
            }
        }

    }



    /// <summary>
    /// Kills the boss.
    /// </summary>
    private void Die()
    {

        Freeze();
        GameController.instance.FreezePlayer();

        bossState = Global.BossState.Dead;
        
        AudioController.instance.BossDeath();

        hP = 0;
        InterfaceController.instance.UpdateBossHP(hP, maxHP);

        bossState = Global.BossState.Dead;
        InterfaceController.instance.UpdateBossState(bossState);

        GameController.instance.SetGameState(Global.GameState.Success);
        InterfaceController.instance.Success();

    }



    /// <summary>
    /// Hits the boss for the amount sent as a parameter.
    /// </summary>
    /// <param name="amt"></param>
    private void Hit(int amt)
    {
        hP -= amt;
        InterfaceController.instance.UpdateBossHP(hP, maxHP);
    }


    
}
