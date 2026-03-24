using UnityEngine;
using UnityEngine.InputSystem;

public class camRotator : MonoBehaviour
{
    public float mouseSensitivity;
    Vector2 mouse;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mouse = Mouse.current.position.ReadValue();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePrev = mouse;
        mouse = Mouse.current.position.ReadValue();
        
        transform.eulerAngles = new(0, (mouse.x)*mouseSensitivity, 0);
        
    }
}
