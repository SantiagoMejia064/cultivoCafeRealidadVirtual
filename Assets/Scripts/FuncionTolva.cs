using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class FuncionTolva : UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable
{
    [SerializeField] private Transform wheelTransform;
    [SerializeField] private GameObject panel; // El panel que se va a mostrar
    [SerializeField] private float angleThreshold = 90.0f; // Ángulo en el que aparece el panel

    public UnityEvent<float> OnWheelRotated;

    private float currentAngle = 0.0f;
    private float totalRotation = 0.0f; // Total acumulado de la rotación

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);
        currentAngle = FindWheelAngle();
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);
        currentAngle = FindWheelAngle();
    }

    public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        base.ProcessInteractable(updatePhase);

        if (updatePhase == XRInteractionUpdateOrder.UpdatePhase.Dynamic)
        {
            if (isSelected)
                RotateWheel();
        }
    }

    private void RotateWheel()
    {
        // Obtener el ángulo total de la rueda
        float totalAngle = FindWheelAngle();

        // Calcular la diferencia de ángulo (diferencia entre el ángulo actual y el anterior)
        float angleDifference = totalAngle - currentAngle;

        // Asegurarnos de acumular correctamente el ángulo total
        totalRotation += angleDifference;

        // Aplicar la rotación a la rueda
        wheelTransform.Rotate(Vector3.forward, angleDifference, Space.World);

        // Almacenar el ángulo actual para la siguiente iteración
        currentAngle = totalAngle;

        // Llamar al evento OnWheelRotated
        OnWheelRotated?.Invoke(angleDifference);

        // Verificar si el ángulo acumulado ha superado el umbral
        if (Mathf.Abs(totalRotation) >= angleThreshold && panel != null)
        {
            panel.SetActive(true); // Activar el panel cuando se haya superado el umbral
        }
    }

    private float FindWheelAngle()
    {
        float totalAngle = 0;

        // Combinar las direcciones de los interactores actuales
        foreach (UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor interactor in interactorsSelecting)
        {
            Vector2 direction = FindLocalPoint(interactor.transform.position);
            totalAngle += ConvertToAngle(direction) * FindRotationSensitivity();
        }

        return totalAngle;
    }

    private Vector2 FindLocalPoint(Vector3 position)
    {
        // Convertir las posiciones de la mano a local, para poder encontrar el ángulo más fácilmente
        return transform.InverseTransformPoint(position).normalized;
    }

    private float ConvertToAngle(Vector2 direction)
    {
        // Usar una dirección constante para encontrar el ángulo
        return Vector2.SignedAngle(Vector2.up, direction);
    }

    private float FindRotationSensitivity()
    {
        // Usar una sensibilidad menor con dos manos
        return 1.0f / interactorsSelecting.Count;
    }

    // Añadir un método Start para asegurarse de que el panel esté desactivado al inicio
    private void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false); // Asegúrate de que el panel está desactivado al inicio
        }
    }
}
