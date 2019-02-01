using System.Collections;
using UnityEngine;

/// <summary>
/// Made by: Filip Nilsson, planned by: Filip Nilsson + Jesper Uddefors
/// </summary>
public class BossMovement : MonoBehaviour
{

    //Protected Static

    protected static Coroutine executeRoutine;



    //Main

    public void StartExecute(Boss boss)
    {

        if (executeRoutine == null)
        {
            executeRoutine = StartCoroutine(Execute(boss));
        }

    }

    protected virtual IEnumerator Execute(Boss boss)
    {
        executeRoutine = null;
        yield break;
    }

}
