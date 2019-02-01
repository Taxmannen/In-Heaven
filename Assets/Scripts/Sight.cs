using UnityEngine;
using UnityEngine.UI;

/* Script made by Daniel */
public class Sight : MonoBehaviour
{
    public RectTransform canvasRect;

    [Range(300, 1500)]
    public float sensitivity;

    Vector2 minBounds;
    Vector2 maxBounds;
    Vector2 size;
    Image img;

    private void Start()
    {
        img = GetComponent<Image>();

        Rect rect = GetComponent<RectTransform>().rect;
        size = new Vector2(rect.width, rect.height);

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
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + movement.x, minBounds.x, maxBounds.x), Mathf.Clamp(transform.position.y + movement.y, minBounds.y, maxBounds.y), 0);
        }
        else transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    }

    //Bör kallas vid resolution byte
    void SetScreenBounds()
    {
        minBounds = new Vector2(size.x/2, size.y/2);
        maxBounds = new Vector2(canvasRect.rect.width - (size.x/2), canvasRect.rect.height - (size.y/2));  
    }
}