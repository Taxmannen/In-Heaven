using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    private Camera camera;
    private void Awake()
    {
        if(instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            enabled = true;
        }
    }
    private void Start()
    {
        camera = GetComponent<Camera>();
    }

    public void CameraShake()
    {
        iTween.PunchPosition(gameObject, Vector3.back, 0.5f);
    }
}
