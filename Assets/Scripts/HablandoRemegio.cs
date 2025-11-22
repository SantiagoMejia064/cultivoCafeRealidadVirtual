using UnityEngine;
using UnityEngine.UI;

public class HablandoRemegio : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Animator animator;

    [Header("Parámetro del Animator")]
    [SerializeField] private string animationBoolName = "isTalking";
    public GameObject siguiente;
    public GameObject actual;

    // Para saber si cambió el estado del audio
    private bool wasPlaying = false;

    [Header("Botón de Omitir")]
    [SerializeField] private Button omitirButton;

    private void Reset()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (animator == null)
            animator = GetComponent<Animator>();

        if (omitirButton == null)
            omitirButton = GetComponent<Button>();
    }

    private void Update()
    {
        if (audioSource == null || animator == null) return;

        bool isPlaying = audioSource.isPlaying;

        if (isPlaying != wasPlaying)
        {
            wasPlaying = isPlaying;

            if (isPlaying)
            {
                animator.SetBool(animationBoolName, true);
            }
            else
            {
                animator.SetBool(animationBoolName, false);
            }
        }

        // Deshabilitar el botón cuando el audio termine de sonar
        if (!audioSource.isPlaying && omitirButton != null && omitirButton.interactable)
        {
            omitirButton.interactable = false; // Deshabilita el botón cuando el audio termine
            siguiente.SetActive(true);
            actual.SetActive(false);
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
            siguiente.SetActive(true);
            actual.SetActive(false);
        }
    }
}
