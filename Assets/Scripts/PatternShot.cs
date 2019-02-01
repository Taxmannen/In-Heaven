using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternShot : MonoBehaviour
{
    public string pattern1 = "PatternShot1";
    string txtContents;
    string[] patternString;

    private BossController bossController;

    public GameObject PatternShotBullet;

    private Coroutine bossShootPatternShot;

    public void Start()
    {
        bossController = GameObject.Find("Boss").GetComponent<BossController>();
        TextAsset txtAssets = (TextAsset)Resources.Load(pattern1);
        txtContents = txtAssets.text;
    }

   
    public void PatternGenerateArray()
    {
        //patternString = (pattern1.text.Split('\n'));
    }
    
    public void PatternShotShoot(Vector3 PatternShotSpawnPosition)
    {
        if (bossShootPatternShot == null)
        {
            bossShootPatternShot = StartCoroutine(PatternShotCorutine(PatternShotSpawnPosition));
        }
    }

    private IEnumerator PatternShotCorutine(Vector3 PatternShotSpawnPosition)
    {

        //var textFile = Resources.Load<TextAsset>("TextAssets/PatternShots1");

        float[] patternLength = new float[txtContents.Length];

        float patternSpread = float.Parse(patternString[0]);

        float patternShotTargetX = float.Parse(patternString[1]);
        float patternShotTargetY = float.Parse(patternString[2]);

        Vector3 PatternShotStartingPoint = bossController.RandomSpawnPoint();
        
        for (int i = 3; i < patternLength.Length; i++)
        {

            Vector3 PatternShotTarget = new Vector3(patternShotTargetX, patternShotTargetY, 0);

             Vector3 direction = PatternShotTarget - PatternShotStartingPoint;

            if (float.Parse(patternString[i]) == 0)
            {
                patternShotTargetX = patternShotTargetX + patternSpread;
            }

            if(float.Parse(patternString[i]) == 1)
            {
                GameObject PatternShotBulletClone = Instantiate(PatternShotBullet, PatternShotStartingPoint, Quaternion.identity);

                patternShotTargetX = patternShotTargetX + patternSpread;
            }

            if(float.Parse(patternString[i]) == 2)
            {
                patternShotTargetY = patternShotTargetY + patternSpread;
            }            
        }

        yield break;
    }

}
