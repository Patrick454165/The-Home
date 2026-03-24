using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public PlayerInput pi;
    Vector2 getMovement;
    public bool canMove;
    public float moveModifier;
    public float checkThisFarForConsole;
    public GameObject cam;
    public Camera camAsset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    public void OnMove(InputValue value)
    {
        if (canMove)
        {
            getMovement = value.Get<Vector2>();
        }
        
    }

    public void OnAttack(InputValue value)
    {
        Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward)*checkThisFarForConsole, Color.red, 10f);
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, checkThisFarForConsole))
        {
            if (hit.collider.gameObject.CompareTag("Console"))
            {
                Camera conCam = hit.collider.gameObject.GetComponentInChildren<Camera>();
                camAsset.targetDisplay=1;
                conCam.targetDisplay=0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(new(getMovement.x*moveModifier*Time.deltaTime, 0, getMovement.y*moveModifier*Time.deltaTime));
    }
}
