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



    [Header("Time Stats")]
    [SerializeField] [ReadOnly] internal float timeTookToCompleteTutorial;
    [SerializeField] [ReadOnly] internal float timeTookToCompleteMovementTutorial;
    [SerializeField] [ReadOnly] internal float timeTookToCompleteJumpTutorial;
    [SerializeField] [ReadOnly] internal float timeTookToCompleteDashTutorial;
    [SerializeField] [ReadOnly] internal float timeTookToCompleteShootTutorial;
    [SerializeField] [ReadOnly] internal float timeTookToCompleteParryTutorial;
    [SerializeField] [ReadOnly] internal float timeTookToCompleteSuperCharageTutorial;


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

    public void UpdateTimeCompleteMovement(float time)
    {
        timeTookToCompleteMovementTutorial = time - timeTookToCompleteTutorial;
    }
    public void UpdateTimeCompleteJump(float time)
    {
        timeTookToCompleteJumpTutorial = time - timeTookToCompleteMovementTutorial;
    }
    public void UpdateTimeCompleteDash(float time)
    {
        timeTookToCompleteDashTutorial = time - timeTookToCompleteJumpTutorial;
    }
    public void UpdateTimeCompleteShoot(float time)
    {
        timeTookToCompleteShootTutorial = time - timeTookToCompleteDashTutorial;
    }
    public void UpdateTimeCompleteParry(float time)
    {
        timeTookToCompleteParryTutorial = time - timeTookToCompleteDashTutorial;
    }
    public void UpdateTimeCompleteSuperCharge(float time)
    {
        timeTookToCompleteShootTutorial = time - timeTookToCompleteParryTutorial;
    }
    public void UpdateTimeCompleteTutorial(float time)
    {
        timeTookToCompleteTutorial = time;
    }






}
