using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events; 

public class FuncionTolva : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
    [SerializeField] private Transform wheelTransform;

    public UnityEvent<float> OnWheelRotated;

    private float currentAngle = 0.0f;
    private float previousAngle = 0f;

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

    private float CalculateTotalAngle()
    {
        float angleSum = 0f;
        int count = interactorsSelecting.Count;
        if (count == 0) return previousAngle;

        Vector3 axis     = transform.right;  // eje de giro (local X)
        Vector3 reference = transform.up;    // referencia “arriba” de la rueda

        foreach (var interactor in interactorsSelecting)
        {
            // Vector desde el centro de la rueda hasta la mano en espacio mundo
            Vector3 toInteractor = interactor.transform.position - transform.position;

            // Proyectar en el plano perpendicular al eje para ignorar la componente a lo largo del eje
            Vector3 projected = Vector3.ProjectOnPlane(toInteractor, axis);

            // Calcular el ángulo entre la referencia y el vector proyectado, alrededor del eje
            float angle = Vector3.SignedAngle(reference, projected, axis);
            angleSum += angle;
        }

        return angleSum / count;
    }


    private void RotateWheel()
    {
        // Calcular el ángulo total actual
        float totalAngle = FindWheelAngle();

        // Diferencia respecto al ángulo calculado en el fotograma anterior
        float angleDifference = currentAngle - totalAngle;

        // Girar alrededor del eje local X (transform.right) para que se vea como una rueda real.
        // Si ves que el sentido es inverso al deseado, cambia el signo de angleDifference.
        wheelTransform.Rotate(transform.right, angleDifference, Space.World);

        // Almacenar el ángulo para el siguiente fotograma
        currentAngle = totalAngle;
        OnWheelRotated?.Invoke(angleDifference);
    }

    private float FindWheelAngle()
    {
        float totalAngle = 0f;

        // Sumar el ángulo de cada mano que esté agarrando el volante
        foreach (UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor interactor in interactorsSelecting)
        {
            Vector2 direction = FindLocalPoint(interactor.transform.position);
            totalAngle += ConvertToAngle(direction) * FindRotationSensitivity();
        }

        return totalAngle;
    }

    private Vector2 FindLocalPoint(Vector3 position)
    {
        // Convertir la posición del interactor al espacio local
        Vector3 localPos = transform.InverseTransformPoint(position);

        // Proyectar en el plano Y-Z y normalizar.  X se descarta porque estamos rotando en torno a X.
        return new Vector2(localPos.z, localPos.y).normalized;
    }

    private float ConvertToAngle(Vector2 direction)
    {
        // Medir el ángulo firmado respecto al eje Y local en el plano YZ
        return Vector2.SignedAngle(Vector2.up, direction);
    }

    private float FindRotationSensitivity()
    {
        // Si hay dos manos, sensibilidad 1/2; con una sola mano es 1
        return 1.0f / interactorsSelecting.Count;
    }
}
