using UnityEngine;
using System.Collections;
public class BotonCascada : MonoBehaviour
{
    [Header("Generación de Frutos")]
    [SerializeField] private GameObject coffeeFruitPrefab; // Prefab del fruto de café
    [SerializeField] private BoxCollider dropArea;         // BoxCollider donde los frutos caerán
    [SerializeField] private float dropHeight = 5f;        // Altura desde donde caen los frutos
    [SerializeField] private float dropInterval = 0.1f;    // Intervalo entre cada fruto

    [Header("Sonido")]
    [SerializeField] private AudioSource sonidoCascada;    // Sonido de la cascada de frutos

    private bool generandoFrutos = false;

    public void GenerarFrutos()
    {
        //sonidoCascada.Play(); // Reproduce el sonido de la cascada

        // Genera frutos en rápida sucesión (como una cascada)
        for (int i = 0; i < 30; i++) // 30 frutos caen como ejemplo, ajusta el número
        {
            // Obtener el centro del BoxCollider y calcular una posición aleatoria dentro de los límites
            Vector3 boxCenter = dropArea.bounds.center;
            float randomX = Random.Range(boxCenter.x - dropArea.bounds.extents.x, boxCenter.x + dropArea.bounds.extents.x);
            float randomZ = Random.Range(boxCenter.z - dropArea.bounds.extents.z, boxCenter.z + dropArea.bounds.extents.z);

            // La posición Y será la altura de caída
            Vector3 dropPosition = new Vector3(randomX, dropHeight, randomZ);

            // Instanciar el fruto en la posición de caída
            GameObject fruit = Instantiate(coffeeFruitPrefab, dropPosition, Quaternion.identity);

            // Asegurarse de que el Rigidbody esté activo para la caída
            Rigidbody fruitRb = fruit.GetComponent<Rigidbody>();
            if (fruitRb != null)
            {
                // Asegurarse de que la gravedad esté activada
                fruitRb.useGravity = true;
            }

            // Esperar antes de generar el siguiente fruto (simula la cascada)
            StartCoroutine(EsperarEntreFrutos(dropInterval));
        }
    }

    private IEnumerator EsperarEntreFrutos(float intervalo)
    {
        // Pausa entre la caída de los frutos
        yield return new WaitForSeconds(intervalo);
    }

    public void AccionCascada()
    {
        if (!generandoFrutos)
        {
            generandoFrutos = true;
            GenerarFrutos();
        }
        else
        {
            // Si la cascada ya está ocurriendo, no hacer nada
            // Aquí podrías agregar la opción de detenerla si quieres
        }
    }
}
