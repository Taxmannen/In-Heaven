﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made by: Anton Lindkvist
/// </summary>
public class PatternImporter : MonoBehaviour
{
    [Header("Requires texture Import Setting: 'Read/Write Enabled'!")]
    public Texture2D importedTexture;
    public List<PatternStruct> patternList = new List<PatternStruct>();

    [SerializeField]
    [Header("Import Settings")]
    [Range(0.25f, 3.0f)]
    public float xOffset;
    [Range(0.25f, 3.0f)]
    public float yOffset;
    [Range(-2.0f, 2.0f)]
    [Tooltip("Make negative to reverse pattern 'animation'.")]
    public int timeDelayMultiplier;


    public void GeneratePattern()
    {
        patternList = new List<PatternStruct>();
        var pix = importedTexture.GetPixels32();
        float textureWidth = importedTexture.width;
        /*
        for (int i = 0; i < pix.Length; i++)
        {
            if (pix[i].r < 129 && pix[i].g < 75)
            {
                var hej = (i / textureWidth) - (textureWidth / 2);
                var toInt = Mathf.CeilToInt(hej);//(int)(hej + 0.5f);

                //patternList.Add(new PatternStruct(i % textureWidth - (textureWidth / 2), i / textureWidth - (textureWidth / 2), pix[i].r, false));
                // OBS! Fulkod för att fixa en weird offset av Y-axeln på importerade mönster. -Anton
                //Debug.Log("x: " + (i % textureWidth - (textureWidth / 2)) + " y: " + hej);
                //patternList.Add(new PatternStruct(i % textureWidth - (textureWidth / 2), hej, pix[i].r, false));
            }
        }
        */
        /*
        for (int i = 0; i < 32; i++)
        {
            for (int j = 0; j < 32; j++)
            {
                if (pix[i*j + j].r < 129)
                {
                    float x = i - (textureWidth / 2);
                    float y = (j*i%32) - (textureWidth / 2);
                    patternList.Add(new PatternStruct(x, y, pix[i*j+j].r, false));
                }
                    
            }
        }
        */

        for (int i = 0; i < pix.Length; i++)
        {
            if (pix[i].r < 129 && pix[i].g < 75)
            {
                var hej = (i / textureWidth + (1 - (i % textureWidth * (1 / textureWidth))) - (textureWidth / 2));
                //var toInt = (int)(hej + 0.5f);

                //patternList.Add(new PatternStruct(i % textureWidth - (textureWidth / 2), i / textureWidth - (textureWidth / 2), pix[i].r, false));
                // OBS! Fulkod för att fixa en weird offset av Y-axeln på importerade mönster. -Anton
                patternList.Add(new PatternStruct(i % textureWidth - (textureWidth / 2), hej, pix[i].r, false));
            }
        }

        Debug.Log(importedTexture.width);
        Debug.Log(patternList.Count);
        Debug.Log(pix.Length);
        Debug.Log("Thing() executed");
    }

    private void OnDrawGizmosSelected()
    {
        foreach (var patternStruct in patternList)
        {
            float parryableColor = 0;

            if (patternStruct.parryable == true)
            {
                parryableColor = 1;
            }

            float timeDelayColor = (255 - patternStruct.timeDelay) / 255f;

            //TODO: Check box "Reverse animation" to then reverse all numbers from 128 to 255
            Gizmos.color = new Color(1, 0, parryableColor, timeDelayColor);
            Gizmos.DrawSphere(new Vector3(transform.position.x + (patternStruct.x * xOffset), transform.position.y + (patternStruct.y * yOffset), 0), 0.5f);
        }
    }

}

[System.Serializable]
public struct PatternStruct
{
    [Range(-20, 20)]
    public float x; 
    [Range(-20, 20)]
    public float y;
    [Range(0,128)]
    public int timeDelay;
    public bool parryable;

    public PatternStruct(float x, float y, int timeDelay, bool parryable)
    {
        this.x = x;
        this.y = y;
        this.timeDelay = timeDelay;
        this.parryable = parryable;
    }
}
