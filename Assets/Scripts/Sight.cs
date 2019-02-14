using UnityEngine;
using UnityEngine.UI;

/* Script made by Daniel */
public class Sight : MonoBehaviour
{
    [SerializeField] private RectTransform canvasRect;

    [Range(100, 1000)] [Tooltip("The sensitivity when moving cursor")] 
    [SerializeField] private float sensitivity;
    [Range(100, 600)] [Tooltip("The sensitivity when shooting and moving cursor")]
    [SerializeField] private float shootingSensitivity;

    Vector2 minBounds;
    Vector2 maxBounds;
    Vector2 size;

    private void Start()
    {
        Image img = GetComponent<Image>();

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
            float currentSensitivity;
            if (InputController.instance.GetKeyShoot())
            {
                currentSensitivity = shootingSensitivity;
            }
            else currentSensitivity = sensitivity;

            Vector2 movement = new Vector2(x, y) * currentSensitivity * Time.deltaTime;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + movement.x, minBounds.x, maxBounds.x), Mathf.Clamp(transform.position.y + movement.y, minBounds.y, maxBounds.y), 0);
        }
        else transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
    }

    //Bör kallas vid resolution byte
    public void SetScreenBounds()
    {
        minBounds = new Vector2(size.x/2, size.y/2);
        maxBounds = new Vector2(canvasRect.rect.width - (size.x/2), canvasRect.rect.height - (size.y/2));  
    }
}