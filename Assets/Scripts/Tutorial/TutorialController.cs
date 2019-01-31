using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{

    public static TutorialController instance;
    [SerializeField] TutorialCannon tutorialCannon;
    private SuperChargeResource superChargeResource;
    private Coroutine checkSuperChargeRoutine;

    public int movementTargetsDestroyed;
    public int jumpingTargetsDestroyed;
    public int dashTargetsDestroyed;
    public int shootTargetsDestroyed;
    public int parryTargetsDestroyed;

    public Transform movementDummyParent;
    public Transform jumpDummyParent;
    public Transform dashDummyParent;
    public Transform shootDummyParent;
    public Transform parryDummyParent;

    public Transform tutorialBulletOrigin;
    public Transform tutorialBulletTarget;
    public GameObject tutorialBulletPrefab;
    public GameObject bulletClone;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        superChargeResource = FindObjectOfType<SuperChargeResource>();
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
            tutorialCannon.SpawnBullet();
            //parryDummyParent.gameObject.SetActive(true);
            shootDummyParent.gameObject.SetActive(false);
            ResetShootDummies();
        }
    }
    public void CheckParryGoal()
    {
        if (superChargeResource.superCharge == superChargeResource.superChargeMax)
        {

            state = TutorialState.SuperCharge;
            checkSuperChargeRoutine = StartCoroutine(CheckSuperchargeRoutine());
        }
        else
        {
            tutorialCannon.SpawnBullet();
        }
    }
    public void CheckSuperChargeGoal()
    {
        if (checkSuperChargeRoutine == null)
        {
            checkSuperChargeRoutine = StartCoroutine(CheckSuperchargeRoutine());

        }
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
    public void ResetParryDummy()
    {
        tutorialCannon.SpawnBullet();
    }
    private IEnumerator CheckSuperchargeRoutine()
    {
        while (true)
        {
            if (Statistics.instance.numberOfSuperChargesUnleashed == 1)
            {
                Debug.Log("TUTORIAL FINISHED");
                yield break;
            }
            yield return null;
        }
    }
}
