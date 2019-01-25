using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerController))]
public class InvincibleEffect : MonoBehaviour
{
    private PlayerController player;
    internal Coroutine invincibleCoroutine = null;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    /// <summary>
    /// Applies the Invincible PlayerState to the object for the duration sent as a parameter.
    /// </summary>
    /// <param name="duration"></param>
    internal void Invincible(float duration)
    {

        if (invincibleCoroutine != null)
        {
            StopCoroutine(invincibleCoroutine);
        }

        invincibleCoroutine = StartCoroutine(InvincibleCoroutine(duration));

    }

    private IEnumerator InvincibleCoroutine(float duration)
    {
        player.playerState = Global.PlayerState.Invincible;
        InterfaceController.instance.UpdatePlayerState(player.playerState); //Debug
        yield return new WaitForSeconds(duration);
        player.playerState = Global.PlayerState.Default;
        InterfaceController.instance.UpdatePlayerState(player.playerState); //Debug
        yield break;
    }

    public void MyStopCoroutine()
    {
        if (invincibleCoroutine != null)
        {
            StopCoroutine(invincibleCoroutine);
            invincibleCoroutine = null;
        }
    }
       
}
