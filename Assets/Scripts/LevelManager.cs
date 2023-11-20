using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;

    public Transform startPoint;
    public Transform[] path;
    public int currency;

    void Awake()
    {
        main = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        currency = 200;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void IncreaseCurrency(int amount)
    {
        currency += amount;
    }
    public bool SpendCurrency(int amount)
    {
        if (amount <= currency )
        {
            
            currency -= amount;
            return true;

        }
        else
        {
            Debug.Log("u Broke lol");
                return false; 
        }
    }
    public static Dictionary<int, int> levelBuildIndexes = new Dictionary<int, int>()
    {
        {1, 4},  // Level 1's build index is 4
        {2, 5},  // Level 2's build index is 5
        {3, 6},  // Level 3's build index is 6
        // Continue mapping levels to their build indexes
    };

    public static int GetNextLevelBuildIndex(int currentLevelBuildIndex)
    {
        int levelNumber = currentLevelBuildIndex - 3; // Assuming level 1 starts at build index 4
        int nextLevelNumber = levelNumber + 1;

        if (levelBuildIndexes.TryGetValue(nextLevelNumber, out int nextLevelBuildIndex))
        {
            return nextLevelBuildIndex;
        }
        else
        {
            return -1; // No more levels available
        }
    }
}
