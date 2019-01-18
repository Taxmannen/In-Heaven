using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    //Design
    [SerializeField] [Range(0, 100000)] private int maxHP = 1000;

    //Debug
    [SerializeField] [ReadOnly] private int hP;
    [SerializeField] [ReadOnly] Global.BossState bossState = Global.BossState.Default;

    private void Start()
    {
        hP = maxHP;

        InterfaceController.instance.UpdateBossHP(hP, maxHP);
        InterfaceController.instance.UpdateBossState(bossState);
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Bullet")
        {

            if (bossState == Global.BossState.Default)
            {
                if (hP - other.GetComponent<Bullet>().GetDamage() <= 0)
                {
                    Die();
                    AudioController.instance.BossHit();
                }

                else
                {
                    hP -= other.GetComponent<Bullet>().GetDamage();
                    InterfaceController.instance.UpdateBossHP(hP, maxHP);
                    AudioController.instance.BossHit();
                }
            }

        }
        
    }

    private void Die()
    {
        hP = 0;
        InterfaceController.instance.UpdateBossHP(hP, maxHP);
        //bossState = PHASE
        //gameState = COMPLETE (if last phase died)
        InterfaceController.instance.GameOver(); //winScreen
    }

    //Inspector
    [ExecuteInEditMode]
    void OnValidate()
    {
        hP = maxHP;
    }

}
