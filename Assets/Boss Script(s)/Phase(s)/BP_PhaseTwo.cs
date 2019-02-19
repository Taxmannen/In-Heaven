using System.Collections;
using UnityEngine;

/* Script Made by Daniel */
public class BP_PhaseTwo : BossPhase
{
    [SerializeField] private GameObject explosion;
    [SerializeField] private ParticleSystem core;
    [SerializeField] private Collider col;
    [SerializeField] private Renderer[] rend;

    protected override IEnumerator PhaseRoutine(Boss boss)
    {
        explosion.GetComponent<ParticleSystem>().Play();
        Destroy(explosion, 5);
        Invoke("EnableCore", 0.15f);
        boss.Die();
        yield break;
    }

    private void EnableCore()
    {
        for (int i = 0; i < rend.Length; i++)
        {
            rend[i].enabled = false;
            col.enabled = true;
            core.gameObject.SetActive(true);
            core.GetComponent<ParticleSystem>().Play();
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Sparks");
            foreach (GameObject g in gameObjects) Destroy(g);
        }
    }
}