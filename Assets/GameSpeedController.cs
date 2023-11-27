using UnityEngine;

public class GameSpeedController : MonoBehaviour
{
    private bool isSpeedIncreased = false;

    public void ToggleGameSpeed()
    {
        if (isSpeedIncreased)
        {
            // Set game speed to normal
            Time.timeScale = 1f;
            isSpeedIncreased = false;
        }
        else
        {
            // Double the game speed
            Time.timeScale = 2f;
            isSpeedIncreased = true;
        }
    }
}
