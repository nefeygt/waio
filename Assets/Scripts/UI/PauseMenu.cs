using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management
using UnityEngine.InputSystem;
using System;
using UnityEngine.InputSystem.OnScreen; // Required for Input Action disabling

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    [SerializeField] public GameObject pauseMenuUI;
    [SerializeField] public GameObject leftButton;
    [SerializeField] public GameObject rightButton;
    [SerializeField] public GameObject jumpButton;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        Time.timeScale = 1f;
        GameIsPaused = false;
        AudioListener.pause = false;

        SetMovementButtonsActive(true);
    }

    void Pause()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
        Time.timeScale = 0f;
        GameIsPaused = true;
        AudioListener.pause = true;

        SetMovementButtonsActive(false);
    }

    private void SetMovementButtonsActive(bool isActive)
    {
        if (leftButton != null)
        {
            var onScreenButton = leftButton.GetComponent<OnScreenButton>();
            if (onScreenButton != null)
            {
                onScreenButton.enabled = isActive;
            }
        }
        if (rightButton != null)
        {
            var onScreenButton = rightButton.GetComponent<OnScreenButton>();
            if (onScreenButton != null)
            {
                onScreenButton.enabled = isActive;
            }
        }
        if (jumpButton != null)
        {
            var onScreenButton = jumpButton.GetComponent<OnScreenButton>();
            if (onScreenButton != null)
            {
                onScreenButton.enabled = isActive;
            }
        }
    }

    public void RestartLevel()
    {
        Resume();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadMainMenu()
    {
        Resume();
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
