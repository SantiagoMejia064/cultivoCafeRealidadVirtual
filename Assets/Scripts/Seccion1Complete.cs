using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class Seccion1Complete : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform tubo; // El tubo que debe vibrar
    [SerializeField] private float tiempoVibracion = 0.5f; // Tiempo de duración de la vibración
    [SerializeField] private float rangoVibracion = 0.05f; // Rango del movimiento de vibración

    private bool isVibrando = false;  // Control para saber si ya estamos vibrando
    private bool funcionando = false; // Control para asegurarnos de que todo solo se active una vez

    // Este método se llamará cuando el botón sea presionado
    public void ActivarFuncionamiento(SelectEnterEventArgs args)
    {
        if (!funcionando) // Solo iniciamos el funcionamiento si no ha comenzado antes
        {
            funcionando = true; // Marcamos que ya estamos en funcionamiento
            StartCoroutine(VibrarTubo());  // Iniciamos la vibración
            DestruirFrutos(); // Iniciamos la destrucción de frutos
        }
    }

    // Método para destruir los frutos con los tags "roja" o "naranja"
    private void DestruirFrutos()
    {
        // Obtener todos los objetos con el tag "roja" o "naranja" en la escena
        GameObject[] frutos = GameObject.FindGameObjectsWithTag("roja");

        // Destruir todos los frutos con el tag "roja"
        foreach (GameObject fruto in frutos)
        {
            Destroy(fruto); // Destruir el objeto inmediatamente
        }

        // Hacer lo mismo para los frutos con el tag "naranja"
        frutos = GameObject.FindGameObjectsWithTag("naranja");

        // Destruir todos los frutos con el tag "naranja"
        foreach (GameObject fruto in frutos)
        {
            Destroy(fruto); // Destruir el objeto inmediatamente
        }
    }

    // Método para vibrar el tubo
    private IEnumerator VibrarTubo()
    {
        isVibrando = true; // Establecer que estamos en el proceso de vibración
        Vector3 posicionOriginal = tubo.position;

        for (float t = 0; t < tiempoVibracion; t += Time.deltaTime)
        {
            // Desplazar el tubo de forma aleatoria dentro de un pequeño rango
            float desplazamientoX = Random.Range(-rangoVibracion, rangoVibracion);
            float desplazamientoZ = Random.Range(-rangoVibracion, rangoVibracion);

            tubo.position = new Vector3(posicionOriginal.x + desplazamientoX, posicionOriginal.y, posicionOriginal.z + desplazamientoZ);
            yield return null;
        }

        // Asegurarse de que el tubo vuelva a su posición original al finalizar la vibración
        tubo.position = posicionOriginal;
        isVibrando = false; // Terminamos el proceso de vibración
    }
}
