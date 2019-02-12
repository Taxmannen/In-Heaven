using System.Collections;
using UnityEngine;

public class BA_LaserCore : BossAttack
{
    //Variables
    private LaserCoreData data;

    [SerializeField]
    internal GameObject laser;

    [SerializeField] [ReadOnly]private string startSide;

    protected override IEnumerator Execute(Boss boss)
    {
        SpawnLaser();

        SetStartSide();

        InvokeRepeating("MoveLaser", 0, 0.01f);

        yield return new WaitForSeconds(data.attackTime);
        CancelInvoke("MoveLaser");
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
                        Debug.Log("Works");
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
            Debug.Log("SetAttackData: " + gameObject);
        }
        else
        {
            Debug.LogError("Wrong Data!!" + gameObject);
        }
    }
}