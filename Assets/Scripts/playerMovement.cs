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
    public Camera conCam;
    public GameObject conConsole;
    public GameObject conCanvas;
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
        if (canMove)
        {
            Debug.DrawRay(cam.transform.position, cam.transform.TransformDirection(Vector3.forward)*checkThisFarForConsole, Color.red, 10f);
            RaycastHit hit;
            if (Physics.Raycast(cam.transform.position, cam.transform.TransformDirection(Vector3.forward), out hit, checkThisFarForConsole))
            {
                if (hit.collider.gameObject.CompareTag("Console"))
                {
                    conCam = hit.collider.gameObject.GetComponent<consoleScript>().cameraAsset;
                    conCanvas=hit.collider.gameObject.GetComponent<consoleScript>().canvas;
                    conConsole = hit.collider.gameObject;
                    conConsole.GetComponent<BoxCollider>().enabled=false;
                    camAsset.targetDisplay=1;camAsset.enabled=false;
                    conCam.targetDisplay=0;conCam.enabled=true;
                    Cursor.lockState = CursorLockMode.None;
                    canMove=false;
                    conCanvas.SetActive(true);
                }
            }
        }
        
    }

    public void OnExit(InputValue value)
    {
        if (!canMove)
        {
            Cursor.lockState = CursorLockMode.Locked;
            canMove=true;
            conConsole.GetComponent<BoxCollider>().enabled=true;
            camAsset.targetDisplay=0;camAsset.enabled=true;
            conCam.targetDisplay=1;conCam.enabled=false;
            conCanvas.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(new(getMovement.x*moveModifier*Time.deltaTime, 0, getMovement.y*moveModifier*Time.deltaTime));

    }

    
    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("restricted"))
        {
            transform.Translate(new(-getMovement.x*moveModifier*Time.deltaTime, 0, -getMovement.y*moveModifier*Time.deltaTime));
        }
        
    }
}
