using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class boton : MonoBehaviour
{
    [Header("Animación cilindros separador")]
    [SerializeField] private Animator cilindro1;
    [SerializeField] private Animator cilindro2;

    [Header("Prefabs y puntos de instanciación")]
    public GameObject grano;          // Prefab a instanciar
    public Transform puntoInstanciacion;  // Punto donde instanciar los objetos
    public GameObject btnSiguienteSeccion;

    [Header("Tiempo y Rango")]
    public float intervalo = 0.5f;     // Tiempo entre instanciaciones
    private float timer = 0f;          // Temporizador para controlar la frecuencia de instanciación


    private bool btnPresionado = false;


    void Update()
    {
        if (btnPresionado)
        {
            timer += Time.deltaTime;

            if (timer >= intervalo)  // Si ha pasado el intervalo
            {
                InstanciarObjeto();
                timer = 0f;         // Resetear el temporizador
            }
        }
    }

    public void StarAnimRodillos()
    {
        cilindro1.Play("Separador1");
        cilindro2.Play("Separador2");

        btnPresionado = true;
    }

    public void StopAnimRodillos()
    {
        cilindro1.Play("Idle1");
        cilindro2.Play("Idle2");

        btnPresionado = false;
    }

    void InstanciarObjeto()
    {
        Instantiate(grano, puntoInstanciacion.position, puntoInstanciacion.rotation);
    }
}
