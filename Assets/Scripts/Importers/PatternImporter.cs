using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternImporter : MonoBehaviour
{
    public Texture2D texture;
    public List<PatternStruct> patternList = new List<PatternStruct>();

    [SerializeField]
    [Header("Import Settings")]
    [Range(0, 10)]
    public int xOffset;
    [Range(0, 10)]
    public int yOffset;
    [Range(0, 10)]
    public int timeDelayMultiplier;


    public void Thing()
    {
        patternList = new List<PatternStruct>();
        var pix = texture.GetPixels32();
        int textureWidth = texture.width;

        Debug.Log(pix.Length);
        Debug.Log(pix[0]);

        for (int i = 0; i < pix.Length; i++)
        {
            if (pix[i].r < 1)
            {
                Debug.Log("Found a darker pixel at: " + i % textureWidth + ", " + i / textureWidth);
                patternList.Add(new PatternStruct(i % textureWidth, i / textureWidth, pix[i].r, false));
            }
        }
    }

    void SortList()
    {

    }

}

[System.Serializable]
public struct PatternStruct
{
    public int x;
    public int y;
    public float timeDelay;
    public bool parryable;

    public PatternStruct(int x, int y, float timeDelay, bool parryable)
    {
        this.x = x;
        this.y = y;
        this.timeDelay = timeDelay;
        this.parryable = parryable;
    }
}
