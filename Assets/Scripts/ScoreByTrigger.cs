using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ScoreByTrigger : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Puntos")]
    public int score = 0;

    private HashSet<Collider> objetosContabilizados = new HashSet<Collider>();

    [Header("Referencia a BasketSocket")]
    private BasketSocket basketSocket;  // Referencia al script BasketSocket

    private void Start()
    {
        // Si no está asignado en inspector, buscar automáticamente en el padre o raíz
        if (basketSocket == null)
            basketSocket = GetComponentInParent<BasketSocket>();

        UpdateScoreUI();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si la canasta está llena
        if (basketSocket != null && basketSocket.fruitCount >= basketSocket.maxFruitCount)
        {
            // Canasta llena, no sumar puntos
            return;
        }

        // Verificar si el objeto ya ha sido contabilizado
        if (objetosContabilizados.Contains(other))
            return;

        // Sumar o restar puntos según tag
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
            return; // Tag no relevante, salir
        }

        objetosContabilizados.Add(other);
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }
    }
}
