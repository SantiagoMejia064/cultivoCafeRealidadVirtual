using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.UIElements;

public class ScoreByTrigger : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;

    [Header("Puntos")]
    public int score = 0;

    private HashSet<Collider> objetosContabilizados = new HashSet<Collider>();

    [Header("Referencia a BasketSocket")]
    private BasketSocket basketSocket;  // Referencia al script BasketSocket

    [SerializeField] private GameObject logroRecolectorEntusiasta;

    private void Start()
    {
        // Si no est� asignado en inspector, buscar autom�ticamente en el padre o ra�z
        if (basketSocket == null)
            basketSocket = GetComponentInParent<BasketSocket>();

        UpdateScoreUI();
    }

    void Update()
    {
        if(score >= 100)
        {
            logroRecolectorEntusiasta.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si la canasta est� llena
        if (basketSocket != null && basketSocket.fruitCount >= basketSocket.maxFruitCount)
        {
            // Canasta llena, no sumar puntos
            return;
        }

        // Verificar si el objeto ya ha sido contabilizado
        if (objetosContabilizados.Contains(other))
            return;

        // Sumar o restar puntos seg�n tag
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
