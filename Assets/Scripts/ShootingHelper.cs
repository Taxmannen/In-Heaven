using UnityEngine;

public class ShootingHelper : MonoBehaviour
{
    public static GameObject Shoot(Vector3 origin, Vector3 target, GameObject projectile, float speed = 10, Transform parent = null, float destroyTime = 3)
    {
        float xangle = Mathf.Atan2(target.z - origin.z, target.y - origin.y) * 180 / Mathf.PI;
        float yangle = Mathf.Atan2(target.x - origin.x, target.z - origin.z) * 180 / Mathf.PI;
        //Debug.Log("!: " + "[x angle] " + xangle + ". [y angle] " + yangle + ". [target] " + target + ". [direction]: " + (target - origin));
        GameObject pro = projectile;
        pro.transform.position = origin;
        pro.transform.rotation = Quaternion.Euler(xangle, yangle, 0);
        pro.transform.SetParent(parent);
        Vector3 dir = target - origin;
        dir.Normalize();
        pro.GetComponent<Bullet>().particleEffect.Play();
        pro.GetComponent<Bullet>().SetDirection(dir);
        pro.GetComponent<Rigidbody>().velocity = dir * speed;
        //Destroy(pro, destroyTime);
        return pro;
    }
}