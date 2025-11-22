using UnityEngine;
using System.Collections;
using System.Collections.Generic; // Importante para las listas

public class InstanciadorBoton : MonoBehaviour
{
    [Header("Generación de Frutos")]
    [SerializeField] private GameObject prefab;               // Prefab a instanciar (fruto de café)
    [SerializeField] private List<Transform> puntosInstanciacion; // Lista de puntos donde se instanciarán los objetos
    [SerializeField] private float intervalo = 0.5f;          // Tiempo entre instanciaciones
    public GameObject boton; 

    private float timer = 0f;                                  // Temporizador para el intervalo

    private bool generandoFrutos = false;                      // Controla si la generación de frutos está activa

    // Función para empezar a generar frutos cuando el botón es presionado
    public void ActivarGeneracionFrutos()
    {
        if (!generandoFrutos)
        {
            generandoFrutos = true;
            StartCoroutine(GenerarFrutos());
        }

        boton.SetActive(true);
    }

    // Método para generar frutos en intervalos
    private IEnumerator GenerarFrutos()
    {
        while (generandoFrutos) // Mientras esté generando frutos
        {
            timer += Time.deltaTime;  // Incrementa el temporizador con el tiempo transcurrido

            if (timer >= intervalo)  // Si ha pasado el intervalo, instanciamos un fruto
            {
                InstanciarObjeto();
                timer = 0f;  // Reiniciar el temporizador
            }

            yield return null;  // Espera hasta el siguiente frame
        }
    }

    // Método para instanciar el prefab en uno de los puntos aleatorios
    private void InstanciarObjeto()
    {
        // Verificar que la lista de puntos no esté vacía
        if (puntosInstanciacion.Count > 0)
        {
            // Seleccionamos un punto de instanciación aleatorio
            Transform puntoInstanciacion = puntosInstanciacion[Random.Range(0, puntosInstanciacion.Count)];

            // Instanciamos el objeto en el punto aleatorio
            GameObject nuevoFruto = Instantiate(prefab, puntoInstanciacion.position, puntoInstanciacion.rotation);

            // Destruir el objeto instanciado después de 0.5 segundos
            Destroy(nuevoFruto, 0.5f); // Ahora destruimos la instancia, no el prefab
        }
        else
        {
            Debug.LogWarning("No hay puntos de instanciación disponibles.");
        }
    }

    // Si deseas detener la generación de frutos
    /*
    public void DetenerGeneracionFrutos()
    {
        generandoFrutos = false;  // Para de generar frutos
    }
    */
}
