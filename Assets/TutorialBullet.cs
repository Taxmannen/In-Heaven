using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBullet : MonoBehaviour
{

    private void OnDestroy()
    {
        TutorialParryBulletSpeedBox.instance.NullCoroutinesStatic();       
    }
}
