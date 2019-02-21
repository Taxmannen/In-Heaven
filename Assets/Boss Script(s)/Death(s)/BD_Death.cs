using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script by Tåqvist
public class BD_Death : BossDeath
{
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private UIWhiteFadeAndFlash whiteFade;

    protected override IEnumerator Execute(Boss boss)
    {
        //Destroy(boss.gameObject);
        AudioController.instance.BossDeath();
        Debug.Log("Dead");

        explosion.GetComponent<ParticleSystem>().Play();
        whiteFade.StartWhiteFade();

        yield return new WaitForSeconds(2);

        executeRoutine = null;

        yield break;
    }
}