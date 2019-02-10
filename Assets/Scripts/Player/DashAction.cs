using System.Collections;
using UnityEngine;

/// <summary>
/// Made By: Jesper Uddefors and Filip Nilsson, Edited By: Daniel Nordahl
/// </summary>
[RequireComponent(typeof(PlayerController))]
public class DashAction : MonoBehaviour
{
    private PlayerController player;

    [SerializeField] [Range(0, 1000)] private float power = 50f; //The speed of the dash (affects dash distance)
    [SerializeField] [Range(0, 10)] private float duration = 0.1f; //The duration of the dash (affects dash distance)
    [SerializeField] [Range(0, 10)] private float invincibleDuration = 0.25f; //Duration of invincibility state after the start of a dash
    [SerializeField] [Range(0, 2)] private float verticalReduction = 0.5f; //Reduces the vertical velocity affecting the player during the dash
    [SerializeField] [Range(0, 100)] private float maxDashesInAir = 1; //Number of dashes possible in air
    [SerializeField] [Range(0, 10)] private float cooldown = 1f; //Cooldown for dash, ground and air
    [SerializeField] internal bool shootDuringDash = true; //Allow player to shoot during dash?

    internal Coroutine coroutine = null;
    internal Coroutine cooldownCorutine = null;

    internal float velocity;
    float dir; //Added so you always dash the direction you faced last
    private ShaderManager shaderManager;

    [Header("DEBUG")]
    [SerializeField] [ReadOnly] private float dashes = 0;
    [SerializeField] [ReadOnly] private float actualVerticalReductionDuringDash = 1;

    void Start()
    {
        player = GetComponent<PlayerController>();
        shaderManager = GetComponent<ShaderManager>();
    }

    private void Update()
    {
        if (player.GetHorizontalDirection() != 0) dir = player.GetHorizontalDirection();
    }

    public void Reset()
    {
        dashes = maxDashesInAir;
    }

    public void Dash()
    {
        if (player.grounded)
        {
            if (cooldownCorutine == null)
            {
                coroutine = StartCoroutine(DashCoroutine());
                cooldownCorutine = StartCoroutine(DashCooldownCoroutine());
            }
        }

        if (!player.grounded)
        {
            if (dashes > 0)
            {
                if (cooldownCorutine == null)
                {
                    dashes--;
                    coroutine = StartCoroutine(DashCoroutine());
                    cooldownCorutine = StartCoroutine(DashCooldownCoroutine());
                }
            }
        }

    }

    private IEnumerator DashCoroutine()
    {
        player.Invincible(invincibleDuration, false);
    
        velocity = dir * power;

        AudioController.instance.PlayerDash();
        Statistics.instance.numberOfDashes++;
        actualVerticalReductionDuringDash = verticalReduction;
        yield return new WaitForSeconds(duration);
        actualVerticalReductionDuringDash = 1f;
        velocity = 0f;
        coroutine = null;
        yield break;
    }

    private IEnumerator DashCooldownCoroutine()
    {
        yield return new WaitUntil(() => coroutine == null);
        yield return new WaitForSeconds(cooldown);
        shaderManager.DashEffect();
        cooldownCorutine = null;
        yield break;
    }

    public void MyStopCorutine()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
            coroutine = null;
        }
    }
}
