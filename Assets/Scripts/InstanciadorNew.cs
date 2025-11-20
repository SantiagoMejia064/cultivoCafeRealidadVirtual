using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class InstanciadorNew : MonoBehaviour
{
    public XRLever palanca;                // Referencia a la palanca
    public GameObject prefab;              // Prefab a instanciar
    public Transform puntoInstanciacion;   // Donde instanciar los objetos
    public float intervalo = 0.5f;         // Tiempo entre instanciaciones

    private float timer = 0f;              // Temporizador para el intervalo

    void Update()
    {
        // Verificar si la palanca está activada
        if (palanca.value)
        {
            // Contabilizar el tiempo
            timer += Time.deltaTime;

            // Si ha pasado el intervalo, instanciamos el objeto
            if (timer >= intervalo)
            {
                InstanciarObjeto();
                timer = 0f;  // Resetear el temporizador
            }
        }

    }

    // Método para instanciar el prefab en la posición deseada
    void InstanciarObjeto()
    {
        Instantiate(prefab, puntoInstanciacion.position, puntoInstanciacion.rotation);
    }
}
