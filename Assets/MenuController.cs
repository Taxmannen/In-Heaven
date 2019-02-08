using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void NextScene()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(SceneManager.GetActiveScene().buildIndex + 1));
    }

}
