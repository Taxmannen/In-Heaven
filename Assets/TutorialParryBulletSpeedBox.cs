using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialParryBulletSpeedBox : MonoBehaviour
{
     TutorialBullet tutorialbullet;
    private IEnumerator coroutine;
    float speed;
    private void Start()
    {

        coroutine = LowerBulletSpeed(0.5f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "TutorialBullet")
        {
            Debug.Log("LKASJFAKF");
            StartCoroutine(coroutine);
            
        }
    }
    private IEnumerator LowerBulletSpeed (float time)
    {
        tutorialbullet.bulletSpeed--;
        yield return new WaitForSeconds (time);
    }
}
