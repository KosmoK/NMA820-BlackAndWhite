using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction switchAction;
    private Rigidbody2D rb;
    private Collider2D hitbox;

    public Vector2 moveVector;
    public AnimationCurve jumpPowerCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    public Boolean gravityNormal = true;

    [SerializeField] private bool hasTouchedGround = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Instansiate the doopid things
        moveAction = InputSystem.actions.FindAction("Move");
        switchAction = InputSystem.actions.FindAction("Attack");
        rb = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        moveVector = moveAction.ReadValue<Vector2>();

        if (switchAction.IsPressed())
        {
            Debug.Log("I'm a general, WEEE!");
        }


        if (moveVector != Vector2.zero) 
        {
            rb.AddForceX(moveVector.normalized.x);
            //TODO ADD JUMP PHYSICS
        };
    }
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.1f);
    }
}
