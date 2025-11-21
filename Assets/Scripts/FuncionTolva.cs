using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

public class FuncionTolva : UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable
{
    [SerializeField] private Transform wheelTransform;
    [SerializeField] private GameObject panel; 
    public Animator Puerta1anim; 
    [SerializeField] private float angleThreshold = 90.0f; 

    public UnityEvent<float> OnWheelRotated;

    private float currentAngle = 0.0f;
    private float totalRotation = 0.0f; 
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
        float totalAngle = FindWheelAngle();

        float angleDifference = totalAngle - currentAngle;
        totalRotation += angleDifference;

        wheelTransform.Rotate(Vector3.forward, angleDifference, Space.World);

        currentAngle = totalAngle;

        OnWheelRotated?.Invoke(angleDifference);

        if (Mathf.Abs(totalRotation) >= angleThreshold && panel != null)
        {
            panel.SetActive(true); 
            Puerta1anim.Play("AbrirPuerta1");
        }
    }

    private float FindWheelAngle()
    {
        float totalAngle = 0;
        foreach (UnityEngine.XR.Interaction.Toolkit.Interactors.IXRSelectInteractor interactor in interactorsSelecting)
        {
            Vector2 direction = FindLocalPoint(interactor.transform.position);
            totalAngle += ConvertToAngle(direction) * FindRotationSensitivity();
        }

        return totalAngle;
    }

    private Vector2 FindLocalPoint(Vector3 position)
    {
        return transform.InverseTransformPoint(position).normalized;
    }

    private float ConvertToAngle(Vector2 direction)
    {
        return Vector2.SignedAngle(Vector2.up, direction);
    }

    private float FindRotationSensitivity()
    {
        return 1.0f / interactorsSelecting.Count;
    }

    private void Start()
    {
        if (panel != null)
        {
            panel.SetActive(false); 
        }
    }
}
