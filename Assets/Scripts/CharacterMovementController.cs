using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovementController : MonoBehaviour
{
    public enum MovementStates
    {
        Idle,
        Running,
        Jumping,
        Attacking
    }
    public enum FacingDirection
    {
        Right,
        Left
    }

    [Header("Movement Values")]
    public float movementSpeed;
    public float jumpForce;

    [Header("Raycast Lenght and layerMask")]
    public float isGroundedRayLength;
    public LayerMask platformLayerMask;

    [Header("Movement States")]
    public MovementStates movementState;
    public FacingDirection facingDirection;

    private Rigidbody2D rigidBody2D;
    private SpriteRenderer spriteRenderer;
    private CharacterAnimationController animController;
    

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        animController = GetComponent<CharacterAnimationController>();
    }
    void Update()
    {
        HandleJump();
    }

    private void FixedUpdate()
    {
        SetCharacterState();
        HandleMovement();
        PlayAnimationsBasedOnstate();
        SetCharacterDirection();
    }
    private void HandleMovement()
    {
        rigidBody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        if (Input.GetKey(KeyCode.A))
        {
            rigidBody2D.velocity = new Vector2(-movementSpeed, rigidBody2D.velocity.y);

        }
        else
        {
            if (Input.GetKey(KeyCode.D))
            {
                rigidBody2D.velocity = new Vector2(+movementSpeed, rigidBody2D.velocity.y);
            }
            else //No keys pressed
            {
                rigidBody2D.velocity = new Vector2(0, rigidBody2D.velocity.y);
                rigidBody2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }
    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(spriteRenderer.bounds.center,
            spriteRenderer.bounds.size, 0f, Vector2.down,
            isGroundedRayLength, platformLayerMask);

        Color rayColor;

        if (raycastHit2D.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        return raycastHit2D.collider != null;

    }
    private void HandleJump()
    {
        if (Input.GetKey(KeyCode.Space) && IsGrounded())
        {
            rigidBody2D.velocity = Vector2.up * jumpForce;
        }
    }

    private void SetCharacterState()
    {
        if (IsGrounded())
        {
            if (rigidBody2D.velocity.x == 0)
            {
                movementState = MovementStates.Idle;
            }
            else if (rigidBody2D.velocity.x > 0)
            {
                facingDirection = FacingDirection.Right;
                movementState = MovementStates.Running;
            }
            else if (rigidBody2D.velocity.x < 0)
            {
                facingDirection = FacingDirection.Left;
                movementState = MovementStates.Running;
            }
        }
        else
        {
            movementState = MovementStates.Jumping;
        }
    }

    private void PlayAnimationsBasedOnstate()
    {
        switch (movementState)
        {
            case MovementStates.Idle:
                animController.PlayIdleAnim();
                break;
            case MovementStates.Running:
                animController.PlayRunningAnim();
                break;
            case MovementStates.Jumping:
                animController.PlayJumpAnim();
                break;
            case MovementStates.Attacking:
                break;
            default:
                break;
        }
    }

    private void SetCharacterDirection()
    {
        switch (facingDirection)
        {
            case FacingDirection.Right:
                spriteRenderer.flipX = false;
                break;
            case FacingDirection.Left:
                spriteRenderer.flipX = true;
                break;
        }
    }
}
