using UnityEngine;
using UnityEngine.UI;

/* Script made by Daniel */
public class Sight : MonoBehaviour
{
    public RectTransform canvasRect;

    [Range(300, 1500)]
    public float sensitivity;

    Vector2 bounds;
    Vector2 size;
    Image img;

    private void Start()
    {
        img = GetComponent<Image>();

        Rect rect = GetComponent<RectTransform>().rect;
        size = new Vector2(rect.width, rect.height);

        transform.position = new Vector2((canvasRect.rect.width - (size.x / 2)) / 2, (canvasRect.rect.height - (size.y / 2)) / 2);
        SetScreenBounds();

        if (InputController.instance.isGamePad)
        {
            Cursor.visible = false;
            img.enabled = true;
        }
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal Sight");
        float y = Input.GetAxis("Vertical Sight");

        if (InputController.instance.isGamePad)
        {
            Vector2 movement = new Vector2(x, y) * sensitivity * Time.deltaTime;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + movement.x, 0, bounds.x), Mathf.Clamp(transform.position.y + movement.y, 0, bounds.y), 0);
        }
        else transform.position = new Vector3(Input.mousePosition.x - (size.x/2), Input.mousePosition.y - (size.y/2), 0);
    }

    //Bör kallas vid resolution byte
    void SetScreenBounds()
    {
        bounds = new Vector2(canvasRect.rect.width - size.x, canvasRect.rect.height - size.y);
    }
}