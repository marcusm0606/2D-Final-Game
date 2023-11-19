using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int hitPoints = 1;
    [SerializeField] private GameObject balloonToDrop; // Reference to the red balloon prefab
    [SerializeField] private int currencyWorth = 2;

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0)
        {
            if (balloonToDrop != null)
            {
                GameObject droppedBalloon = Instantiate(balloonToDrop, transform.position, Quaternion.identity);
                EnemyMovement droppedBalloonMovement = droppedBalloon.GetComponent<EnemyMovement>();

                if (droppedBalloonMovement != null)
                {
                    // Set the pathIndex of the red balloon to continue from the current blue balloon's pathIndex
                    EnemyMovement currentBalloonMovement = GetComponent<EnemyMovement>();
                    if (currentBalloonMovement != null)
                    {
                        droppedBalloonMovement.SetPathIndex(currentBalloonMovement.pathIndex);
                    }
                }
            }
            Destroy(gameObject); // Destroy the blue balloon
            LevelManager.main.IncreaseCurrency(currencyWorth);
        }
    }
}
