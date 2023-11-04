using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelSelection : MonoBehaviour
{
    public Button[] lvlButtons;

    void Start()
    {
        int levelAt = PlayerPrefs.GetInt("levelAt", 4); // Assuming level 1 starts at build index 4
        Debug.Log($"Level at: {levelAt}");

        foreach (var button in lvlButtons)
        {
            int levelBuildIndex = int.Parse(button.name.Replace("Level", "")) + 3; // Get the build index from the button's name
            button.interactable = levelBuildIndex <= levelAt;
        }
    }

}
