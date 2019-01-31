using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{

    public static TutorialController instance;
    [SerializeField] TutorialCannon tutorialCannon;
    private SuperChargeResource superChargeResource;
    private Coroutine checkSuperChargeRoutine;
    [SerializeField] private TutorialParryBulletSpeedBox speedBox;

    [SerializeField] private Canvas moveCanvas;
    [SerializeField] private Canvas jumpCanvas;
    [SerializeField] private Canvas dashCanvas;
    [SerializeField] private Canvas shootCanvas;
    [SerializeField] private Canvas parryCanvas;
    [SerializeField] private Canvas superChargeCanvas;


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
        moveCanvas.enabled = true;
        tutorialCannon.gameObject.SetActive(false);
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
            moveCanvas.enabled = false;
            jumpCanvas.enabled = true;
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
            jumpCanvas.enabled = false;
            dashCanvas.enabled = true;
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
            dashCanvas.enabled = false;
            shootCanvas.enabled = true;
            state = TutorialState.Shoot;            
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
            shootCanvas.enabled = false;
            parryCanvas.enabled = true;
            state = TutorialState.Parry;
            tutorialCannon.gameObject.SetActive(true);
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
            parryCanvas.enabled = false;
            superChargeCanvas.enabled = true;
            state = TutorialState.SuperCharge;
            speedBox.StopCoroutines();
            checkSuperChargeRoutine = StartCoroutine(CheckSuperchargeRoutine());
            
        }
        else
        {
            TutorialParryBulletSpeedBox.instance.StartShoot();
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
                superChargeCanvas.enabled = false;
                yield break;
            }
            yield return null;
        }
    }
}
