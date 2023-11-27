using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerHealth playerHealth; // Reference to the PlayerHealth script

    [Header("Attributes")]
    [SerializeField] private float originalSpeed = 2f;
    private float currentSpeed;

    private Transform target;
    public int pathIndex = 0;
    public int damage;

    void Start()
    {
        currentSpeed = originalSpeed; // Initialize current speed
        target = LevelManager.main.path[pathIndex];

        if (playerHealth == null)
            playerHealth = FindObjectOfType<PlayerHealth>();
    }

    public void SetPathIndex(int index)
    {
        pathIndex = index;
    }

    void Update()
    {
        if (target != null && Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;
            if (pathIndex >= LevelManager.main.path.Length)
            {
                playerHealth?.TakeDamage(damage);
                Destroy(gameObject);
                return;
            }
            target = LevelManager.main.path[pathIndex];
        }
    }

    public void ApplySlow(float strength, float duration)
    {
        currentSpeed *= (1 - strength); // Reduce speed
        StartCoroutine(RestoreSpeedAfterDelay(duration));
    }

    private IEnumerator RestoreSpeedAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        currentSpeed = originalSpeed; // Restore original speed
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * currentSpeed; // Use current speed for movement
    }
}
