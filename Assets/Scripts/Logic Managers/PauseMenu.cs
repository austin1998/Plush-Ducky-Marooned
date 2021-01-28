using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoSingleton<PauseMenu>
{

    [SerializeField] GameObject PauseCanvas;
    [SerializeField] GameObject ConfirmCanvas;


    private delegate void ConfirmBehavior();
    private ConfirmBehavior confirmBehavior;

    public bool isPaused { get; private set; }

    private void Start()
    {
        PauseCanvas.SetActive(false);
        ConfirmCanvas.SetActive(false);
        isPaused = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                ClosePauseMenu();
                BuyMenu.Instance.CloseAllMenus();
            }
            else
            {
                OpenPauseMenu();
            }
        }
    }

    private void OpenPauseMenu()
    {
        PauseGame();
        PauseCanvas.SetActive(true);
    }

    public void PauseGame()
    {
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        Time.timeScale = 0;
    }

    public void ClosePauseMenu()
    {
        ResumeGame();
        PauseCanvas.SetActive(false);
        ConfirmCanvas.SetActive(false);
    }

    public void ResumeGame()
    {
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Time.timeScale = 1;
    }

    public void RestartGame()
    {
        confirmBehavior = () => SceneLoader.Instance.ReloadScene();
        ShowConfirmationMenu();
    }

    public void ReturnToMenu()
    {
        confirmBehavior = () => SceneLoader.Instance.LoadMainMenu();
        ShowConfirmationMenu();
    }

    public void QuitGame()
    {
        confirmBehavior = () => SceneLoader.Instance.QuitGame();
        ShowConfirmationMenu();
    }

    public void Confirm()
    {
        confirmBehavior();
    }

    public void Deny()
    {
        ConfirmCanvas.SetActive(false);
        PauseCanvas.SetActive(true);
    }

    private void ShowConfirmationMenu()
    {
        PauseCanvas.SetActive(false);
        ConfirmCanvas.SetActive(true);
    }
}
