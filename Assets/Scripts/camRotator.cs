using UnityEngine;
using UnityEngine.InputSystem;


public class camRotator : MonoBehaviour
{
    public float mouseSensitivity;
    Vector2 mouse;
    Rect ScreenRect;
    float xRotation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnLook(InputValue value)
    {
        float mouseX = value.Get<Vector2>().x * mouseSensitivity * Time.deltaTime;
        float mouseY = value.Get<Vector2>().y * mouseSensitivity * Time.deltaTime;
        xRotation += mouseX;
        transform.localRotation = Quaternion.Euler(0f, xRotation, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        
        ScreenRect.x = 0;
        ScreenRect.y = 0;
        ScreenRect.width = Screen.width;
        ScreenRect.height = Screen.height;
        
        
    }
}
