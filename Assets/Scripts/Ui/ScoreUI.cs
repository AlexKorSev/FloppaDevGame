using UnityEngine;
using TMPro;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (ScoreManager.Instance != null)
        {
            scoreText.text = ScoreManager.Instance.coinsCollected.ToString();
        }
        else
        {
            Debug.Log("Менеджер не обнаружен");
        }
    }
}