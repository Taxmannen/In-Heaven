using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour
{

    public static Statistics instance;

    //Player
   [SerializeField] [ReadOnly] internal int numberOfDoubleJumps;
   [SerializeField] [ReadOnly] internal int numberOfJumps;
   [SerializeField] [ReadOnly] internal int numberOfDashes;
   [SerializeField] [ReadOnly] internal int numberOfSuccessfulParrys;
   [SerializeField] [ReadOnly] internal int numberOfParrys;
   [SerializeField] [ReadOnly] internal int numberOfBulletsFired;
   [SerializeField] [ReadOnly] internal int numberOfSuperChargesUnleashed;
   [SerializeField] [ReadOnly] internal int numberOfDeaths;
   [SerializeField] [ReadOnly] internal int numberOfWeakpointHits;
   [SerializeField] [ReadOnly] internal int numberOfBossHits;









    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            enabled = true;
        }
    }





}
