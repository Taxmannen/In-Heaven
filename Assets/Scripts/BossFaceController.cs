﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFaceController : MonoBehaviour
{
    public Material[] screenMats;
    Renderer newFace;

    private float timer=0;

    void Start()
    {
      //screenMaterial1 = GetComponent<Renderer>().material;


        newFace = GetComponent<Renderer>();
        newFace.enabled = true;
        newFace.sharedMaterial = screenMats[0];
    }

   
    void Update()
    {
        timer += Time.deltaTime;
        
        if (timer>=4 && timer<=8)
        {
            newFace.sharedMaterial = screenMats[1];
        }
        else if (timer >=8 && timer <=12)
        {
            newFace.sharedMaterial = screenMats[2];
        }
        else if (timer >= 12 && timer <= 16)
        {
            newFace.sharedMaterial = screenMats[3];
        }
        else if (timer >= 16 && timer <= 20)
        {
            newFace.sharedMaterial = screenMats[4];
        }
        else if (timer >= 20 && timer <= 24)
        {
            newFace.sharedMaterial = screenMats[5];
        }
        else if (timer >= 24 && timer <= 28)
        {
            newFace.sharedMaterial = screenMats[6];
        }
        else if (timer >= 28)
        {
            newFace.sharedMaterial = screenMats[7];
        }

    }
}