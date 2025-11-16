using UnityEngine;

public class HablandoRemegio : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Animator animator;

    [Header("Parámetro del Animator")]
    [SerializeField] private string animationBoolName = "isTalking";

    // Para saber si cambió el estado del audio
    private bool wasPlaying = false;

    private void Reset()
    {
        // Autoasignar referencias si el script se añade desde el inspector
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (animator == null)
            animator = GetComponent<Animator>();
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
}
