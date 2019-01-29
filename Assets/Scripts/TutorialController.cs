using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{

    public static TutorialController instance;

    public int movementTargetsDestroyed;
    public int jumpingTargetsDestroyed;
    public int dashTargetsDestroyed;
    public int shootTargetsDestroyed;

    public Transform movementDummyParent;
    public Transform jumpDummyParent;
    public Transform dashDummyParent;
    public Transform shootDummyParent;

    private void Awake()
    {
        instance = this;
    }



    public enum TutorialState
    {
        Movement,
        Jump,
        Dash,
        Shoot,
        Parry,
        SuperCharge,
        Pause
        
    }
    internal static TutorialState state = TutorialState.Movement;
    
    public void CheckMovementGoal()
    {
        movementTargetsDestroyed++;
        if (movementTargetsDestroyed >= movementDummyParent.childCount)
        {
            state = TutorialState.Jump;

            jumpDummyParent.gameObject.SetActive(true);
            movementDummyParent.gameObject.SetActive(false);
            ResetMovementDummies();
        }
    }

    public void CheckJumpingGoal()
    {
        jumpingTargetsDestroyed++;
        if (jumpingTargetsDestroyed >= jumpDummyParent.childCount)
        {
            state = TutorialState.Dash;
            Debug.Log("Dash");
            dashDummyParent.gameObject.SetActive(true);
            jumpDummyParent.gameObject.SetActive(false);
            ResetJumpingDummies();

        }
    }
    public void CheckDashingGoal()
    {
        dashTargetsDestroyed++;
        if (dashTargetsDestroyed >= dashDummyParent.childCount)
        {
            state = TutorialState.Shoot;
            Debug.Log("Shoot");
            shootDummyParent.gameObject.SetActive(true);
            dashDummyParent.gameObject.SetActive(false);
            ResetDashDummies();

        }
    }
    public void CheckShootingGoal()
    {
        shootTargetsDestroyed++;
        if (shootTargetsDestroyed >= shootDummyParent.childCount)
        {
            state = TutorialState.Parry;
            //parryDummyParent.gameObject.SetActive(true);
            shootDummyParent.gameObject.SetActive(false);
        }
        ResetShootDummies();
    }

    public void ResetMovementDummies()
    {
        foreach (Transform movementDummy in movementDummyParent)
        {
            movementDummy.gameObject.SetActive(true);
        }
    }
    public void ResetJumpingDummies()
    {
        foreach (Transform jumpDummy in jumpDummyParent)
        {
            jumpDummy.gameObject.SetActive(true);
        }
    }

    public void ResetDashDummies()
    {
        foreach (Transform dashDummy in dashDummyParent)
        {
            dashDummy.gameObject.SetActive(true);
        }
    }

    public void ResetShootDummies()
    {
        foreach (Transform shootDummy in shootDummyParent)
        {
            shootDummy.gameObject.SetActive(true);
        }
    }

}
