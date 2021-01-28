using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        GameLogic.OnScoreUpdated += SetScoreText;
    }

    private void SetScoreText(int score)
    {
        scoreText.text = score + " Points";
    }

    private void OnDestroy()
    {
        GameLogic.OnScoreUpdated -= SetScoreText;
    }
}
