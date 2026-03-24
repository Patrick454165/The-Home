using UnityEngine;
using UnityEngine.InputSystem;

public class playerMovement : MonoBehaviour
{

    public Rigidbody rb;
    public PlayerInput pi;
    Vector2 getMovement;
    public bool canMove;
    public float moveModifier;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    public void OnMove(InputValue value)
    {
        if (canMove)
        {
            getMovement = value.Get<Vector2>();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.Translate(new(getMovement.x*moveModifier*Time.deltaTime, 0, getMovement.y*moveModifier*Time.deltaTime));
    }
}
