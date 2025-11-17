using System.Collections.Generic;
using UnityEngine;


public class BasketSocket : MonoBehaviour
{
    [Header("�rea de recepci�n (este mismo objeto con trigger)")]
    public SphereCollider socketArea;

    [Header("Padre donde se guardan los frutos")]
    public Transform storedFruitsParent;

    [Header("Configuraci�n visual")]
    public float innerRadius = 0.2f;    // radio interno donde se acomodan
    public float fruitsHeight = 0.0f;   // altura local dentro de la canasta

    private List<Transform> storedFruits = new List<Transform>();

    private void Reset()
    {
        socketArea = GetComponent<SphereCollider>();
        if (socketArea != null)
            socketArea.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Buscamos el CoffeeFruit en el objeto o sus padres
        CoffeeFruit fruit = other.GetComponentInParent<CoffeeFruit>();
        if (fruit == null) return;

        StoreFruit(fruit);
    }

    private void StoreFruit(CoffeeFruit fruit)
    {
        Transform t = fruit.transform;
        Rigidbody rb = fruit.GetComponent<Rigidbody>();
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab = fruit.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        // 1. Desactivar f�sica para que no se salga
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // 2. Opcional: ya no se puede agarrar de nuevo (si quieres que s�, comenta esto)
        if (grab != null)
        {
            grab.enabled = false;
        }

        // 3. Hacerlo hijo del padre interno de la canasta
        if (storedFruitsParent == null)
            storedFruitsParent = transform;

        t.SetParent(storedFruitsParent);

        // 4. Colocarlo en una posici�n aleatoria dentro de la canasta
        Vector3 localPos = GetRandomLocalPosition();
        t.localPosition = localPos;
        t.localRotation = Random.rotation;

        storedFruits.Add(t);
    }

    private Vector3 GetRandomLocalPosition()
    {
        // Punto aleatorio en un c�rculo (X-Z) dentro del radio
        Vector2 insideCircle = Random.insideUnitCircle * innerRadius;
        return new Vector3(insideCircle.x, fruitsHeight, insideCircle.y);
    }
}
