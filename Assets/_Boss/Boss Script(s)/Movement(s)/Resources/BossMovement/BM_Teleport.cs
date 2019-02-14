using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BM_Teleport : BossMovement
{

    public float delayUntilStart = 0f;
    public List<TeleportData> teleportDataList = new List<TeleportData>();

    protected override IEnumerator Execute(Boss boss)
    {

        yield return new WaitForSeconds(delayUntilStart);

        foreach (TeleportData teleportData in teleportDataList)
        {

            transform.position = teleportData.GetTeleportData().Key;

            yield return new WaitForSeconds(teleportData.GetTeleportData().Value);

        }

        executeRoutine = null;
        yield break;

    }

}
