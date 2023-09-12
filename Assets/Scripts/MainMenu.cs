using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    // Play Button
    public void PlayGame()
    {
        // Load the game scene
        UnityEngine.SceneManagement.SceneManager.LoadScene("Beach");
    }

    // Quit Button
    public void QuitGame()
    {
        // Quit the game
        Application.Quit();
    }
}
