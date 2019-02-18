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

    private bool s1 = true;
    private bool s2 = true;
    private bool s3 = true;
    private bool s4 = true;
    private bool s5 = true;
    private bool s6 = true;
    private bool s7 = true;
    private bool s8 = true;

    void Start()
    {
      //screenMaterial1 = GetComponent<Renderer>().material;


        newFace = GetComponent<Renderer>();
        newFace.enabled = true;
        newFace.sharedMaterial = screenMats[0];

        rendStatic = GetComponent<Renderer>();
        rendStatic.enabled = true;
    }

    void updateScreen(int screenController)
    {
        StartCoroutine(staticScreen(screenController));
    }



    IEnumerator staticScreen(int screenController)
    {
        for (int i = 0; i < staticDuration; i++)
       // while(true)
        {
            rendStatic.sharedMaterial = staticMats[0];
            yield return new WaitForSeconds(0.1f);
           

            rendStatic.sharedMaterial = staticMats[1];
            yield return new WaitForSeconds(0.1f);

            
            rendStatic.sharedMaterial = staticMats[2];
            yield return new WaitForSeconds(0.1f);
           

            rendStatic.sharedMaterial = staticMats[3];
            yield return new WaitForSeconds(0.1f);

            //staticMats[0]= null;
            //staticMats[1]= null;
            //staticMats[2]= null;
            //staticMats[3]= null;
         
            // yield return null;
        }

        
       
        
        

        newFace.sharedMaterial = screenMats[screenController];
        yield break;
    }




    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer>=4 && timer<=8&&s1==true)
        {
            //newFace.sharedMaterial = screenMats[1];
            updateScreen(1);
            s1 = false;
        }
        else if (timer >=8 && timer <=12 && s2==true)
        {
            s2 = false;
            //newFace.sharedMaterial = screenMats[2];
            updateScreen(2);

        }
        else if (timer >= 12 && timer <= 16 && s2 == true)
        {
            s3 = false;
            //newFace.sharedMaterial = screenMats[3];
            updateScreen(3);
        }
        else if (timer >= 16 && timer <= 20 && s2 == true)
        {
            s4 = false;
            // newFace.sharedMaterial = screenMats[4];
            updateScreen(4);
        }
        else if (timer >= 20 && timer <= 24 && s2 == true)
        {
            s5 = false;
            //newFace.sharedMaterial = screenMats[5];
            updateScreen(5);
        }
        else if (timer >= 24 && timer <= 28 && s2 == true)
        {
            s6 = false;
            //newFace.sharedMaterial = screenMats[6];
            updateScreen(6);
        }
        else if (timer >= 28 && s2 == true)
        {
            s7 = false;
            //newFace.sharedMaterial = screenMats[7];
            updateScreen(7);
        }

    }
}
