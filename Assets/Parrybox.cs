using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrybox : MonoBehaviour
{

    RaycastHit hit;
    Vector3 point;

    [SerializeField] public LayerMask lm = 0;

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Boss Parryable Bullet")
        {

            other.tag = "Bullet";

            Rigidbody rigi = other.GetComponent<Rigidbody>();

            //float xangle = Mathf.Atan2(point.z - rigi.position.z, point.y - rigi.position.y) * 180 / Mathf.PI;
            //float yangle = Mathf.Atan2(point.x - rigi.position.x, point.z - rigi.position.z) * 180 / Mathf.PI;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 0))
            {

                if (hit.transform.gameObject.tag != "Player")
                {
                    point = hit.point;
                }

            }

            else
            {
                point = ray.GetPoint(transform.parent.GetComponent<PlayerController>().GetBulletTrajectoryDistance());
            }



            Vector3 dir = point - rigi.position;

            dir.Normalize();

            rigi.velocity = dir * transform.parent.GetComponent<PlayerController>().GetParryBulletSpeed();

            other.gameObject.GetComponent<Damage>().SetDamage(1);

        }

    }

}

/*
 
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out aimHit))
        {

            if (aimHit.transform.gameObject.tag != "Player")
            {
                aimPoint = aimHit.point;
            }

        }

        else
        {
            aimPoint = ray.GetPoint(bulletTrajectoryDistance);
        }

    ====================================================================================================================================

        float xangle = Mathf.Atan2(point.z - rigi.position.z, point.y - rigi.position.y) * 180 / Mathf.PI;
        float yangle = Mathf.Atan2(point.x - rigi.position.x, point.z - rigi.position.z) * 180 / Mathf.PI;

        GameObject bullet = Instantiate(bulletPrefab, rigi.position, Quaternion.Euler(xangle, yangle, 0), bullets);
        Destroy(bullet, bulletDuration);

        Vector3 dir = point - rigi.position;

        dir.Normalize();

        bullet.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;

        bullet.GetComponent<Damage>().SetDamage(bulletDamage);

        AudioController.instance.PlayerShoot();

        yield return new WaitForSeconds(1 / bulletsPerSecond);
        shootCoroutine = null;
        yield break;
 
     */
