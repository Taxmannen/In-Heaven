using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerController))]
public class ShootAction : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] private Transform tf;
    internal Coroutine shootCoroutine = null;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bullets;


    [SerializeField] [Range(0, 100000)] private int playerBulletDamage = 7;
    [SerializeField] [Range(1, 100)] private float basePlayerBulletsPerSecond = 10f; //Bullets per second during left mouse down
    [SerializeField] [Range(0, 1000)] private float playerBulletSpeed = 25f; //The speed of the bullets
    [SerializeField] [Range(0, 10)] private float playerBulletLifetime = 3f; //The duration the bullets last until they are destroyed, low number reduces potential lag
  

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

    private IEnumerator ShootCoroutine(Vector3 point)
    {

        float xangle = Mathf.Atan2(point.z - tf.position.z, point.y - tf.position.y) * 180 / Mathf.PI;
        float yangle = Mathf.Atan2(point.x - tf.position.x, point.z - tf.position.z) * 180 / Mathf.PI;

        GameObject bullet = Instantiate(bulletPrefab, tf.position, Quaternion.Euler(xangle, yangle, 0), bullets);
        Destroy(bullet, playerBulletLifetime);

        Vector3 dir = point - tf.position;
        AudioController.instance.PlayerShoot();
        Statistics.instance.numberOfBulletsFired++;
        dir.Normalize();

        bullet.GetComponent<Rigidbody>().velocity = dir * playerBulletSpeed;

        bullet.GetComponent<PlayerBullet>().SetDamage(playerBulletDamage);
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
