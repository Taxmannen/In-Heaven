using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialParryBulletSpeedBox : MonoBehaviour
{

    internal Coroutine lowerBulletSpeedRoutine = null;
    internal Coroutine increaseBulletSpeedRoutine = null;
    internal Coroutine resetBulletRoutine = null;
    [SerializeField] TutorialCannon tutorialCannon;

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
                StopCoroutine(lowerBulletSpeedRoutine);
                Debug.Log("Exited");
                increaseBulletSpeedRoutine = StartCoroutine(IncreaseBulletSpeed());
            }
        }
    }

    private IEnumerator LowerBulletSpeed()
    {
        while (tutorialCannon.rigi != null)
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
    private IEnumerator IncreaseBulletSpeed()
    {
        while (tutorialCannon.bulletSpeed < tutorialCannon.originalBulletSpeed)
        {
            tutorialCannon.IncreaseSpeed();
            yield return new WaitForSeconds(0.01f);
        }
        lowerBulletSpeedRoutine = null;
        yield break;
    }
    public void StopCoroutines()
    {
        StopCoroutine(IncreaseBulletSpeed());
        StopCoroutine(LowerBulletSpeed());
    }



}

