using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BM_Teleport : BossMovement
{
    public TeleportData data;

    protected override IEnumerator Execute(Boss boss)
    {

        yield return new WaitForSeconds(data.delayUntilStart);
        boss.transform.position = data.teleportPosition;

        yield return new WaitForSeconds(data.delayAfterTeleport);

        

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
