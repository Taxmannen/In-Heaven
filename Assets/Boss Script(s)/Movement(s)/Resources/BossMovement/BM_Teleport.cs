using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BM_Teleport : BossMovement
{
    [SerializeField] private Animator anim;
    [SerializeField] private ParticleSystem teleportStart;
    [SerializeField] private ParticleSystem teleportEnd;

    public TeleportData data;

    protected override IEnumerator Execute(Boss boss)
    {
        anim.SetTrigger("Teleport");
        anim.SetBool("TeleportGoing", true);
        teleportStart.Play();
    
        yield return new WaitForSeconds(data.delayUntilStart + 0.15f);
   
        boss.transform.position = data.teleportPosition;
        Vector3 bossPos = FindObjectOfType<Boss>().transform.position;
        FindObjectOfType<AimMechanic>().playerBulletTrajectoryDistance = Vector3.Distance(bossPos, new Vector3(bossPos.x, bossPos.y, Camera.main.transform.position.z));

        yield return new WaitForSeconds(1);

        anim.SetBool("TeleportGoing", false);
        teleportEnd.Play();

        yield return new WaitForSeconds(data.delayAfterTeleport + 0.15f);

        executeRoutine = null;
        yield break;

    }

    public override void SetMovmentData(MovementData data)
    {
        if (this.data = data as TeleportData)
        {
            //Debug.Log("SetAttackData");
        }
        else
        {
            Debug.LogError("Wrong Data!!");
        }
    }
}