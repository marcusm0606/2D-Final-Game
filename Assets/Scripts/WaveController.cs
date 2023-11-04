using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Wave
{
    // Information about a wave
    public string waveName;            // Name of the wave
    public int noOfEnemies;           // Number of enemies in the wave
    public GameObject[] typeOfEnemy;  // Types of enemies in the wave
    public float spawnRate;           // Rate at which enemies spawn
}

public class WaveController : MonoBehaviour
{
    public Wave[] waves;               // Array of waves
    public Transform[] spawnPoints;   // Array of spawn points for enemies

    private Wave currentWave;          // The current wave being processed
    private int currentWaveNumber;    // The index of the current wave
    private bool canSpawn = true;     // Indicates if enemies can spawn
    private float nextSpawn;          // Time of the next enemy spawn
    public Animator animator;         // Reference to the animator for wave completion animation
    public TextMeshProUGUI waveName;  // UI text element for displaying wave name
    private bool canAnimate = false;  // Indicates if the wave completion animation can be triggered
    public int nextSceneLoad;

    void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
        Debug.Log($"Initial nextSceneLoad: {nextSceneLoad}");
    }
    private void Update()
    {
        currentWave = waves[currentWaveNumber]; // Get the current wave from the array
        SpawnWave(); // Spawn enemies based on the current wave's properties

        // Find all GameObjects with the "Enemy" tag
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (totalEnemies.Length == 0)
        {
            // If all enemies are defeated and there are more waves
            if (currentWaveNumber + 1 != waves.Length)
            {
                if (canAnimate)
                {
                    // Update the UI with the next wave's name, trigger wave completion animation,
                    // and prevent multiple animations from triggering
                    waveName.text = waves[currentWaveNumber + 1].waveName;
                    animator.SetTrigger("WaveComplete");
                    canAnimate = false;
                }
            }

            else// If all waves are completed, trigger game over and load the win scene or unlock next level
            {
                if (SceneManager.GetActiveScene().buildIndex == LevelManager.levelBuildIndexes[LevelManager.levelBuildIndexes.Count])
                {
                    Debug.Log("You Win");
                    SceneManager.LoadScene("Win");
                }
                else
                {
                    int nextLevelBuildIndex = LevelManager.GetNextLevelBuildIndex(SceneManager.GetActiveScene().buildIndex);
                    Debug.Log($"Level Complete! Next Scene Build Index: {nextLevelBuildIndex}");
                    if (nextLevelBuildIndex > PlayerPrefs.GetInt("levelAt"))
                    {
                        PlayerPrefs.SetInt("levelAt", nextLevelBuildIndex);
                    }
                    SceneManager.LoadScene("LevelSelection");
                }
                
            }
        }
    }

    void SpawnNextWave()
    {
        // Move to the next wave and allow spawning
        currentWaveNumber++;
        canSpawn = true;
    }

    void SpawnWave()
    {
        if (canSpawn && nextSpawn < Time.time)
        {
            // Get a random enemy type and spawn it at a random spawn point
            GameObject randomEnemy = currentWave.typeOfEnemy[Random.Range(0, currentWave.typeOfEnemy.Length)];
            Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(randomEnemy, randomPoint.position, Quaternion.identity);

            // Decrease the count of remaining enemies in the wave
            currentWave.noOfEnemies--;

            // Set the time for the next enemy spawn
            nextSpawn = Time.time + currentWave.spawnRate;

            // If there are no more enemies in the wave, prevent further spawning and allow animation
            if (currentWave.noOfEnemies == 0)
            {
                canSpawn = false;
                canAnimate = true;
            }
        }
    }
}
