using UnityEngine;

public class AnimacionesDespedregador : MonoBehaviour
{
    // Referencias a los animadores de los objetos que tienen las animaciones
    public Animator despregadorAnimator;
    public Animator despulpadoraAnimator; // Esta línea es la que faltaba
    public Animator tuboRecirculadorAnimator;
    public Animator tubosConductoresAnimator;

    // Start se llama al principio, puedes usarlo para iniciar las animaciones
    void Start()
    {
        // Iniciar todas las animaciones al mismo tiempo
        StartAllAnimations();
    }

    // Función para iniciar las animaciones
    void StartAllAnimations()
    {
        if (despregadorAnimator != null)
            despregadorAnimator.Play("Despregador"); // Asegúrate de que el nombre coincida con el de tu animación en el Animator

        if (despulpadoraAnimator != null)
            despulpadoraAnimator.Play("Desulpadora"); // Asegúrate de que el nombre coincida con el de tu animación en el Animator

        if (tuboRecirculadorAnimator != null)
            tuboRecirculadorAnimator.Play("TuboRecirculador"); // Asegúrate de que el nombre coincida con el de tu animación en el Animator

        if (tubosConductoresAnimator != null)
            tubosConductoresAnimator.Play("Tubos Conductores");
    }
}
