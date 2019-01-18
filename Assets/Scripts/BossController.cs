using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{

    [SerializeField] Global.BossState bossState = Global.BossState.Default;

    [SerializeField] private int maxHP = 1000;

    [SerializeField] private int hP;

    private void Start()
    {
        hP = maxHP;
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
                }

                else
                {
                    hP -= other.GetComponent<Bullet>().GetDamage();
                    InterfaceController.instance.UpdateBossHP(hP, maxHP);
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

}
