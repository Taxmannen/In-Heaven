using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TeleportData
{

    public Vector3 teleportPosition;
    public float delayAfterTeleport;
    private KeyValuePair<Vector3, float> keyValuePair = new KeyValuePair<Vector3, float>(Vector3.zero, 0);

    public TeleportData()
    {
        teleportPosition = new Vector3();
        delayAfterTeleport = 0;
        keyValuePair = new KeyValuePair<Vector3, float>(teleportPosition, delayAfterTeleport);
    }

    public KeyValuePair<Vector3, float> GetTeleportData()
    {
        return keyValuePair;
    }

}
