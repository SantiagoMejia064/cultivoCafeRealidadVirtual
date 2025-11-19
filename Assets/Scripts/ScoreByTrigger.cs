using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScoreByTrigger : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Puntos")]
    public int score = 0;

    // Utilizamos un HashSet para evitar contar las mismas colisiones múltiples veces
    private HashSet<Collider> objetosContabilizados = new HashSet<Collider>();

    private void Start()
    {
        UpdateScoreUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto ya ha sido contabilizado
        if (objetosContabilizados.Contains(other)) return;

        // Si el objeto tiene un tag relevante, le sumamos o restamos puntos
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

        // Añadimos el objeto al HashSet para que no sea contado de nuevo
        objetosContabilizados.Add(other);

        // Actualiza la UI de los puntos
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
