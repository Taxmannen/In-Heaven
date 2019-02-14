using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//Script by Tåqvist
public class UIWhiteFadeAndFlash : MonoBehaviour
{
    [SerializeField]
    private Image whiteImage;

    [SerializeField]
    private float flashTime;

    [SerializeField]
    private float minAlphaFlash = 0;
    [SerializeField]
    private float maxAlphaFlash = 1;

    private Color color;

    private Color minColor = new Color(255,255,255, 0);
    private Color maxColor = new Color(255, 255, 255, 1);

    private Coroutine whiteFlashCoroutine;


    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        StartWhiteFlash();
    //    }
    //}



    // Used for testing
    public void StartWhiteFlash() {
        SetColors(minAlphaFlash, maxAlphaFlash);
        whiteFlashCoroutine = StartCoroutine(WhiteFlash(minAlphaFlash, maxAlphaFlash));
    }

    private void SetColors(float minAlphaTemp, float maxAlphaTemp)
    {
        minColor = whiteImage.color;
        maxColor = whiteImage.color;
        minColor.a = minAlphaTemp;
        maxColor.a = maxAlphaTemp;

        color = minColor;
    }

    private IEnumerator WhiteFlash(float minAlphaTemp, float maxAlphaTemp)
    {

        whiteImage.color = maxColor;

        yield return new WaitForSeconds(flashTime);

        whiteImage.color = minColor;

        yield return null;
    }




    private bool stopScript = false;

    [SerializeField]
    private float fadeStep = 0.01f;
    [SerializeField]
    private float fadeRate = 0.01f;

    [SerializeField]
    private float minAlphaFade = 0;
    [SerializeField]
    private float maxAlphaFade = 1;


    public void StartWhiteFade() {
        stopScript = false;
        SetColors(minAlphaFade, maxAlphaFade);
        InvokeRepeating("WhiteFade", 0.1f, fadeRate);
    }

    private bool fadeUp = true;
    private Coroutine scriptStopperCoroutine;

    private void WhiteFade() {
        if (!stopScript) {
            if (whiteImage.color.a <= maxAlphaFade && fadeUp)
            {
                color.a += fadeStep;
                whiteImage.color = color;
            }
            if (whiteImage.color.a >= maxAlphaFade)
            {
                fadeUp = false;
                scriptStopperCoroutine = StartCoroutine("ScriptStopper");
            }

            if (whiteImage.color.a >= minAlphaFade && !fadeUp)
            {
                color.a -= fadeStep;
                whiteImage.color = color;
            }
            if (whiteImage.color.a <= minAlphaFade)
            {
                fadeUp = true;
                stopScript = true;
            }
        }
    }

    [SerializeField]
    private float whiteStayTime = 0;
    private IEnumerator ScriptStopper() {
        stopScript = true;
        yield return new WaitForSeconds(whiteStayTime);
        stopScript = false;
        yield return null;
    }

}
