using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BA_LaserCore : BossAttack
{
    //Variables
    [SerializeField]
    private GameObject laser;

    [SerializeField]
    private float attackTime = 5f;

    [SerializeField]
    private Vector3 speed = new Vector3 (10, 0, 0);

    private List<GameObject> lasers;

    [SerializeField]
    public Vector3 startPosition = new Vector3(0,0,0);

    [SerializeField]
    public Vector3 endPosition = new Vector3(0, 0, 0);

    [SerializeField]
    private float laserStayTime = 5f;

    private string startSide;

    protected override IEnumerator Execute(Boss boss)
    {
        SpawnLaser();

        SetStartSide();

        InvokeRepeating("MoveLaser", 0, 0.01f);

        yield return new WaitForSeconds(attackTime);
        CancelInvoke("MoveLaser");
        Destroy(laser);
        executeRoutine = null;
        yield break;
    }

    //Code runs
    void SpawnLaser() {
        laser = Instantiate(laser, new Vector3(startPosition.x, startPosition.y, startPosition.z), Quaternion.identity);
        laser.GetComponent<BossLaser>().SetDamage(1);
    }

    void SetStartSide() {
        if (startPosition.x < endPosition.x)
            startSide = "LEFT";
        if (startPosition.x > endPosition.x)
            startSide = "RIGHT";
    }


    private bool startDirection = true;

    void MoveLaser() {

        if(startSide == "LEFT")
        {
            if (startDirection)
            {
                if (laser.transform.position.x <= endPosition.x)
                {
                    laser.GetComponent<Rigidbody>().velocity = speed;
                }
                else
                {
                    StartCoroutine("StopAndChangeDirection");
                }


            }

            if (!startDirection)
            {
                if (laser.transform.position.x >= startPosition.x)
                {
                    laser.GetComponent<Rigidbody>().velocity = -speed;
                }
                else
                {
                    StartCoroutine("StopAndChangeDirection");
                }
            }
        }
        
        if(startSide == "RIGHT")
        {
            if (startDirection)
            {
                if (laser.transform.position.x >= endPosition.x)
                {
                    laser.GetComponent<Rigidbody>().velocity = -speed;
                    Debug.Log("Works");
                }
                else
                {
                    StartCoroutine("StopAndChangeDirection");
                }


            }

            if (!startDirection)
            {
                if (laser.transform.position.x <= startPosition.x)
                {
                    laser.GetComponent<Rigidbody>().velocity = speed;
                }
                else
                {
                    StartCoroutine("StopAndChangeDirection");
                }
            }
        }
        
    }

    IEnumerator StopAndChangeDirection() {
        CancelInvoke("MoveLaser");
        laser.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        yield return new WaitForSeconds(laserStayTime);
        InvokeRepeating("MoveLaser", 0, 0.001f);
        startDirection = !startDirection;
    }

}
