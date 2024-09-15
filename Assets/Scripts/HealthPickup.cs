using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthRestore = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 180, 0);
    private AudioSource pickupSource;

    private void Awake()
    {
        pickupSource = GetComponent<AudioSource>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Damageable>(out Damageable damageable) && damageable.Health < damageable.MaxHealth)
        {
            if (damageable.Heal(healthRestore))
            {
                pickupSource?.Play(); // Use null-conditional to avoid errors
                Destroy(gameObject);
            }
        }
    }

    private void Update()
    {
        transform.Rotate(spinRotationSpeed * Time.deltaTime);
    }
}
