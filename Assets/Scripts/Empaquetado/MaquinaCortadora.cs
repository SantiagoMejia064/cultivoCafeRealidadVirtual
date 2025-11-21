using UnityEngine;

public class MaquinaCortadora : MonoBehaviour
{
    public GameObject paqueteCaféPrefab;        // Prefab para los cafés sin cortar
    public GameObject paqueteCaféCortadoPrefab; // Prefab para los cafés cortados
    public Transform transformSubnormal;        // Transform donde se instancian los cafés
    public Transform segundaPosicion;          // Transform donde se instancian los cafés cortados

    // Método para instanciar un lote de cafés en la posición exacta
    public void InstanciarLoteDeCafe()
    {
        if (paqueteCaféPrefab != null && transformSubnormal != null)
        {
            // Usamos la posición global exacta de transformSubnormal sin alterar nada
            Vector3 nuevaPosicion = transformSubnormal.position;  // Mantener la posición global sin alteración
            Instantiate(paqueteCaféPrefab, nuevaPosicion, Quaternion.identity);  // Instanciamos con la rotación predeterminada
        }
        else
        {
            Debug.LogError("Los prefabs no están asignados correctamente en el Inspector.");
        }
    }

    // Se llama cuando otro objeto entra en el collider de la máquina
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PaqueteCafe"))  // Si el objeto es un paquete de café
        {
            // Destruir el paquete original (café sin cortar)
            Destroy(other.gameObject);

            // Instanciar el paquete de café cortado en la misma posición exacta
            if (segundaPosicion != null)
            {
                // Usamos la posición exacta de segundaPosicion sin alterarla
                Vector3 nuevaPosicion = segundaPosicion.position;  // Mantener la posición global exacta para la segunda posición
                Instantiate(paqueteCaféCortadoPrefab, nuevaPosicion, Quaternion.identity);  // Instanciamos el café cortado
            }
        }
    }

    // Para realizar una prueba inicial
    void Start()
    {
        InstanciarLoteDeCafe();  // Instanciar un lote de cafés sin cortar en la posición inicial al inicio
    }
}
