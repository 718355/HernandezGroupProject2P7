using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Main Menu Transition to Maze //
        SceneManager.LoadScene("Dark Maze");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
