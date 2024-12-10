using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Damageable : MonoBehaviour
{
 
    public UnityEvent<int, Vector2> damagableHit;  
    public UnityEvent<int, int> healthChanged;     

    [SerializeField] private int _maxHealth = 100; // Maksymalne zdrowie
    [SerializeField] private int _health = 100;    // Aktualne zdrowie
    private float alpha = 1f;                      // Poziom przezroczystoœci podczas nietykalnoœci
    private bool _isAlive = true;                  // Czy obiekt ¿yje
    public bool isInvincible = false;              // Czy obiekt jest nietykalny
    private float timeSinceHit = 0;                // Czas od ostatniego trafienia
    public float invincibilityTime = 3f;           // Czas trwania nietykalnoœci po otrzymaniu obra¿eñ

    private Animator animator;
    private Rigidbody2D rb;
    private Renderer rend;
    private Color originalColor;

    public int Maxhealth
    {
        get { return _maxHealth; }
        private set { _maxHealth = value; }
    }

    public int Health
    {
        get { return _health; }
        private set
        {
            _health = value;
            healthChanged?.Invoke(_health, Maxhealth);
            if (_health <= 0) isAlive = false;
        }
    }
  
    public bool LockVelocity
    {
        get { return animator.GetBool(AnimationStrings.lockVelocity); }
        set { animator.SetBool(AnimationStrings.lockVelocity, value); }
    }

    public bool isAlive
    {
        get { return _isAlive; }
        private set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set to " + value);

            if (!value)
            {
                // Po œmierci blokuje ruch i wy³¹cza kontrolê gracza
                rb.gravityScale = 500;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                var playerInput = GetComponent<PlayerInput>();
                if (playerInput != null) playerInput.enabled = false;
            }
        }
    }

    private void Awake()
    {     
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<Renderer>();
        originalColor = rend.material.color;
    }

    private void Update()
    {       
        if (isInvincible && isAlive)
        {
            timeSinceHit += Time.deltaTime;
            if (timeSinceHit >= invincibilityTime) DisableInvincibility();
            else HandleInvincibilityEffect();
        }
    }
   
    public bool Hit(int damage, Vector2 knockback)
    {
        if (isAlive && !isInvincible)
        {
            
            Health -= damage;
            isInvincible = true;
            timeSinceHit = 0;

            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;

            
            damagableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            EnableInvincibility(); 
            return true;
        }
        return false;
    }

    // W³¹czenie nietykalnoœci, ignorowanie kolizji miêdzy okreœlonymi warstwami czyli 7 to gracz 8 to przeciwnik
    private void EnableInvincibility()
    {
        isInvincible = true;
        timeSinceHit = 0;
        Physics2D.IgnoreLayerCollision(7, 8, true);
        alpha = 1f;
    }

   
    private void HandleInvincibilityEffect()
    {
        float fadeSpeed = 3f;  // Szybkoœæ migotania przezroczystoœci
        alpha = Mathf.PingPong(Time.time * fadeSpeed, 0.7f) + 0.3f;  // Zakres migotania od 0.3 do 1
        rend.material.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
    }

    
    private void DisableInvincibility()
    {
        isInvincible = false;
        timeSinceHit = 0;
        Physics2D.IgnoreLayerCollision(7, 8, false);
        rend.material.color = originalColor;
    }

    
    public bool Heal(int healthRestore)
    {
        if (isAlive && Health < Maxhealth)
        {
            int maxHeal = Mathf.Max(Maxhealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;
            CharacterEvents.characterHealed.Invoke(gameObject, actualHeal); 
            return true;
        }
        return false;
    }

    public void IncreaseMaxHealth(int additionalHealth)
    {
        Maxhealth += additionalHealth;
        Health = Mathf.Min(Health, Maxhealth);  // Zapewnia, ¿e Health nie przekroczy nowego Maxhealth

        // Wywo³ujemy healthChanged, aby zaktualizowaæ maksymalne zdrowie w UI
        healthChanged.Invoke(Health, Maxhealth);
    }
}
