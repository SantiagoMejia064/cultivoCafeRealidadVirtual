using System.Collections;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class Instanciador : MonoBehaviour
{
    [Header("Objeto de palanca")]
    public XRLever palanca;            // Referencia a la palanca para activación

    [Header("Prefabs y puntos de instanciación")]
    public GameObject prefab;          // Prefab a instanciar
    public Transform puntoInstanciacion;  // Punto donde instanciar los objetos
    public GameObject impactEffect;    // Efecto cuando la gota impacta
    public Transform puntoImpacto;     // Nuevo punto para los efectos de impacto
    public GameObject efectoExtra;     // El efecto adicional que se activará con la palanca
    public GameObject boton;

    [Header("Tiempo y Rango")]
    public float intervalo = 0.5f;     // Tiempo entre instanciaciones
    public float impactRange = 5f;     // Rango del área donde generaremos los efectos
    public float impactLifetime = 2f;  // Tiempo de vida del efecto de impacto antes de ser destruido
    private float timer = 0f;          // Temporizador para controlar la frecuencia de instanciación
    public float cantidadCafe = 0f;

    void Update()
    {
        if (cantidadCafe >= 10f)
        {
            boton.SetActive(true);
        }
        else
        {
            boton.SetActive(false);
        }

        // Comprobamos si la palanca está activada o desactivada
        if (palanca.value) // Si la palanca está activada
        {
            // Activar el efecto extra si la palanca está activada
            if (efectoExtra != null)
                efectoExtra.SetActive(true);

            timer += Time.deltaTime;

            if (timer >= intervalo)  // Si ha pasado el intervalo
            {
                InstanciarObjeto();
                InstanciarImpactos();   // Instanciamos los efectos de impacto en el nuevo punto
                timer = 0f;         // Resetear el temporizador
            }
        }
        else
        {
            // Desactivar el efecto extra si la palanca está desactivada
            if (efectoExtra != null)
                efectoExtra.SetActive(false);
        }
    }


    // Método para instanciar el objeto
    void InstanciarObjeto()
    {
        Instantiate(prefab, puntoInstanciacion.position, puntoInstanciacion.rotation);
    }

    // Método para instanciar los impactos en 3 posiciones aleatorias
    void InstanciarImpactos()
    {
        for (int i = 0; i < 3; i++) // Generar 3 efectos de impacto
        {
            // Generar una posición aleatoria dentro del rango especificado en el punto de impacto
            Vector3 randomPosition = new Vector3(
                 puntoImpacto.position.x,  // Mantener la posición fija en X
                 puntoImpacto.position.y,  // Mantener la posición fija en Y
                 puntoImpacto.position.z + Random.Range(-impactRange, impactRange)  // Aleatorio solo en Z
             );


            // Instanciar el efecto de impacto en la posición aleatoria
            GameObject impact = Instantiate(impactEffect, randomPosition, Quaternion.identity);

            // Destruir el efecto después de un cierto tiempo
            Destroy(impact, impactLifetime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cafe"))
        {
            cantidadCafe += 1f;
        }
    }
}