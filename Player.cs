using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Player : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction switchAction;
    public Rigidbody2D rb;
    public Collider2D hitbox;

    [Header("Movement Settings")]
    // All curves represent velocity curves
    public bool xControlIsActive = true;
    public Vector2 moveVector;
    public float speedMult = 0.5f;
    public AnimationCurve moveCurve = AnimationCurve.EaseInOut(0, 0, 0.5f, 1);
    private int moveTimer = 0;

    [Header("Jump Settings")]
    public bool yControlIsActive = true;
    public float jumpPowerMult = 6f;
    public AnimationCurve jumpPowerCurve = AnimationCurve.EaseInOut(0, 1, 0.5f, 0);
    private bool jumped = false;
    public bool normalGravity = true;

    //[SerializeField] private bool hasTouchedGround = false;

    void Start()
    {
        // Instansiate the doopid things
        moveAction = InputSystem.actions.FindAction("Move");
        switchAction = InputSystem.actions.FindAction("Attack");
        rb = GetComponent<Rigidbody2D>();
        hitbox = GetComponent<Collider2D>();
    }

    void Update()
    {
        moveVector = moveAction.ReadValue<Vector2>();

        if (switchAction.IsPressed())
        {

        }


        if (moveVector != Vector2.zero) 
        {
            // Movement
            if (xControlIsActive){MoveX(moveVector.normalized.x);}

            // Jumping
            if (IsGrounded() && moveVector.y > 0 && !jumped && normalGravity){
                jumped = true;
                StartCoroutine(JumpCoroutine());
            }
            else if(IsGrounded() && moveVector.y < 0 && !jumped && !normalGravity){
                jumped = true;
                StartCoroutine(JumpCoroutine());
            }
        }
        ;
    }

    public void MoveX(float amount)
    {
        if (amount == 0) { moveTimer = 0; }

        if (amount != 0)
        {
            float movePower = moveCurve.Evaluate(moveTimer * Time.deltaTime) * speedMult;
            rb.AddForceX(amount * movePower);
            moveTimer += 1;
        }
    }

    private IEnumerator JumpCoroutine()
    {
        int timer = 0;

        while (moveVector.y != 0)
        {
            float jumpPower = jumpPowerCurve.Evaluate(timer * Time.deltaTime) * jumpPowerMult;
            rb.AddForceY(moveVector.normalized.y * jumpPower);
            timer+=1;

            yield return new WaitForEndOfFrame();
        }
        jumped = false;
        yield return new WaitForEndOfFrame();
    }

    public bool IsGrounded()
    {
        return rb.linearVelocity.y == 0;
    }
    public void FlipGravity(){
        normalGravity = !normalGravity;
        rb.gravityScale *= -1;
    }
 }
