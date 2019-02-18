using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateIncrease : MonoBehaviour
{
    ShootAction shootAction;
    ParryAction parryAction;
    Parrybox parrybox;

    [Range(600f, 90000000f)] public int boostedDamage;
    [Range(1f, 10f)] public float dmgBoostDuration;

    private float timer;

    private int baseDamage;

    private bool dmgBoostReset=false;
  
    void Start()
    {
        shootAction = GameObject.Find("Player").GetComponent<ShootAction>();
        parryAction = GameObject.Find("Player").GetComponent<ParryAction>();

        baseDamage = shootAction.playerBulletDamage;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boss Parryable Bullet")
        {
            Debug.Log("Timer:" + timer);
            dmgBoostReset = true;
            shootAction.playerBulletDamage=boostedDamage;
            timer = 0;

            
        }
    }

    void Update()
    {
        Debug.Log("Dmg: "+shootAction.playerBulletDamage);
        

        timer += Time.deltaTime;

        if (dmgBoostReset == true && timer >= dmgBoostDuration)
        {
            shootAction.playerBulletDamage = baseDamage;
            Debug.Log("Damage Boost Reset");
        }

    }
}
