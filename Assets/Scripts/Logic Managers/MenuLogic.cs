using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static LoadingOptions;

public class MenuLogic : MonoBehaviour
{
    [SerializeField] Canvas PrepareCanvas;
    [SerializeField] Canvas TitleCanvas;
    [SerializeField] Text TimerText;
    [SerializeField] string nextSceneName;

    [Header("Difficulty Select")]
    [SerializeField] Text difficultyText;
    [SerializeField] Color easyColor;
    [SerializeField] Color normalColor;
    [SerializeField] Color hardColor;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        ShowDifficulty();
    }
    public void StartButtonClicked()
    {
        TitleCanvas.gameObject.SetActive(false);
        PrepareCanvas.gameObject.SetActive(true);
    }
    public void PrepareButtonClicked() {
        Time.timeScale = 1;
        SceneLoader.Instance.LoadScene("IslandGame");
    }

    public void IncrementDifficulty()
    {
        switch(difficultyLevel)
        {
            case DifficultyLevel.Easy:
                difficultyLevel = DifficultyLevel.Normal;
                break;
            case DifficultyLevel.Normal:
                difficultyLevel = DifficultyLevel.Hard;
                break;
            case DifficultyLevel.Hard:
                difficultyLevel = DifficultyLevel.Easy;
                break;
        }
        ShowDifficulty();
    }

    private void ShowDifficulty()
    {
        switch(difficultyLevel)
        {
            case DifficultyLevel.Easy:
                difficultyText.color = easyColor;
                difficultyText.text = "EASY";
                difficultyModifier = .8f;
                break;
            case DifficultyLevel.Normal:
                difficultyText.color = normalColor;
                difficultyText.text = "NORMAL";
                difficultyModifier = 1f;
                break;
            case DifficultyLevel.Hard:
                difficultyText.color = hardColor;
                difficultyText.text = "HARD";
                difficultyModifier = 1.2f;
                break;
        }
    }
}
