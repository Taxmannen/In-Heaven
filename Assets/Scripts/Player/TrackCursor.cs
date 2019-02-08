using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackCursor : MonoBehaviour
{
    
    private Rigidbody rb;
    public float cameraBossDistance;
    Vector3 cursorPosition;
    float distance;


    void Start()
    {
        cursorPosition = transform.position;
        distance = transform.position.z - Camera.main.transform.position.z;
    }

  
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            distance = cameraBossDistance;
        }
        else
        {
            distance = transform.position.z - Camera.main.transform.position.z;
        }
        

        cursorPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        cursorPosition = Camera.main.ScreenToWorldPoint(cursorPosition);

        transform.position = Vector3.MoveTowards(transform.position, cursorPosition, 10);

       
    }
}
