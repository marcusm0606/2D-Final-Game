using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    public void Play(string levelName)
    {
        SceneManager.LoadScene(levelName);
        Time.timeScale = 1f; // Reset the time scale
        PauseMenu.isPaused = false;  // Ensure isPaused is also reset

    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; 
#else
        Application.Quit();
#endif 
    }
}
