using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Debugger : MonoBehaviour
{
    public static Debugger instance;
    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            enabled = true;
        }
    }

    private void Update()
    {
        //Debug.Log("I am in SCENE: " + SceneManager.GetActiveScene().name);
    }
}
