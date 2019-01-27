using UnityEngine;

public class Sight : MonoBehaviour
{
    public RectTransform canvasRect;

    [Range(200, 700)]
    public float sensitivity;

    Vector2 bounds;
    Vector2 size;

    private void Start()
    {
        Rect rect = GetComponent<RectTransform>().rect;
        size = new Vector2(rect.width, rect.height);

        transform.position = new Vector2((canvasRect.rect.width - (size.x / 2)) / 2, (canvasRect.rect.height - (size.y / 2)) / 2);
        bounds = new Vector2(canvasRect.rect.width - size.x, canvasRect.rect.height - size.y); //Bör kallas vid resolution byte
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal Sight");
        float y = Input.GetAxis("Vertical Sight");
        Vector2 movement = new Vector2(x, y) * sensitivity * Time.deltaTime;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x + movement.x, 0, bounds.x), Mathf.Clamp(transform.position.y + movement.y, 0, bounds.y), 0);
    }
}