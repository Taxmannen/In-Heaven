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
        explosion.GetComponent<ParticleSystem>().Play();
        InterfaceController.instance.HidePlayerHealth();
        InterfaceController.instance.HideBossHPBar();
        whiteFade.StartWhiteFade();

        yield return new WaitForSeconds(2);
        boss.Die();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        executeRoutine = null;

        yield break;
    }
}