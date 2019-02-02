using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingHelper : MonoBehaviour
{
    public static GameObject Shoot(Vector3 origin,Vector3 target, GameObject projectile, float speed = 10, Transform parent = null)
    {
        float xangle = Mathf.Atan2(target.z - origin.z, target.y - origin.y) * 180 / Mathf.PI;
        float yangle = Mathf.Atan2(target.x - origin.x, target.z - origin.z) * 180 / Mathf.PI;
        GameObject pro = Instantiate(projectile, origin, Quaternion.Euler(xangle, yangle, 0), parent);
        Vector3 dir = target - origin;
        dir.Normalize();
        pro.GetComponent<Rigidbody>().velocity = dir * speed;
        return pro;
    }
}
