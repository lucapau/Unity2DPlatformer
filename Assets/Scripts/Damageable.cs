using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent damageableDeath;
    public UnityEvent<int, int> healthChanged;

    private Animator animator;

    [SerializeField]
    private int _maxHealth = 100;

    public int MaxHealth
    {
        get => _maxHealth;
        set => _maxHealth = value;
    }

    [SerializeField]
    private int _health = 100;

    public int Health
    {
        get => _health;
        set
        {
            _health = value;
            healthChanged?.Invoke(_health, MaxHealth);

            if (_health <= 0)
                IsAlive = false;
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    public bool IsAlive
    {
        get => _isAlive;
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log($"IsAlive set to {value}");

            if (!value)
                damageableDeath.Invoke();
        }
    }

    [SerializeField]
    private bool isInvincible = false;
    private float timeSinceHit = 0;
    public float invincibilityTime = 0.25f;

    public bool LockVelocity
    {
        get => animator.GetBool(AnimationStrings.lockVelocity);
        set => animator.SetBool(AnimationStrings.lockVelocity, value);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isInvincible)
        {
            timeSinceHit += Time.deltaTime;
            if (timeSinceHit > invincibilityTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
        }
    }

    public bool Hit(int damage, Vector2 knockback)
    {
        if (IsAlive && !isInvincible)
        {
            Health -= damage;
            isInvincible = true;

            animator.SetTrigger(AnimationStrings.hitTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }
        return false;
    }

    public bool Heal(int healthRestore)
    {
        if (IsAlive && Health < MaxHealth)
        {
            int actualHeal = Mathf.Min(MaxHealth - Health, healthRestore);
            Health += actualHeal;
            CharacterEvents.characterHealed.Invoke(gameObject, actualHeal);
            return true;
        }
        return false;
    }
}
