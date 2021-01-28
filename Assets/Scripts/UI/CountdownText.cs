using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountdownText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countdownText;
    [SerializeField] TextMeshProUGUI promptText;

    // Start is called before the first frame update
    void Start()
    {
        Rounds.OnCountdownChanged += UpdateCountdownText;
        Rounds.OnCountdownStarted += ActivateObject;
        Rounds.OnCountdownFinished += DeactivateObject;
        countdownText.gameObject.SetActive(false);
        promptText.gameObject.SetActive(false);
    }

    private void UpdateCountdownText(int value)
    {
        countdownText.text = value.ToString();
    }

    private void ActivateObject()
    {
        countdownText.gameObject.SetActive(true);
        promptText.gameObject.SetActive(true);
    }

    private void DeactivateObject()
    {
        countdownText.gameObject.SetActive(false);
        promptText.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        Rounds.OnCountdownChanged -= UpdateCountdownText;
        Rounds.OnCountdownStarted -= ActivateObject;
        Rounds.OnCountdownFinished -= DeactivateObject;
    }
}
