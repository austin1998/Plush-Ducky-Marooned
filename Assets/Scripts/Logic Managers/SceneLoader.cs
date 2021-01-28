using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Loads scenes using these functions
public class SceneLoader : MonoSingleton<SceneLoader> {
    void Start() {
        //GameObject.Find("StartButton").GetComponentInChildren<Text>().text = "START";
    }

    protected override void Awake() {
        base.Awake();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void LoadScene(string sceneToLoad) {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ReloadScene() {
        //Debug.Log("Called");
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadMainMenu()
    {
        LoadScene("MainMenu");
    }

    public void QuitGame() {
        Application.Quit();
    }

}
