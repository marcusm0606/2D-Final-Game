using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerHealth playerHealth; // Reference to the PlayerHealth script

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2;

    private Transform target;
    public int pathIndex = 0;
    public int damage;
    void Start()
    {
        target = LevelManager.main.path[pathIndex];
        // Find the PlayerHealth component in the scene
        // This assumes there's only one PlayerHealth component in the scene
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
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damage); 
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
