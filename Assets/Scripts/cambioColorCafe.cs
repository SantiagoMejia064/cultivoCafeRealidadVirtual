using System.Collections;
using UnityEngine;

public class cambioColorCafe : MonoBehaviour
{
    public float tiempoAntesDeCambiar = 5f;  // Tiempo antes de cambiar el material (en segundos)
    public Material materialNuevo;            // Nuevo material para el objeto
    private Material materialOriginal;        // Material original del objeto
    private Renderer rend;                    // Referencia al Renderer del objeto
    private bool haCambiadoMaterial = false;  // Verifica si el material ya fue cambiado

    void Start()
    {
        // Obtener el componente Renderer del objeto
        rend = GetComponent<Renderer>();

        // Guardar el material original del objeto
        if (rend != null)
        {
            materialOriginal = rend.material;
        }
    }

    // Cuando el objeto entra en el trigger
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TriggerArea") && !haCambiadoMaterial) // Verifica si el objeto está entrando en un área específica (con el tag "TriggerArea")
        {
            StartCoroutine(CambiarMaterial());
        }
    }

    IEnumerator CambiarMaterial()
    {
        // Espera el tiempo especificado antes de cambiar el material
        yield return new WaitForSeconds(tiempoAntesDeCambiar);

        // Cambiar el material a uno nuevo si no ha cambiado aún
        if (rend != null && !haCambiadoMaterial)
        {
            rend.material = materialNuevo;  // Cambiar al nuevo material
            haCambiadoMaterial = true; // Evitar que se cambie más de una vez
        }
    }
}
