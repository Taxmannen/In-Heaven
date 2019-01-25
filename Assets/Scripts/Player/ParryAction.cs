using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryAction : MonoBehaviour
{
    
    [SerializeField] private BoxCollider parrybox;
    [SerializeField] [Range(0, 100000)] private float playerParryBulletDamage = 10f;
    [SerializeField] [Range(0, 1000)] private float playerParryBulletSpeed = 100f;
    [SerializeField] [Range(0, 10)] private float parryDuration = 1f;
    [SerializeField] [Range(0, 10)] private float parryCooldown = 1f;

    private Coroutine parryCoroutine = null;
    private Coroutine parryCooldownCoroutine = null;
    private float parryCoroutineCounter;

    [Header("SLEDGEHAMMER")]
    [SerializeField] [Range(0, 10)] private float sledgeDuration = 3f;
    // Start is called before the first frame update
    void Start()
    {
        parrybox = GetComponentInChildren<Parrybox>().GetComponent<BoxCollider>();
    }

    /// <summary>
    /// Parry WIP
    /// </summary>
    public void Parry()
    {

        if (parryCooldownCoroutine == null)
        {
            parryCoroutine = StartCoroutine(ParryCoroutine());
            parryCooldownCoroutine = StartCoroutine(ParryCooldownCoroutine());
        }

    }

    private IEnumerator ParryCoroutine()
    {

        parrybox.enabled = true;
        //playerState = Global.PlayerState.Invincible;    

        for (parryCoroutineCounter = sledgeDuration; parryCoroutineCounter > 0; parryCoroutineCounter -= Time.deltaTime)
        {

            if (parryCoroutineCounter <= (sledgeDuration - parryDuration))
            {
                parrybox.enabled = false;
                //playerState = Global.PlayerState.Default;
                break;
            }

            yield return null;

        }

        yield return new WaitForSeconds(parryCoroutineCounter);
        parryCoroutine = null;
        yield break;

    }

    private IEnumerator ParryCooldownCoroutine()
    {
        yield return new WaitUntil(() => parryCoroutine == null);
        yield return new WaitForSeconds(parryCooldown);
        parryCooldownCoroutine = null;
        yield break;
    }
}
