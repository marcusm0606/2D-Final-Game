using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerHealth playerHealth; // Reference to the PlayerHealth script

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2;

    private Transform target;
    private int pathIndex = 0;

    void Start()
    {
        target = LevelManager.main.path[pathIndex];
        // Find the PlayerHealth component in the scene
        // This assumes there's only one PlayerHealth component in the scene
        if (playerHealth == null)
            playerHealth = FindObjectOfType<PlayerHealth>();
    }

    void Update()
    {
        if (target != null && Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;
            if (pathIndex >= LevelManager.main.path.Length)
            {
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(1); // Assumes each enemy deals 1 damage
                }
                Destroy(gameObject);
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }
}
