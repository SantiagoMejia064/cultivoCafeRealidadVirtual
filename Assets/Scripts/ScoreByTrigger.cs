using UnityEngine;
using TMPro;

public class ScoreByTrigger : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Puntos")]
    public int score = 0;

    private void Start()
    {
        UpdateScoreUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("roja"))
        {
            score += 10;
        }
        else if (other.CompareTag("naranja"))
        {
            score += 5;
        }
        else if (other.CompareTag("verde"))
        {
            score -= 5;
        }
        else
        {
            return; // Si no tiene tag relevante, no hace nada
        }

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Puntos: " + score;
        }
    }
}
