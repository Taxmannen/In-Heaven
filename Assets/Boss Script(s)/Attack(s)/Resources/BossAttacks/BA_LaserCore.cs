﻿using System.Collections;
using UnityEngine;

public class BA_LaserCore : BossAttack
{
    //Variables
    private LaserCoreData data;

    [SerializeField]
    internal ParticleSystem vfx_Laser_Start;

    [SerializeField]
    internal ParticleSystem vfx_Laser_Shoot;

    [SerializeField]
    internal GameObject vfx_Laser;

    [SerializeField]
    internal GameObject laser;

    [SerializeField]
    internal Animator animator;

    [SerializeField] [ReadOnly]private string startSide;

    protected override IEnumerator Execute(Boss boss)
    {
        //Laser_Start
        animator.SetTrigger("LaserTrigger");
        vfx_Laser_Start.Play();

        yield return new WaitForSeconds(1.65f);

        //Laser_Shoot
        animator.SetBool("Laser", true);
        vfx_Laser.SetActive(true);
        vfx_Laser_Shoot.Play();

        yield return new WaitForSeconds(2f);

        //Laser_End
        animator.SetBool("Laser", false);
        vfx_Laser.SetActive(false);

        yield return new WaitForSeconds(1f);

        SpawnLaser();

        SetStartSide();

        InvokeRepeating("MoveLaser", 0, 0.01f);

        yield return new WaitForSeconds(data.attackTime);
        CancelInvoke("MoveLaser");
        Destroy(laser);
        AudioController.instance.StopBossLaserLoop();
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
        laser.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
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
}