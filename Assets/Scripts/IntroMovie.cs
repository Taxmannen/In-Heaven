using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


//Code by Tåqvist

public class IntroMovie : MonoBehaviour
{
    [SerializeField]
    private Text text;

    [SerializeField]
    private UIWhiteFadeAndFlash fade;

    UnityEngine.Video.VideoPlayer vp;

    private float frameTemp;
    private float frameCountTemp;

    private void Start()
    {
        vp = GetComponent<UnityEngine.Video.VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        frameTemp = vp.frame;
        frameCountTemp = vp.frameCount;
        //Debug.Log(frameTemp + 10 + " " + frameCountTemp);

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1)) {
            CancelInvoke("fadeInText");
            text.color = new Color(1, 1, 1, 0);
            InvokeRepeating("fadeInText", 0.1f, 0.1f);

        }

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Return)) {
            vp.Pause();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        //Fulfix. Går inte upp till summan som vi vill ha annars..
        if (frameCountTemp == frameTemp + 600)
        {
            fade.StartWhiteFade();
        }

        if (frameCountTemp == frameTemp + 500)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    private void fadeInText() {
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + 0.15f);
    }
}
