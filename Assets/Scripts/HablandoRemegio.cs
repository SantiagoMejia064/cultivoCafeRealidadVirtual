using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con el botón

public class HablandoRemegio : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Animator animator;

    [Header("Parámetro del Animator")]
    [SerializeField] private string animationBoolName = "isTalking";

    // Para saber si cambió el estado del audio
    private bool wasPlaying = false;

    [Header("Botón de Omitir")]
    [SerializeField] private Button omitirButton; // Referencia al botón

    private void Reset()
    {
        // Autoasignar referencias si el script se añade desde el inspector
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (omitirButton == null)
            omitirButton = GetComponent<Button>(); // Asume que el botón está en el mismo GameObject
    }

    private void Update()
    {
        if (audioSource == null || animator == null) return;

        bool isPlaying = audioSource.isPlaying;

        // Solo actuamos cuando cambia el estado (para no spamear SetBool)
        if (isPlaying != wasPlaying)
        {
            wasPlaying = isPlaying;

            if (isPlaying)
            {
                // Empezó a sonar el audio → activar animación
                animator.SetBool(animationBoolName, true);
            }
            else
            {
                // Se terminó (o se detuvo) el audio → desactivar animación
                animator.SetBool(animationBoolName, false);
            }
        }
    }

    // Función para omitir (mute el audio, cambia el bool a falso, y deshabilita el botón)
    public void Omitir()
    {
        if (audioSource != null)
        {
            audioSource.mute = true; // Mutea el AudioSource
        }

        if (animator != null)
        {
            animator.SetBool(animationBoolName, false); // Desactiva la animación
        }

        // También aseguramos que el audio se detenga si es necesario
        if (audioSource.isPlaying)
        {
            audioSource.Stop(); // Detiene el audio si está sonando
        }

        // Deshabilitar el botón de omitir después de presionarlo
        if (omitirButton != null)
        {
            omitirButton.interactable = false; // Deshabilita el botón
        }
    }
}
