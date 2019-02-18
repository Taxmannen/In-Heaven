//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Recoil : MonoBehaviour
//{

//    [Range(1f, 20f)]
//    public float recoilForce;

//    [Range(0.001f, 2f)]
//    public float recoilTime;

//    float timer;

//    float positionX;
//    float positionY;
//    float positionZ;

//    bool gunFired = false;

//    Rigidbody handRigidBody;

//    private LimbReferenceLocation handReferenceLocation;
//    private LimbReferenceLocation shoulderReferenceLocation;

//    Vector3 handPosition;
//    Vector3 shoulderPosition;
//    Vector3 originalPosition;

//    Vector3 recoilPosition;

//    private IEnumerator corutine;


    
//    void Start()
//    {
//        handRigidBody = GetComponent<Rigidbody>();

//        handReferenceLocation = GameObject.Find("HandReference").GetComponent<LimbReferenceLocation>();
//        shoulderReferenceLocation = GameObject.Find("ShoulderReference").GetComponent<LimbReferenceLocation>();

//        corutine = RecoilCorutine();
//    }
    
//    private IEnumerator RecoilCorutine()
//    {
//        originalPosition = transform.position;

//        recoilPosition = new Vector3(transform.position.x, transform.position.y, recoilForce);

//        transform.position = Vector3.MoveTowards(transform.position, recoilPosition, recoilTime);
//        yield return new WaitForSeconds(recoilTime);

//        transform.position = Vector3.MoveTowards(transform.position, originalPosition, recoilTime);
//    }
   



//    void Update()
//    {
//        timer = timer + Time.deltaTime;
//        handPosition = handReferenceLocation.LimbReferencePosition(positionX, positionY, positionZ);
//        shoulderPosition = shoulderReferenceLocation.LimbReferencePosition(positionX, positionY, positionZ);


//        if (Input.GetMouseButton(0) && timer >= recoilTime)
//        {
//            StartCoroutine(corutine);

//          //  currentPosition = handPosition;
//          //  Debug.Log("Shit might work soon");
//          //handRigidBody.AddForce(shoulderPosition*recoilForce);
//          ////handRigidBody.AddForce(transform.up * recoilForce);

//          //  gunFired = true;

//          //  timer = 0;
//        }

//        else if (timer>=recoilTime&&gunFired==true)
//        {
//            transform.position = currentPosition;
//            gunFired = false;
//            timer = 0;
//        }

//    }
//}
