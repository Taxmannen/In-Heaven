using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenStartUp : MonoBehaviour
{
    public Material[] startMat;

    public GameObject bossHead;
    private BossFaceController bossFaceController;
   
    float lerpDuration = 2f;

    Renderer render;

    void Start()
    {
        bossHead = GameObject.Find("Head");
        bossFaceController = (BossFaceController)bossHead.GetComponent(typeof(BossFaceController));

        render = GetComponent<Renderer>();
        render.enabled = true;

        render.material = startMat[0];
    }

    public IEnumerator startUp()
    {
        float timer = 0f;

        yield return new WaitForSeconds(0.5f);

        while (timer<lerpDuration)
        {
            timer += Time.deltaTime;


            float lerpTime = Mathf.PingPong(Time.time, lerpDuration) / lerpDuration;
            render.material.Lerp(startMat[0], startMat[1], lerpTime);

            yield return null; 
        }
        bossFaceController.StartCoroutine(bossFaceController.warOfTheAnts(1));
    }

}
