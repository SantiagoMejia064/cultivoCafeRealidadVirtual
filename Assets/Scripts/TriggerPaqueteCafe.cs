using UnityEngine;

public class TriggerPaqueteCafe : MonoBehaviour
{
    [Header("Referencias")]
    public AudioSource audioSource;  // Componente AudioSource para el sonido
    public GameObject canvas;           // Canvas que se mostrará cuando el trigger ocurra
    public int maxPaquetes = 1  ;    // Número máximo de paquetes que se pueden detectar
    private int currentPaquetes = 0; // Contador de los paquetes detectados

    private void Start()
    {
        // Asegurarse que el canvas esté oculto al inicio
        if (canvas != null)
        {
            canvas.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra tiene el tag "PaqueteCafe"
        if (other.CompareTag("PaqueteCafe"))
        {
            // Incrementar el contador de paquetes detectados
            currentPaquetes++;

            // Verificar si no hemos alcanzado el número máximo de paquetes
            if (currentPaquetes == maxPaquetes)
            {
                // Mostrar el canvas
                if (canvas != null)
                {
                    canvas.gameObject.SetActive(true);
                }

                // Reproducir el sonido
                if (audioSource != null && !audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }
    }
}
