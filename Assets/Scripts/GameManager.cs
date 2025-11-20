using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class GameManager : MonoBehaviour
{
    [Header("Seccion 3")]
    public XRLever palanca;

    [Header("Seccion 4")]
    public GameObject btnSiguienteSeccion;
    public float cantidadCafe = 0f;

    void Update()
    {
        if (cantidadCafe >= 10f)
        {
            btnSiguienteSeccion.SetActive(true);
        }
    }

    public void seccion3_4()
    {
        palanca.value = false;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cafe"))
        {
            cantidadCafe += 1f;
        }
    }


}
