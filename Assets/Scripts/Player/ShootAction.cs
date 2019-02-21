using System.Collections;
using UnityEngine;

/// <summary>
/// Made By: Jesper Uddefors and Filip Nilsson, Edited By: Daniel Nordahl
/// </summary>
[RequireComponent(typeof(PlayerController))]
public class ShootAction : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private Transform shootFrom;
    internal Coroutine shootCoroutine = null;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject muzzleEfect;
    [SerializeField] private Transform bullets;
    [SerializeField] private Transform VFX;

    [SerializeField] [Range(0, 1000)] public int playerBulletDamage = 7;
    [SerializeField] [Range(1, 100)] private float basePlayerBulletsPerSecond = 10f; //Bullets per second during left mouse down
    [SerializeField] [Range(0, 1000)] private float playerBulletSpeed = 25f; //The speed of the bullets
    [SerializeField] [Range(0, 10)] private float playerBulletLifetime = 3f; //The duration the bullets last until they are destroyed, low number reduces potential lag

    [Header("Testing Settings")]
    [SerializeField] internal bool onlyShootOnGround;
    [SerializeField] internal bool onlyShootWhenStandingStill;
    [SerializeField] internal bool isMuzzleEffect;

    internal float playerBulletsPerSecond;

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
        if (AudioController.instance)
        {
            AudioController.instance.PlayerShoot();
        }
        if (Statistics.instance)
        {
            Statistics.instance.numberOfBulletsFired++;
        }

        GameObject bullet = ShootingHelper.Shoot(shootFrom.position, point, PlayerBulletPool.current.GetPlayerBullets(), playerBulletSpeed, bullets, 3);
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        bulletScript.SetDamage(playerBulletDamage);
        bulletScript.SetPoint(point);
        bulletScript.SetFromPlayer(true);

        if (isMuzzleEffect)
        {
            GameObject muzzle = Instantiate(muzzleEfect, shootFrom.position, transform.rotation, VFX);
            Destroy(muzzle, 1);
        }

        yield return new WaitForSeconds(1 / playerBulletsPerSecond);
        shootCoroutine = null;
        yield break;
    }

    public void ShootReverb()
    {
        //AudioController.instance.PlayerGunReverb();
    }

    public void DestroyAllBullets()
    {
        /*foreach (Transform bullet in bullets)
        {
            if (bullet != null) bullet.GetComponent<Bullet>().ResetBullet();
            else Debug.Log(bullet == null);
        }*/
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