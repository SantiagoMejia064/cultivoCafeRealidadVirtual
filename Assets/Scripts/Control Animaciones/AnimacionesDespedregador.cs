using UnityEngine;

public class AnimacionesDespedregador : MonoBehaviour
{
    // Referencias a los animadores de los objetos que tienen las animaciones
    public Animator despregadorAnimator;
    public Animator despulpadoraAnimator;
    public Animator despulpadoraAnimator2;
    public Animator tuboRecirculadorAnimator;
    public Animator tubosConductoresAnimator;

    // Referencia al AudioSource
    public AudioSource sonido;

    // Función para iniciar las animaciones
    void StartAllAnimations()
    {
        tubosConductoresAnimator.Play("TubosConductores");
        despulpadoraAnimator.Play("Despulpadora");
        despulpadoraAnimator2.Play("Despulpadora");
        despregadorAnimator.Play("Despedregador Principal");
        tuboRecirculadorAnimator.Play("Tubo recirculador");
    }

    // Función pública para iniciar animaciones y sonido
    public void ActivateAnimationsAndSound()
    {
        // Activar todas las animaciones
        StartAllAnimations();

        // Reproducir sonido si está asignado
        if (sonido != null)
            sonido.Play();
    }
}
