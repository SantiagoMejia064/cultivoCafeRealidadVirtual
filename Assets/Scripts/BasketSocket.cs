using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BasketSocket : MonoBehaviour
{
    [Header("Área de recepción (este mismo objeto con trigger)")]
    public SphereCollider socketArea;

    [Header("Padre donde se guardan los frutos")]
    public Transform storedFruitsParent;

    [Header("Configuración visual")]
    public float innerRadius = 0.2f;    // radio interno donde se acomodan
    public float fruitsHeight = 0.0f;   // altura local dentro de la canasta

    private List<Transform> storedFruits = new List<Transform>();

    [Header("Configuración del input")]
    public InputActionProperty toggleButton; // Acción para el toggle button

    private bool toggleState = false;  // Estado del toggle (activado/desactivado)

    [Header("Contador de frutos")]
    public int fruitCount = 0; // Contador de frutos en la canasta

    [Header("Configuración del máximo número de frutos")]
    [Tooltip("Número máximo de frutos que se pueden almacenar en la canasta")]
    public int maxFruitCount = 20; // Máximo número de frutos que se pueden almacenar

    private void Reset()
    {
        socketArea = GetComponent<SphereCollider>();
        if (socketArea != null)
            socketArea.isTrigger = true;
    }

    private void Update()
    {
        // Verificar si el botón está presionado
        bool isPressed = toggleButton.action.IsPressed();

        // Si presionas B, liberar frutos y desactivar socket
        if (isPressed && !toggleState)
        {
            toggleState = true;
            FreeFruits();
            DisableSocket();
        }
        // Si dejas de presionar B, reactivar socket
        else if (!isPressed && toggleState)
        {
            toggleState = false;
            EnableSocket();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Solo almacenar frutos si el socket está activo
        if (socketArea == null || !socketArea.enabled) return;

        CoffeeFruit fruit = other.GetComponentInParent<CoffeeFruit>();
        if (fruit == null) return;

        StoreFruit(fruit);
    }

    private void StoreFruit(CoffeeFruit fruit)
    {
        if (fruitCount >= maxFruitCount)
        {
            Debug.Log("Máximo número de frutos alcanzado.");
            return;
        }

        Transform t = fruit.transform;
        Rigidbody rb = fruit.GetComponent<Rigidbody>();
        var grab = fruit.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // 1. Activar isKinematic y desactivar gravedad
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // 2. Opcional: ya no se puede agarrar (desactiva grab interactable)
        if (grab != null)
            grab.enabled = false;

        // 3. Emparentar y posicionar
        if (storedFruitsParent == null)
            storedFruitsParent = transform;

        t.SetParent(storedFruitsParent);
        Vector3 localPos = GetRandomLocalPosition();
        t.localPosition = localPos;
        t.localRotation = Random.rotation;

        // 4. Añadir a lista y contador
        storedFruits.Add(t);
        fruitCount++;

        // 5. Si se llegó al límite, desactiva el socket
        if (fruitCount >= maxFruitCount)
        {
            DisableSocket();
            Debug.Log("El socket ha sido desactivado.");
        }
    }

    private Vector3 GetRandomLocalPosition()
    {
        Vector2 insideCircle = Random.insideUnitCircle * innerRadius;
        return new Vector3(insideCircle.x, fruitsHeight, insideCircle.y);
    }

    // Libera los frutos (quita el parenting y activa la física)
    private void FreeFruits()
    {
        for (int i = storedFruits.Count - 1; i >= 0; i--)
        {
            Transform fruit = storedFruits[i];
            if (fruit != null)
            {
                Rigidbody rb = fruit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb.useGravity = true;
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
                fruit.SetParent(null);
            }
        }

        storedFruits.Clear();
        fruitCount = 0;
        // No activar el socket aquí, solo cuando se suelte B
    }

    // Deshabilita el socket para que no reciba frutos nuevos
    private void DisableSocket()
    {
        if (socketArea != null)
            socketArea.enabled = false;
    }

    // Habilita el socket para volver a recibir frutos
    private void EnableSocket()
    {
        if (socketArea != null)
            socketArea.enabled = true;
    }
}
