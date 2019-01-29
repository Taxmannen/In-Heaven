using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : Character
{

    //Serialized
    [SerializeField] private Transform bullets;
    [SerializeField] private Rigidbody rigi;
    SpreadShot spreadShot;

    [SerializeField] private List<Transform> bossBulletSpawnPoint;

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
    [SerializeField] [Range(0, 500)] private float bossBulletSpeed;
    [SerializeField] [Range(1, 100)] private float bossBulletFireRate;
    [SerializeField] [Range(1, 10)] private float bossLaserRaySpeed;

    //Debug
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

        spreadShot = GetComponent<SpreadShot>();

    }
    private void Update()
    {
        if(GameController.instance.gameState == Global.GameState.Game)
        {
            UpdateBoss();
        }
       
    }
    /// <summary>
    /// Updates everything in boss.
    /// </summary>
    private void UpdateBoss()
    {
        NewMove();

        if (patternCoroutine == null)
        {
            patternCoroutine = StartCoroutine(Pattern());
        }

    }

    private Coroutine patternCoroutine;

    private float counter;

    bool ready = true;

    private IEnumerator Pattern()
    {


        int random = Random.Range((int)1, (int)4); // Change to 4 for spread

        Vector3 SpreadShotBulletSpawnPosition = RandomSpawnPoint();
        float spreadShotStartingTarget = spreadShot.generateSpreadShootTarget();

        yield return new WaitForSeconds(1f);
        // Debug.Log("Testing");
        //int i = 0;
        for (counter = 3; counter > 0; counter -= Time.deltaTime)
        {
            //  i++;
            //Debug.Log(i);

            switch (random)
            {
                case 1:
                    RandomSpawnPointShoot();
                    break;

                case 2:
                    Laser();
                    break;

                case 3:
                    spreadShot.SpreadShotShoot(SpreadShotBulletSpawnPosition, spreadShotStartingTarget);
                    break;
            }

            yield return null;
        }

        patternCoroutine = null;
        yield break;

    }

    private IEnumerator ShootPattern()
    {

        ready = false;

        for (counter = 3; counter > 0; counter -= Time.deltaTime)
        {
            Shoot();
            yield return null;
        }

        ready = true;
        patternCoroutine = null;
        yield break;
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

    

    /// <summary>
    /// Freezes the boss.
    /// </summary>
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
    public Vector3 RandomSpawnPoint()
    {
       
        Vector3 spawnPoint = new Vector3(rigi.position.x - 14, rigi.position.y + 15.7f, rigi.position.z - 13);
        if (bossBulletSpawnPoint.Count > 0)
        {
            int randomNumber = Random.Range(0, bossBulletSpawnPoint.Count);
            spawnPoint = bossBulletSpawnPoint.ToArray()[randomNumber].position;

        }
        return spawnPoint;
    }
    public void RandomSpawnPointShoot()
    {
        Vector2 target = FindObjectOfType<PlayerController>().GetComponent<Rigidbody>().position;
        if(bossBulletSpawnPoint.Count > 0)
        {
            int randomNumber = Random.Range(0, bossBulletSpawnPoint.Count);
            Vector3 spawnPoint = bossBulletSpawnPoint.ToArray()[randomNumber].position;
            Shoot(target, spawnPoint);
        }else
        {
            Shoot(target);
        }
    }

    /// <summary>
    /// (WIP) Shoots towards the direction the boss is aiming towards.
    /// </summary>
    public void Shoot()
    {
        if (bossShootCoroutine == null)
        {
            Vector2 target = FindObjectOfType<PlayerController>().GetComponent<Rigidbody>().position;
            Shoot(target);
        }
    }
    public void Shoot(Vector2 target)
    {
        if (bossShootCoroutine == null)
        {
            Vector3 bossBulletSpawnPosition = new Vector3(rigi.position.x - 14, rigi.position.y + 15.7f, rigi.position.z - 13);
            Shoot(target, bossBulletSpawnPosition);
        }
    }
    public void Shoot(Vector2 target, Vector3 spawnPosition)
    {
        if (bossShootCoroutine == null)
        {
            bossShootCoroutine = StartCoroutine(ShootCoroutine(target, spawnPosition,bossBullet));
        }
    }
    private IEnumerator ShootCoroutine(Vector3 target, Vector3 spawnPosition, GameObject bullet)
    {
        GameObject bossBulletClone = Instantiate(bullet, spawnPosition, Quaternion.identity, bullets);
        AudioController.instance.BossShoot();
        Destroy(bossBulletClone, 3f);
        

        Vector3 dir = target - bossBulletClone.GetComponent<Rigidbody>().position;

        dir.Normalize();

        bossBulletClone.GetComponent<Rigidbody>().velocity = dir * bossBulletSpeed;

        InterfaceController.instance.BossBulletOverlay(target);

        bossBulletClone.GetComponent<BossBullet>().SetDamage(1);

        yield return new WaitForSeconds(1 / bossBulletFireRate);
        bossShootCoroutine = null;
        yield break;
    }

    /// <summary>
    /// Checks whether the boss should die or get hit by the source depending on the amount sent as a parameter.
    /// </summary>
    /// <param name="amt"></param>
    internal override void Receive(float amt)
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
    internal override void Die()
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
    internal override void Hit(float amt)
    {
        hP -= amt;
        InterfaceController.instance.UpdateBossHP(hP, maxHP);
    }



    /// <summary>
    /// Laser
    /// </summary>
    public void Laser()
    {
        StartCoroutine(LaserCoroutine());
    }

    private IEnumerator LaserCoroutine()
    {
        //Here If Needed
       Rigidbody laserRigi = GameObject.FindGameObjectWithTag("BossLaserRay").GetComponent<Rigidbody>();
        laserRigi.GetComponent<BossLaser>().SetDamage(1337);
        if (laserRigi.position.x >= 19)
        {
            laserRigi.velocity = Vector3.left * bossLaserRaySpeed;
            yield return new WaitUntil(() => laserRigi.position.x <= -19);
            laserRigi.velocity = Vector3.zero;

        }
        else if (laserRigi.position.x <= -19)
        {
            laserRigi.velocity = Vector3.right * bossLaserRaySpeed;
            yield return new WaitUntil(() => laserRigi.position.x >= 19);
            laserRigi.velocity = Vector3.zero;
        }
       


        //laserRigi.velocity = Vector3.left * 10;
        //yield return new WaitUntil(() => laserRigi.position.x <= -19);

        
        yield break;
    }
    
}
