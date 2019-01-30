using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialParryBulletSpeedBox : MonoBehaviour
{

    internal Coroutine lowerBulletSpeedRoutine = null;
    internal Coroutine increaseBulletSpeedRoutine = null;
    [SerializeField] TutorialCannon tutorialCannon;


    private void Start()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TutorialBullet")
        {
            if (lowerBulletSpeedRoutine == null)
            {
                Debug.Log("Entered");
                lowerBulletSpeedRoutine = StartCoroutine(LowerBulletSpeed());
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "TutorialBullet")
        {

            if (increaseBulletSpeedRoutine == null)
            {
                Debug.Log("Exited");
                //increaseBulletSpeedRoutine = StartCoroutine(IncreaseBulletSpeed());


            }
        }
    }



    private IEnumerator LowerBulletSpeed()
    {
        while (true)
        {

            tutorialCannon.LowerSpeed();

                if (tutorialCannon.bulletSpeed < 3)
                {
                    tutorialCannon.bulletSpeed = 2;
                }
            
            yield return new WaitForSeconds(0.01f);
        }
        lowerBulletSpeedRoutine = null;
        yield break;
    }
    /*private IEnumerator IncreaseBulletSpeed()
    {
        while (true)
        {
            Debug.Log("Testing");
            tutorialCannon.IncreaseSpeed();
            yield return new WaitForSeconds(0.01f);
        }
        lowerBulletSpeedRoutine = null;
        yield break;
    }*/
}



//private IEnumerator LowerBulletSpeed()
//{
//    while (true)
//    {
//        if (tutorialbullet != null)
//        {
//            tutorialbullet.UpdateVelocity(tutorialbullet.bulletSpeed--);
//            if (tutorialbullet.bulletSpeed < 3)
//            {
//                tutorialbullet.bulletSpeed = 2;
//            }
//        }
//        yield return new WaitForSeconds(0.03f);
//    }
//    lowerBulletSpeedRoutine = null;
//    yield break;
//}
//    private IEnumerator IncreaseBulletSpeed()
//    {
//        while (true)
//        {
//            Debug.Log("Increase");
//            tutorialbullet.UpdateVelocity(tutorialbullet.bulletSpeed++);
//            yield return new WaitForSeconds(0.01f);


//            yield break;
//        }
//        yield return new WaitForSeconds(1f);
//        increaseBulletSpeedRoutine = null;
//        yield break;



//    }
//    //private IEnumerator WaitaWhile()
//    //{
//    //    yield return new WaitForSeconds (1);

//    //    yield break;
//    //}

//}
