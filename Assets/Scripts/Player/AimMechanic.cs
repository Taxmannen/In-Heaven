using UnityEngine;

public class AimMechanic : MonoBehaviour
{
    [SerializeField] [Range(0, 1000)] private float playerBulletTrajectoryDistance = 50f; //The max end point for the bullets trajectory, should be about the same as the distance between the player face and the boss face.
    private RaycastHit aimHit;
    internal Vector3 aimPoint;
    public Transform aim;

    private Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }
    /// <summary>
    /// Updates the direction the player is aiming towards.
    /// </summary>
    public void Aim()
    {
        Ray ray = camera.ScreenPointToRay(aim.position);

        if (Physics.Raycast(ray, out aimHit, 0))
        {
            if (aimHit.transform.gameObject.tag != "Player")
            {
                aimPoint = aimHit.point;
            }
        }

        else
        {
            aimPoint = ray.GetPoint(playerBulletTrajectoryDistance);
        }

    }
}
