using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Made By: Jesper Uddefors and Filip Nilsson
/// </summary>
[RequireComponent(typeof(PlayerController))]
public class ShootAction : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private Transform tf;
    internal Coroutine shootCoroutine = null;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bullets;


    [SerializeField] [Range(0, 1000)] private int playerBulletDamage = 7;
    [SerializeField] [Range(1, 100)] private float basePlayerBulletsPerSecond = 10f; //Bullets per second during left mouse down
    [SerializeField] [Range(0, 1000)] private float playerBulletSpeed = 25f; //The speed of the bullets
    [SerializeField] [Range(0, 10)] private float playerBulletLifetime = 3f; //The duration the bullets last until they are destroyed, low number reduces potential lag

    [Header("Testing Settings")]
    [SerializeField] internal bool onlyShootOnGround;
    [SerializeField] internal bool onlyShootWhenStandingStill;

    internal float playerBulletsPerSecond;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
        BulletPerSecondReset();
    }
    public void BulletPerSecondReset()
    {
        playerBulletsPerSecond = basePlayerBulletsPerSecond;
    }
    /// <summary>
    /// Shoots towards the direction the player is aiming towards.
    /// </summary>
    public void Shoot()
    {

        if ((onlyShootOnGround ? player.grounded : true) && (onlyShootWhenStandingStill? player.standingStill:true))
        {

            if (player.GetShootDuringDash())
            {

                if (shootCoroutine == null)
                {
                    shootCoroutine = StartCoroutine(ShootCoroutine(player.aim.aimPoint));
                }
            }

            else
            {
                if (player.GetDashVelocity() == 0)
                {
                    if (shootCoroutine == null)
                    {
                        shootCoroutine = StartCoroutine(ShootCoroutine(player.aim.aimPoint));
                    }
                }
            }
        }

    }

    private IEnumerator ShootCoroutine(Vector3 point)
    {
        if(AudioController.instance)
        {
            AudioController.instance.PlayerShoot();
        }
        if(Statistics.instance)
        {
            Statistics.instance.numberOfBulletsFired++;
        }

        GameObject bullet = ShootingHelper.Shoot(transform.position, point, bulletPrefab, playerBulletSpeed, bullets, 3);
        bullet.GetComponent<Bullet>().SetDamage(playerBulletDamage);
        yield return new WaitForSeconds(1 / playerBulletsPerSecond);
        shootCoroutine = null;
        yield break;

    }

    public void ShootReverb()
    {
        AudioController.instance.PlayerGunReverb();
        
    }
    public void DestroyAllBullets()
    {
        foreach (Transform bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
    }
    public void MyStopCorutine()
    {
        if (shootCoroutine != null)
        {
            StopCoroutine(shootCoroutine);
            shootCoroutine = null;
        }

    }

}
