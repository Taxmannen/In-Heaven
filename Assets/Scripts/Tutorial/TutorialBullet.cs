using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBullet : Bullet
{
    
    private void OnDestroy()
    {
        if(TutorialParryBulletSpeedBox.instance != null)
        {
            //TutorialParryBulletSpeedBox.instance.StartShoot();   
        }
    }
}
