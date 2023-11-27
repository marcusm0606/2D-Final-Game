using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    private Transform target;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private float lifetime = 5f; // Lifetime of the bullet
    [SerializeField] public float SplashRange = 0f;
    [SerializeField] private float slowTime = 0f; // Duration of slow effect
    [SerializeField] private float slowStrength = 0f; // Strength of slow effect

    private void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the bullet after 'lifetime' seconds
    }

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * bulletSpeed;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        ApplyDamageAndEffects(other);

        Destroy(gameObject); // Destroy the bullet on collision or after applying effects
    }

    private void ApplyDamageAndEffects(Collision2D collision)
    {
        var hitColliders = SplashRange > 0 ? Physics2D.OverlapCircleAll(transform.position, SplashRange) : new Collider2D[] { collision.collider };

        foreach (var hitCollider in hitColliders)
        {
            Health healthComponent = hitCollider.GetComponent<Health>();
            EnemyMovement enemyMovement = hitCollider.GetComponent<EnemyMovement>();

            healthComponent?.TakeDamage(bulletDamage);
            if (slowTime > 0 && slowStrength > 0 && enemyMovement != null)
            {
                enemyMovement.ApplySlow(slowStrength, slowTime);
            }
        }
    }

}
