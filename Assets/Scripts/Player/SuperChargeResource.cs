using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerController))]
public class SuperChargeResource : MonoBehaviour
{
    private PlayerController player;

    [SerializeField] [Range(0, 100)] internal float superChargeMax = 1f;
    [SerializeField] [Range(0, 100)] private float superChargeIncrease = 1f;
    private Coroutine superChargeCoroutine = null;

    [Header("DEBUG")]
    [SerializeField] [ReadOnly] internal float superCharge;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        
    }

    public void SuperCharge()
    {

        if (superCharge == superChargeMax)
        {

            if (superChargeCoroutine == null)
            {
                superChargeCoroutine = StartCoroutine(SuperChargeCoroutine());
            }

        }

    }

    private IEnumerator SuperChargeCoroutine()
    {
        superCharge = 0;
        InterfaceController.instance.UpdateSuperChargeSlider(0);
        player.shootAction.playerBulletsPerSecond *= 2f; //Hardcoded
        yield return new WaitForSeconds(1f); //Hardcoded
        player.shootAction.BulletPerSecondReset();
        superChargeCoroutine = null;
        yield break;
    }

    public void IncreaseSuperCharge()
    {

        if (superCharge + superChargeIncrease >= superChargeMax)
        {
            superCharge = superChargeMax;
        }

        else
        {
            superCharge += superChargeIncrease;
        }

        InterfaceController.instance.UpdateSuperChargeSlider(superCharge);

    }

}
