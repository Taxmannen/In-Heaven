using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson
/// </summary>
public class BossMovementLeftExample : BossMovement
{

    //Private

    private float movementSpeed = 1f;
    private float movementDistance = 1f;
    private float wait = 0.5f;



    //Main

    protected override IEnumerator Execute(Boss boss)
    {

        Vector3 origin = boss.transform.position;
        Rigidbody bossRigidbody = boss.GetComponent<Rigidbody>();

        bossRigidbody.velocity = new Vector3(-movementSpeed, 0, 0);

        while (bossRigidbody.position.x - (movementSpeed * Time.fixedDeltaTime) >= origin.x - movementDistance)
        {
            yield return null;
        }

        bossRigidbody.velocity = new Vector3(0, 0, 0);
        yield return null;

        bossRigidbody.MovePosition(new Vector3(origin.x - movementDistance, origin.y, origin.z));
        yield return null;

        yield return new WaitForSeconds(wait);

        executeRoutine = null;
        yield break;

    }

}
