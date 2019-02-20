using System.Collections;
using UnityEngine;

public class BM_Teleport : BossMovement
{
    [SerializeField] private Animator anim;
    [SerializeField] private ParticleSystem teleportStart;
    [SerializeField] private ParticleSystem teleportEnd;
    [SerializeField] private Collider coreCollider;

    public TeleportData data;

    protected override IEnumerator Execute(Boss boss)
    {
        anim.SetTrigger("Teleport");
        anim.SetBool("TeleportGoing", true);
        AudioController.instance.BossTeleportIn();
        teleportStart.Play();
        coreCollider.enabled = false;

        yield return new WaitForSeconds(data.delayUntilStart + 1.55f);
    
        boss.transform.position = data.teleportPosition;
        Vector3 bossPos = FindObjectOfType<Boss>().transform.position;
        FindObjectOfType<AimMechanic>().playerBulletTrajectoryDistance = Vector3.Distance(bossPos, new Vector3(bossPos.x, bossPos.y, Camera.main.transform.position.z));

        yield return new WaitForSeconds(data.delayAfterTeleport);

        anim.SetBool("TeleportGoing", false);
        AudioController.instance.BossTeleportOut();
        teleportEnd.Play();
        yield return new WaitForSeconds(0.5f);

        coreCollider.enabled = true;
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