using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFaceController : MonoBehaviour
{
    public Material[] screenMats;
    public Material[] staticMats;

    Renderer newFace;
    Renderer rendStatic;

    public int staticDuration;
    private float timer=0;
    int screenController;



    void Start()
    {
      //screenMaterial1 = GetComponent<Renderer>().material;


        newFace = GetComponent<Renderer>();
        newFace.enabled = true;
        newFace.sharedMaterial = screenMats[0];

        rendStatic = GetComponent<Renderer>();
        rendStatic.enabled = true;
    }

    public void updateScreen(int screenController)
    {
        StartCoroutine(warOfTheAnts(screenController));
    }

    public IEnumerator ded()
    {
        for(int i=0; i<9001; i++)
        {
            rendStatic.sharedMaterial = staticMats[0];
            yield return new WaitForSeconds(0.1f);

            newFace.sharedMaterial = screenMats[3];
            yield return new WaitForSeconds(0.1f);

            rendStatic.sharedMaterial = staticMats[1];
            yield return new WaitForSeconds(0.1f);

            newFace.sharedMaterial = screenMats[3];
            yield return new WaitForSeconds(0.1f);

            rendStatic.sharedMaterial = staticMats[2];
            yield return new WaitForSeconds(0.1f);

            newFace.sharedMaterial = screenMats[3];
            yield return new WaitForSeconds(0.1f);

            rendStatic.sharedMaterial = staticMats[3];
            yield return new WaitForSeconds(0.1f);

            newFace.sharedMaterial = screenMats[3];
            yield return new WaitForSeconds(0.1f);
        }

        yield break;
    }

    private void Update()
    {
        //StartCoroutine(warOfTheAnts(0));
    }

    public IEnumerator warOfTheAnts(int staticScreenDuration)
    {
        for (int i = 0; i < staticDuration; i++)
      
        {
            rendStatic.sharedMaterial = staticMats[0];
            yield return new WaitForSeconds(0.1f);
           

            rendStatic.sharedMaterial = staticMats[1];
            yield return new WaitForSeconds(0.1f);

            
            rendStatic.sharedMaterial = staticMats[2];
            yield return new WaitForSeconds(0.1f);
           

            rendStatic.sharedMaterial = staticMats[3];
            yield return new WaitForSeconds(0.1f);

        }

        newFace.sharedMaterial = screenMats[screenController];
        yield break;
    }
}
