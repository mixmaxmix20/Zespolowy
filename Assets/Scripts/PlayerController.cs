using UnityEngine.InputSystem;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float moveSpeedMultiplier = 1f;
    public float targetSpeed;
    public float acceleration = 7f;
    public float decceleration = 7f;
    public float velPower = 0.9f;
    public float attackMoveSpeed = 0f;
    public float frictionAmount = 0.2f;
    public float knockbackForceX = 15f;

    public float jumpImpulse = 10f;
    public float jumpMultiplier = 1f;
    public float maxJumpTime = 0.3f;
    public float jumpHoldForce = 5f;
    public float jumpCutMultiplier = 0.5f;

    private bool _isFacingRight = true;
    private float speedDif;
    private float accelRate;
    private float movement;
    private bool _isMoving = false;

    private bool canReceiveInput = true;
    private float lastOnGroundTime;
    private bool isJumping = false;
    private float jumpTimeCounter;

    private Vector2 moveInput;
    private TouchingDirections touchingDirections;
    private Rigidbody2D rb;
    private Animator animator;
    private Damageable damageable;
    private InventoryManager inventoryManager; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
        if (inventoryManager == null)
        {
            inventoryManager = FindObjectOfType<InventoryManager>();
        }
    }
    
    public void BlockInput() => CanReceiveInput = false;
    public void UnblockInput() => CanReceiveInput = true;
    
    public bool IsMoving
    {
        get { return _isMoving; }
        private set
        {
            if (touchingDirections.isOnWall && !touchingDirections.isGrounded)
            {
                _isMoving = false;
            }
            else
            {
                _isMoving = value;
            }

            animator.SetBool(AnimationStrings.isMoving, _isMoving);
        }
    }

    public bool CanReceiveInput
    {
        get => canReceiveInput;
        set
        {
            canReceiveInput = value;
            animator.SetBool(AnimationStrings.canReceiveInput, canReceiveInput);
        }
    }

    public float AttackCooldown
    {
        get { return animator.GetFloat(AnimationStrings.attackCooldown); }
        private set { animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0)); }
    }

    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }
            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get { return animator.GetBool(AnimationStrings.canMove); }
    }

    public float CurrentSpeed
    {
        get { return animator.GetFloat(AnimationStrings.currentSpeed); }
        private set { animator.SetFloat(AnimationStrings.currentSpeed, value); }
    }

    private void FixedUpdate()
    {
        UpdateGroundTime();
        HandleMovement();
        HandleGravity();

        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }

    public void StartSpeedBoost(float multiplier)
    {
        StartCoroutine(SpeedBoostCoroutine(multiplier));
    }

    private IEnumerator SpeedBoostCoroutine(float multiplier)
    {
        moveSpeedMultiplier = multiplier;
        yield return new WaitForSeconds(20f);
        moveSpeedMultiplier = 1f;
        print("Speed boost przestal dzialac");
    }

    public void StartJumpBoost(float multiplier)
    {
        StartCoroutine(JumpBoostCoroutine(multiplier));
    }

    private IEnumerator JumpBoostCoroutine(float multiplier)
    {
        jumpMultiplier = multiplier;
        yield return new WaitForSeconds(20f);
        jumpMultiplier = 1f;
        print("Jump boost przestal dzialac");
    }

    public void StartDamageBoost(float multiplier)
    {
        StartCoroutine(DamageBoostCoroutine(multiplier));
    }

    private IEnumerator DamageBoostCoroutine(float multiplier)
    {
        //damageMultiplier = multiplier;
        yield return new WaitForSeconds(20f);
        //damageMultiplier = 1f;
        print("Damage boost przestal dzialac");
    }

    private void HandleMovement()
    {
        if (!damageable.LockVelocity)
        {
            if (CanMove)
            {
                targetSpeed = moveInput.x * moveSpeed * moveSpeedMultiplier;
                speedDif = targetSpeed - rb.velocity.x;
                accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
                movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

                rb.AddForce(movement * Vector2.right);

                if (touchingDirections.isGrounded && Mathf.Abs(moveInput.x) < 0.01f)
                {
                    ApplyFriction();
                }
            }
            else
            {
                rb.velocity = new Vector2(moveInput.x * attackMoveSpeed, rb.velocity.y);
            }
        }
        CurrentSpeed = Mathf.Abs(rb.velocity.x);
    }

    private void Update()
    {
        Application.targetFrameRate = 60;
        
        SetFacingDirection(moveInput);
    }

    private void ApplyFriction()
    {
        float frictionForce = Mathf.Min(Mathf.Abs(rb.velocity.x), frictionAmount);
        frictionForce *= Mathf.Sign(rb.velocity.x);
        rb.AddForce(Vector2.right * -frictionForce, ForceMode2D.Impulse);
    }

    private void UpdateGroundTime()
    {
        lastOnGroundTime = touchingDirections.isGrounded ? 0.1f : lastOnGroundTime - Time.deltaTime;
    }

    private void HandleGravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = 2f;
        }
        else
        {
            rb.gravityScale = 1f;
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (CanMove)
        {
            if (moveInput.x > 0 && !IsFacingRight)
            {
                IsFacingRight = true;
            }
            else if (moveInput.x < 0 && IsFacingRight)
            {
                IsFacingRight = false;
            }
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (CanMove)
        {
            IsMoving = moveInput != Vector2.zero;
        }

        SetFacingDirection(moveInput);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && CanJump())
        {
            Jump();
        }

        if (context.performed && isJumping && jumpTimeCounter < maxJumpTime)
        {
            jumpTimeCounter += Time.deltaTime;
            rb.AddForce(Vector2.up * jumpHoldForce, ForceMode2D.Force);
        }

        if (context.canceled && isJumping)
        {
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * jumpCutMultiplier);
            }
            isJumping = false;
        }
    }

    private void Jump()
    {
        isJumping = true;
        jumpTimeCounter = 0f;

        animator.SetTrigger(AnimationStrings.jumpTrigger);

        float force = jumpImpulse;
        if (rb.velocity.y < 0)
        {
            force -= rb.velocity.y;
        }

        rb.AddForce(Vector2.up * force * jumpMultiplier, ForceMode2D.Impulse);
    }

    private bool CanJump()
    {
        return lastOnGroundTime > 0 && CanMove;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.isGrounded && CanReceiveInput && !inventoryManager.IsInventoryOpen && !inventoryManager.IsEquipmentOpen)
        {
            animator.SetTrigger(AnimationStrings.attackTrigger);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        canReceiveInput = true;
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            inventoryManager.ToggleInventory();
        }
    }

    public void OnOpenEquipment(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            inventoryManager.ToggleEquipment();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Vector2 knockbackDirection = (transform.position - collision.transform.position).normalized;
            knockbackDirection.y = 0;

            Vector2 knockback = new Vector2(knockbackDirection.x * knockbackForceX, rb.velocity.y);

            damageable.Hit(10, knockback);
        }
    }
}
