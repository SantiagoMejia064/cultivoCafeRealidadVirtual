using UnityEngine;
using System.Collections;

public class SeparacionFlotes : MonoBehaviour
{
    [SerializeField] private Transform destinationVerde; 
    [SerializeField] private Transform destinationNaranja; 
    [SerializeField] private Transform destinationRojo; 
    [SerializeField] private float waitTime = 18f; 
    [SerializeField] private float offsetRange = 0.2f; 

    private void OnTriggerEnter(Collider other)
    {
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
        yield return new WaitForSeconds(waitTime);

        Vector3 randomOffset = new Vector3(
            Random.Range(-offsetRange, offsetRange),
            0f, 
            Random.Range(-offsetRange, offsetRange)
        );
        grain.transform.position = destination.position + randomOffset;
    }
}
