using UnityEngine;
using System.Collections;

public class SeparacionFlotes : MonoBehaviour
{
    [SerializeField] private Transform destinationVerde; // Posición para el tag "verde"
    [SerializeField] private Transform destinationNaranja; // Posición para el tag "naranja"
    [SerializeField] private Transform destinationRojo; // Posición para el tag "rojo"
    [SerializeField] private float waitTime = 18f; // Tiempo de espera en segundos antes de mover los granos
    [SerializeField] private float offsetRange = 0.2f; // Rango de desplazamiento aleatorio para evitar que los objetos se sobrepongan

    private void OnTriggerEnter(Collider other)
    {
        // Verificar el tag del objeto que entra en la tolva y moverlo a la posición correspondiente
        if (other.CompareTag("verde"))
        {
            StartCoroutine(MoveGrainAfterDelay(other, destinationVerde));
        }
        else if (other.CompareTag("naranja"))
        {
            StartCoroutine(MoveGrainAfterDelay(other, destinationNaranja));
        }
        else if (other.CompareTag("roja"))
        {
            StartCoroutine(MoveGrainAfterDelay(other, destinationRojo));
        }
    }

    private IEnumerator MoveGrainAfterDelay(Collider grain, Transform destination)
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(waitTime);

        // Agregar un pequeño desplazamiento aleatorio a la posición de destino
        Vector3 randomOffset = new Vector3(
            Random.Range(-offsetRange, offsetRange),
            0f, // Para que no se mueva en el eje Y (mantenerse en el agua)
            Random.Range(-offsetRange, offsetRange)
        );

        // Mover el grano de café a la posición de destino con el desplazamiento aleatorio
        grain.transform.position = destination.position + randomOffset;

        // Aquí no necesitamos más física ya que la colisión de la caja será suficiente
    }
}
