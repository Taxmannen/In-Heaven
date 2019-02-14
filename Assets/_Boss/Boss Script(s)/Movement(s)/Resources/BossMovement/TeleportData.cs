using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "TeleportData", menuName = "Boss/Teleport")]
public class TeleportData : MovementData
{
    public float delayUntilStart = 0f;
    public Vector3 teleportPosition;
    public float delayAfterTeleport;

}
