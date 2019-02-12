using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCursor : MonoBehaviour
{
    [Range(0f, 120f)]   
    public float cameraBossDistance;

    Vector3 positionOfCursor;
   
    void Start()
    {
        positionOfCursor = Input.mousePosition;
        positionOfCursor.z = cameraBossDistance;
        
    }

  
    void Update()
    {
        positionOfCursor.x = Input.mousePosition.x;
        positionOfCursor.y = Input.mousePosition.y;

        Vector3 mouseLocation = Camera.main.ScreenToWorldPoint(positionOfCursor);

        transform.position = mouseLocation;
    }

    public Vector3 CursorLocation(float cursorLocationX, float cursorLocationY, float cursorLocationZ)
    {

        cursorLocationX = transform.position.x;
        cursorLocationY = transform.position.y;
        cursorLocationZ = transform.position.z;

        Vector3 mousePosition = new Vector3(cursorLocationX, cursorLocationY, cursorLocationZ);

        return mousePosition;
    }

}