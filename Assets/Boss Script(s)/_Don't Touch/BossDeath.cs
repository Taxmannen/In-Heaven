using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeath : MonoBehaviour
{
    private BossFaceController bossFaceController;
    private GameObject bossHead;

    protected Coroutine executeRoutine = null;

    private void Start()
    {
        bossHead = GameObject.Find("Head");
        bossFaceController = (BossFaceController)bossHead.GetComponent(typeof(BossFaceController));

        //Copy paste to relevent part of script, intended to display a death face for the boss
        //bossFaceController.StartCoroutine(bossFaceController.warOfTheAnts(1));
    }

    public void StartDeath(Boss boss)
    {        
        executeRoutine = StartCoroutine(Execute(boss));
    }

    protected virtual IEnumerator Execute(Boss boss)
    {
        executeRoutine = null;
        yield break;
    }

    public Coroutine GetExecuteRoutine()
    {
        return executeRoutine;
    }

}
