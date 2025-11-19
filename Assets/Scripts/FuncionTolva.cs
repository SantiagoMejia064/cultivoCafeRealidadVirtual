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
        // Convertir esa dirección a un ángulo, luego rotación
        float totalAngle = FindWheelAngle();

        // Aplicar la diferencia de ángulo a la rueda
        float angleDifference = currentAngle - totalAngle;
        wheelTransform.Rotate(transform.forward, -angleDifference, Space.World);

        // Almacenar el ángulo para el siguiente ciclo
        currentAngle = totalAngle;
        OnWheelRotated?.Invoke(angleDifference);

        // Verificar si el ángulo alcanzó el umbral para mostrar el panel
        if (Mathf.Abs(currentAngle) >= angleThreshold && panel != null)
        {
            panel.SetActive(true); // Activar el panel
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
