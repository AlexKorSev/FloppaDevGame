using UnityEngine;
using TMPro;
using System;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        // Проверяем, существует ли менеджер (чтобы не было ошибок)
        if (ScoreManager.Instance != null)
        {
            // Обновляем текст на экране каждый кадр. 
            // "F0" округляет число до целого (без запятых)
            scoreText.text = "Счёт: " + ScoreManager.Instance.currentScore.ToString("F0");
        }
        else
        {
            Debug.Log("Менеджер не обнаружен");
        }
    }
}