using System.Collections;
using UnityEngine;

public class BA_LaserCore : BossAttack
{
    //Variables
    private LaserCoreData data;

    [SerializeField]
    internal ParticleSystem vfx_Laser_Start01;

    [SerializeField]
    internal ParticleSystem vfx_Laser_Start02;

    [SerializeField]
    internal ParticleSystem vfx_Laser_Shoot;

    [SerializeField]
    internal GameObject vfx_Laser;

    [SerializeField]
    internal GameObject laser;

    private Transform scorchMarkTransform;

    [SerializeField]
    internal Animator animator;

    [SerializeField] [ReadOnly]private string startSide;

    protected override IEnumerator Execute(Boss boss)
    {
        //Laser_Start
        animator.SetTrigger("LaserTrigger");
        vfx_Laser_Start01.Play();
        vfx_Laser_Start02.Play();
        AudioController.instance.BossLaserShoot();

        yield return new WaitForSeconds(1.65f);

        //Laser_Shoot
        animator.SetBool("Laser", true);
        animator.SetLayerWeight(2, 1);
        vfx_Laser.SetActive(true);
        vfx_Laser_Shoot.Play();

        GameObject laserGO = Instantiate(data.laserPrefab, null);

        laserGO.transform.position = data.startPosition;

        Vector3 direction = data.endPosition - data.startPosition;

        direction.Normalize();

        laserGO.GetComponent<Rigidbody>().velocity = direction * data.speed;

        if (data.speed > 0)
        {

            if (direction.x > 0)
            {
                yield return new WaitUntil(() => laserGO.transform.position.x > data.endPosition.x);
            }

            else if (direction.x < 0)
            {
                yield return new WaitUntil(() => laserGO.transform.position.x < data.endPosition.x);
            }

            else
            {
                Debug.LogError("You fucked up, start and end positions are the same on laser noob.");
                yield break;
            }

            
        }

        else
        {
            Debug.LogError("You fucked up, speed is 0 or less than 0 on laser noob.");
            yield break;
        }
        //Laser_End
        AudioController.instance.BossLaserCharge();
        animator.SetBool("Laser", false);
        animator.SetLayerWeight(2, 0);
        vfx_Laser.SetActive(false);

        yield return new WaitForSeconds(2.5f);

        Destroy(laserGO);
        AudioController.instance.StopBossLaserLoop();
        executeRoutine = null;
        yield break;
    }

    public override void SetAttackData(AttackData data)
    {
        if (this.data = data as LaserCoreData)
        {
            //Debug.Log("SetAttackData: " + gameObject);
        }
        else
        {
            Debug.LogError("Wrong Data!!" + gameObject);
        }
    }

    #region Legacy
    /*
    protected override IEnumerator Execute(Boss boss)
    {
        

        

        yield return new WaitForSeconds(2f);

        

        yield return new WaitForSeconds(1f);

        SpawnLaser();

        SetStartSide();

        InvokeRepeating("MoveLaser", 0, 0.01f);

        yield return new WaitForSeconds(data.attackTime);
        CancelInvoke("MoveLaser");

        // getComponent.getComponent.GetThing.GetAnotherThing.GetSword.SlayBoss.Camera.Main
        scorchMarkTransform = laser.transform.GetChild(1).GetChild(3);
        scorchMarkTransform.SetParent(null);
        Destroy(scorchMarkTransform.gameObject, 5);

        Destroy(laser);
        
        executeRoutine = null;
        yield break;
    }

    //Code runs
    void SpawnLaser()
    {
        laser = Instantiate(data.laserPrefab, new Vector3(data.startPosition.x, data.startPosition.y, data.startPosition.z), Quaternion.identity,transform);
        laser.GetComponent<BossLaser>().SetDamage(1);

        
    }

    void SetStartSide()
    {
        if (data.startPosition.x <= data.endPosition.x)
            startSide = "LEFT";
        if (data.startPosition.x > data.endPosition.x)
            startSide = "RIGHT";
    }


    private bool startDirection = true;

    void MoveLaser()
    {
        if (laser != null)
        {
            if (startSide == "LEFT")
            {
                if (startDirection)
                {
                    if (laser.transform.position.x <= data.endPosition.x)
                    {
                        laser.GetComponent<Rigidbody>().velocity = data.speed;
                    }
                    else
                    {
                        StartCoroutine("StopAndChangeDirection");
                    }
                }

                if (!startDirection)
                {
                    if (laser.transform.position.x >= data.startPosition.x)
                    {
                        laser.GetComponent<Rigidbody>().velocity = -data.speed;
                    }
                    else
                    {
                        StartCoroutine("StopAndChangeDirection");
                    }
                }
            }

            if (startSide == "RIGHT")
            {
                if (startDirection)
                {
                    if (laser.transform.position.x >= data.endPosition.x)
                    {
                        laser.GetComponent<Rigidbody>().velocity = -data.speed;
                    }
                    else
                    {
                        StartCoroutine("StopAndChangeDirection");
                    }
                }

                if (!startDirection)
                {
                    if (laser.transform.position.x <= data.startPosition.x)
                    {
                        laser.GetComponent<Rigidbody>().velocity = data.speed;
                    }
                    else
                    {
                        StartCoroutine("StopAndChangeDirection");
                    }
                }
            }
        }
    }

    IEnumerator StopAndChangeDirection()
    {
        CancelInvoke("MoveLaser");
        //laser.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(data.laserStayTime);
        InvokeRepeating("MoveLaser", 0, 0.001f);
        startDirection = !startDirection;
    }

    public override void SetAttackData(AttackData data)
    {
        if (this.data = data as LaserCoreData)
        {
            //Debug.Log("SetAttackData: " + gameObject);
        }
        else
        {
            Debug.LogError("Wrong Data!!" + gameObject);
        }
    }
    */
    #endregion

}