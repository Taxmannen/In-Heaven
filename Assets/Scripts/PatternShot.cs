using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternShot : MonoBehaviour
{
    public TextAsset textFile;
    string[] patternString;

  

    public void PatternGenerateArray()
    {
        patternString = (textFile.text.Split('\n'));
    }

    private IEnumerator PatternShotCorutine(Vector3 PatternShotSpawnPosition, Vector3 PatternShotTargetPoint)
    {
       //string[] textFile = Assets.Resources.TextAssets.PatternShot1.txt;
            var textFile = Resources.Load<TextAsset>("TextAssets/PatternShots1");

        float[] patternLength = new float[patternString.Length];

        float patternSpread = float.Parse(patternString[0]);

        float patternShotTargetX = float.Parse(patternString[1]);
        float patternShotTargetY = float.Parse(patternString[2]);

        Vector3 PatternStartingPoint = new Vector3();
        Vector3 PatternShotTarget = new Vector3(patternShotTargetX, patternShotTargetY, 0);


        for (int i = 3; i < patternLength.Length; i++)
        {

            Debug.Log(patternString[i]);

            if (float.Parse(patternString[i]) == 0)
            {

            }
            if(float.Parse(patternString[i]) == 1)
            {

            }


        }

        yield break;
    }

}
