using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int hitPoints = 1;
    [SerializeField] private GameObject balloonToDrop; // Reference to the next lower balloon prefab
    [SerializeField] private int currencyWorth = 2;
    [SerializeField] private AudioClip popSound; // Sound effect for popping
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }
    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0)
        {
            int remainingDamage = -hitPoints; // Calculate remaining damage
            audioSource.PlayOneShot(popSound);
            // Destroy the current balloon
            Destroy(gameObject);

            if (balloonToDrop != null)
            {
                // Spawn the next lower tier balloon
                GameObject droppedBalloon = Instantiate(balloonToDrop, transform.position, Quaternion.identity);
                EnemyMovement droppedBalloonMovement = droppedBalloon.GetComponent<EnemyMovement>();
                Health droppedBalloonHealth = droppedBalloon.GetComponent<Health>();

                // Pass the pathIndex to the new balloon
                if (droppedBalloonMovement != null)
                {
                    EnemyMovement currentBalloonMovement = GetComponent<EnemyMovement>();
                    if (currentBalloonMovement != null)
                    {
                        droppedBalloonMovement.SetPathIndex(currentBalloonMovement.pathIndex);
                    }
                }

                // Apply remaining damage to the new balloon
                if (droppedBalloonHealth != null && remainingDamage > 0)
                {
                    droppedBalloonHealth.TakeDamage(remainingDamage);
                }
            }

            // Increase currency
            LevelManager.main.IncreaseCurrency(currencyWorth);
        }
    }
}
