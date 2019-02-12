using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMovement : MonoBehaviour
{


    Vector3 handPosition;
    Vector3 shoulderPosition;
    Vector3 cursorPosition;

    private LimbReferenceLocation handReferenceLocation;
    private LimbReferenceLocation shoulderReferenceLocation;
    private TrackCursor trackCursor;


    [Range(0.01f, 0.5f)]
    public float timerRef;

    private bool recoilControl;

    private float Timer;

    float positionX;
    float positionY;
    float positionZ;

    float cursorLocationX;
    float cursorLocationY;
    float cursorLocationZ;


   [Range(0.1f, 4f)]
    public float recoilRate;

    void Start()
    {
        handReferenceLocation = GameObject.Find("HandReference").GetComponent<LimbReferenceLocation>();
        shoulderReferenceLocation = GameObject.Find("ShoulderReference").GetComponent<LimbReferenceLocation>();

        
    }


    void Update()
    {
        GameObject CursorReference = GameObject.Find("cursorReference");

        TrackCursor trackCursor = (TrackCursor)CursorReference.GetComponent(typeof(TrackCursor));

        handPosition = handReferenceLocation.LimbReferencePosition(positionX, positionY, positionZ);

        shoulderPosition = shoulderReferenceLocation.LimbReferencePosition(positionX, positionY, positionZ);

        cursorPosition = trackCursor.CursorLocation(cursorLocationX, cursorLocationY, cursorLocationZ);


        if (Input.GetMouseButton(0))
        {
              Timer=Timer+ Time.deltaTime;
            if (Timer <= timerRef)
            {
                transform.position = Vector3.MoveTowards(handPosition, shoulderPosition, recoilRate);
            }

            else if (Timer >= timerRef)
            {
                transform.position = Vector3.MoveTowards(handPosition, cursorPosition, recoilRate);

                Timer = 0;
            }

        }

        
    }
}
