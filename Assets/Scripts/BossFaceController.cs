using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFaceController : MonoBehaviour
{
    public Material[] screenMats;
    public Material[] staticMats;

    Renderer newFace;
    Renderer rendStatic;

    [Range(0.01f, 0.2f)]
    public float statucDuration;

    private bool antWarsRuler=true;

    private float timer=0;
    int screenController;
        
    void Start()
    {
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

    private void Update()
    {
        //StartCoroutine(warOfTheAnts(0));
    }

    public IEnumerator warOfTheAnts(int screenController)
    {
        if (antWarsRuler == true) {
            antWarsRuler = false;
            rendStatic.sharedMaterial = staticMats[0];
            yield return new WaitForSeconds(statucDuration);


            rendStatic.sharedMaterial = staticMats[1];
            yield return new WaitForSeconds(statucDuration);


            rendStatic.sharedMaterial = staticMats[2];
            yield return new WaitForSeconds(statucDuration);


            rendStatic.sharedMaterial = staticMats[3];
            yield return new WaitForSeconds(statucDuration);

            antWarsRuler = true;
        }
        newFace.sharedMaterial = screenMats[screenController];
        yield break;
    }
}
