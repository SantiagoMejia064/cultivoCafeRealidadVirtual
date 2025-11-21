using UnityEngine;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;

public class Seccion1Complete : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform tubo; 
    [SerializeField] private float tiempoVibracion = 0.5f; 
    [SerializeField] private float rangoVibracion = 0.05f; 

    private bool isVibrando = false;  
    private bool funcionando = false; 

    public void ActivarFuncionamiento(SelectEnterEventArgs args)
    {
        if (!funcionando) 
        {
            funcionando = true; 
            StartCoroutine(VibrarTubo()); 
            DestruirFrutos(); 
        }
    }

    private void DestruirFrutos()
    {
        GameObject[] frutos = GameObject.FindGameObjectsWithTag("roja");

        foreach (GameObject fruto in frutos)
        {
            Destroy(fruto); 
        }
        frutos = GameObject.FindGameObjectsWithTag("naranja");

        foreach (GameObject fruto in frutos)
        {
            Destroy(fruto);
        }
    }

    private IEnumerator VibrarTubo()
    {
        isVibrando = true; 
        Vector3 posicionOriginal = tubo.position;

        for (float t = 0; t < tiempoVibracion; t += Time.deltaTime)
        {
            float desplazamientoX = Random.Range(-rangoVibracion, rangoVibracion);
            float desplazamientoZ = Random.Range(-rangoVibracion, rangoVibracion);

            tubo.position = new Vector3(posicionOriginal.x + desplazamientoX, posicionOriginal.y, posicionOriginal.z + desplazamientoZ);
            yield return null;
        }
        tubo.position = posicionOriginal;
        isVibrando = false; 
    }
}
