using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbReferenceLocation : MonoBehaviour
{
    

    
    public Vector3 LimbReferencePosition(float positionX, float positionY, float positionZ)
    {
        positionX = transform.position.x;
        positionY = transform.position.y;
        positionZ = transform.position.z;

        return LimbReferencePosition(positionX, positionY, positionZ);
    }

}
