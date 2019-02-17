using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticScreen : MonoBehaviour
{

    public int staticDuration;

    public Material[] staticMats;
    Renderer rendStatic;

    void Start()
    {
        rendStatic = GetComponent<Renderer>();
        rendStatic.enabled = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void staticmethod()
    {
        StartCoroutine(staticScreen());
    }

    IEnumerator staticScreen()
    {
        for (int i = 0; i < staticDuration; i++)
        {
            rendStatic.sharedMaterial = staticMats[0];
            yield return new WaitForEndOfFrame();
            rendStatic.sharedMaterial = staticMats[1];
            yield return new WaitForEndOfFrame();
            rendStatic.sharedMaterial = staticMats[2];
            yield return new WaitForEndOfFrame();
            rendStatic.sharedMaterial = staticMats[3];
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }
}
