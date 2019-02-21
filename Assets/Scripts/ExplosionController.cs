using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    //Code by Tåqvist

    [SerializeField]
    private float explosionTime;

    [SerializeField]
    private float minTimeBetween;
    [SerializeField]
    private float maxTimeBetween;

    private float randomTimeBetween;

    private Coroutine coroutine1;
    private Coroutine coroutine2;

    [SerializeField]
    private ParticleSystem[] explosions;

    //Used for testing
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        StartExplosions();
    //    }
    //}

    public void StartExplosions()
    {
        coroutine1 = StartCoroutine(ActivateExplosionVFX());
        coroutine2 = StartCoroutine(Timer());
    }

    private IEnumerator ActivateExplosionVFX()
    {
        for (int i = 0; i < explosions.Length; i++) {
            if (explosions[i] != null)
            {
                randomTimeBetween = Random.Range(minTimeBetween, maxTimeBetween);
                explosions[i].Play();
                yield return new WaitForSeconds(randomTimeBetween);
            }
            else
            {
                Debug.Log("Add explosion VFXs to Explosions-Array!");
            }
            if (i == explosions.Length - 1)
            {
                i = 0;
            }
        }
        yield return null;
    }

    private IEnumerator Timer() {
        yield return new WaitForSeconds(explosionTime);
        StopCoroutine(coroutine1);
        yield return null;
    }
}
