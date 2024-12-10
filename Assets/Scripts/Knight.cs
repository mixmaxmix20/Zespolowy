using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class Knight : MonoBehaviour
{
    public float moveSpeed = 4f;                            // Prêdkoœæ poruszania rycerza
    public float maxSpeed = 4f;                             // Maksymalna prêdkoœæ rycerza
    public float moveStopSpeed = 0.6f;                      // Prêdkoœæ zatrzymywania
    public float knockbackForce = 10f;                      // Si³a knockbacku
    
    Rigidbody2D rb;                                         // Odnoœnik do Rigidbody2D rycerza
    TouchingDirections touchingDirections;                  // Odnoœnik do komponentu wykrywaj¹cego dotkniêcia
    Animator animator;                                      // Odnoœnik do animatora
    Damageable damageable;                                  // Odnoœnik do komponentu Damageable
    
    public DetectionZone attackZone;                        // Strefa ataku
    public DetectionZone cliffDetectionZone;                // Strefa wykrywania klifu
    public enum WalkableDirection { Right, Left }           // Kierunki poruszania siê
    private WalkableDirection _walkDirection;               // Kierunek poruszania siê
    private Vector2 walkDirectionVector = Vector2.right;    // Wektor kierunku poruszania siê
    public bool _HasTarget = false;                         // Flaga informuj¹ca o posiadaniu celu

    // W³aœciwoœæ ustawiaj¹ca kierunek poruszania siê i obracaj¹ca postaæ
    public WalkableDirection WalkDirection
    {
        get { return _walkDirection; }
        set
        {
            if (_walkDirection != value)
            {
                // Obracanie postaci
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if (value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if (value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    public bool CanMove
    {
        get { return animator.GetBool(AnimationStrings.canMove); }
    }

    public bool HasTarget
    {
        get { return _HasTarget; }
        private set
        {
            _HasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }

    public float AttackCooldown
    {
        get { return animator.GetFloat(AnimationStrings.attackCooldown); }
        private set { animator.SetFloat(AnimationStrings.attackCooldown, Mathf.Max(value, 0)); }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        touchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }
    private void Update()
    {
        // Sprawdzenie, czy rycerz ma cel w zasiêgu
        HasTarget = attackZone.detectedColliders.Count > 0;
        if (AttackCooldown > 0)
        {
            AttackCooldown -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        if (HasTarget)
        {
            // Zatrzymywanie rycerza, gdy ma cel
            rb.velocity = Vector2.zero;
        }
        else
        {
            // Normalne poruszanie, jeœli rycerz nie ma celu
            if (touchingDirections.isGrounded && touchingDirections.isOnWall)
            {
                FlipDirection();
            }

            // Poruszanie, jeœli nie ma zablokowanej prêdkoœci i mo¿na siê poruszaæ
            if (!damageable.LockVelocity)
            {
                if (CanMove)
                {
                    rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x + (moveSpeed * walkDirectionVector.x * Time.deltaTime), -maxSpeed, maxSpeed), rb.velocity.y);
                }
                else
                {
                    // Stopniowe zatrzymywanie, jeœli nie mo¿na siê poruszaæ
                    rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, moveStopSpeed), rb.velocity.y);
                }
            }
        }
    }

    private void FlipDirection()
    {
        // Nie obracaj postaci, jeœli rycerz atakuje
        if (HasTarget) return;

        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if (WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
        else
        {
            Debug.Log("Current walkable direction is not set to legal values of right and left");
        }
    }
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void OnCliffDetected()
    {
        if (touchingDirections.isGrounded)
        {
            FlipDirection();
        }
    }
}
