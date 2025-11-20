using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class GameManager : MonoBehaviour
{
    [Header("Seccion 3")]
    public XRLever palanca;
    public Animator ducto1Anim;
    public Animator ducto2Anim;


    [Header("Seccion 4")]
    public GameObject btnSiguienteSeccion;
    public float cantidadCafe = 0f;
    public Animator puerta4Anim;

    [Header("Seccion 5")]
    public Animator puerta5Anim;

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
        ducto1Anim.Play("ducto1");
        ducto2Anim.Play("ducto2");
        puerta4Anim.Play("AbrirPuerta4");
    }

    public void seccion4_5()
    {
        puerta5Anim.Play("AbrirPuerta5");
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("cafe"))
        {
            cantidadCafe += 1f;
        }
    }


}
