using System.Collections;
using UnityEngine;

/// <summary>
/// Made By: Jesper Uddefors and Filip Nilsson
/// </summary>
[RequireComponent(typeof(PlayerController))]
public class InvincibleEffect : MonoBehaviour
{
    private PlayerController player;
    private ShaderManager shaderManager;
    internal Coroutine invincibleCoroutine = null;

    void Start()
    {
        player = GetComponent<PlayerController>();
        shaderManager = GetComponent<ShaderManager>();
    }

    /// <summary>
    /// Applies the Invincible PlayerState to the object for the duration sent as a parameter.
    /// </summary>
    /// <param name="duration"></param>
    internal void Invincible(float duration, bool isHit)
    {
        if (invincibleCoroutine != null)
        {
            StopCoroutine(invincibleCoroutine);
        }

        invincibleCoroutine = StartCoroutine(InvincibleCoroutine(duration, isHit));
    }

    private IEnumerator InvincibleCoroutine(float duration, bool isHit)
    {
        player.playerState = Global.PlayerState.Invincible;
        InterfaceController.instance.UpdatePlayerState(player.playerState); //Debug
        if (isHit) shaderManager.HitEffect(duration, true);
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