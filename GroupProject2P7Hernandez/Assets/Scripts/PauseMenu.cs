using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{


    public GameObject pauseMenu;

    public static bool isPaused;


    
    void Start()
    {
        // Start Game = No pause menu and cursor gone //
        pauseMenu.SetActive(false);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GameManager.Instance.IsGameOver)
            {
                if (isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    PauseGame();
                }
            }
        }
    }

    public void PauseGame()
    {
        // When escape press pause menu active //
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        // Resume game when press resume //
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;


    }
    public void GoToMainMenu()
    {
        // Press = Transition to Mani Menu //
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        // Press menu = Quit Game //
        Application.Quit();
        
    }
}
