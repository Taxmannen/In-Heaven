using UnityEngine;

public class HandMovement : MonoBehaviour
{
    public Vector3 handPosition;
    public Vector3 shoulderPosition;
    private LimbReferenceLocation handReferenceLocation;
    private LimbReferenceLocation shoulderReferenceLocation;

    [Range(0.01f, 0.5f)]
    public float timerRef;

    private bool recoilControl;

    private float Timer;

    float positionX;
    float positionY;
    float positionZ;

    Vector3 cursorPosition; //GLÖM EJ!

    [Range(0.1f, 4f)]
    public float recoilRate;

    void Start()
    {
        handReferenceLocation= GameObject.Find("HandReference").GetComponent<LimbReferenceLocation>();
        shoulderReferenceLocation = GameObject.Find("ShoulderReference").GetComponent<LimbReferenceLocation>();
    }


    void Update()
    {
        handPosition = handReferenceLocation.LimbReferencePosition(positionX, positionY, positionZ);
        shoulderPosition = shoulderReferenceLocation.LimbReferencePosition(positionX, positionY, positionZ);

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
