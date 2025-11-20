using UnityEngine;
using System.Collections;

public class SeparacionFlotes : MonoBehaviour
{
    [SerializeField] private Transform destinationPosition; // La posición a la que se moverán los granos de café
    [SerializeField] private float waitTime = 3f; // Tiempo de espera en segundos antes de mover los granos

    private void OnTriggerEnter(Collider other)
    {
        // Verificar si el objeto que entra en la tolva tiene el tag "verde"
        if (other.CompareTag("verde"))
        {
            // Llamar a la corutina para esperar y mover el grano
            StartCoroutine(MoveGrainAfterDelay(other));
        }
    }

    private IEnumerator MoveGrainAfterDelay(Collider grain)
    {
        // Esperar el tiempo especificado
        yield return new WaitForSeconds(waitTime);

        // Mover el grano de café a la posición de destino
        grain.transform.position = destinationPosition.position;

    }
}
