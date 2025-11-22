using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class InstanciadorNew : MonoBehaviour
{
    public XRLever palanca;                // Referencia a la palanca
    public GameObject prefab;              // Prefab a instanciar
    public Transform puntoInstanciacion;   // Donde instanciar los objetos
    public float intervalo = 0.5f;         // Tiempo entre instanciaciones

    private float timer = 0f;              // Temporizador para el intervalo
    public float timerLogro = 0f;
    private float tiempoTranscurrido = 0f; // Temporizador para el tiempo l�mite
    public float tiempoLimite = 10f;       // Tiempo en segundos para desactivar la palanca
    public float tiempoLimimteLogro = 5f;

    [SerializeField] private GameObject logroATostar;
    [SerializeField] private GameObject logroConcedido;



    void Update()
    {
        // Contabilizamos el tiempo transcurrido para el cambio en la palanca
        tiempoTranscurrido += Time.deltaTime;

        // Si ha pasado el tiempo l�mite, desactivamos la palanca
        if (tiempoTranscurrido >= tiempoLimite)
        {
            palanca.value = false; // Desactivar la palanca
            tiempoTranscurrido = 0f; // Resetear el temporizador
        }

        // Verificar si la palanca est� activada
        if (palanca.value)
        {
            logroATostar.SetActive(true);
            // Contabilizar el tiempo para la instanciaci�n
            timer += Time.deltaTime;

            // Si ha pasado el intervalo, instanciamos el objeto
            if (timer >= intervalo)
            {
                InstanciarObjeto();
                timer = 0f;  // Resetear el temporizador
            }
            // Contabilizar el tiempo para el logro
            timerLogro += Time.deltaTime;
            if(timerLogro >= tiempoLimimteLogro)
            {
                logroConcedido.SetActive(true);
                logroATostar.SetActive(false);
            }
        }
        else
        {
            // Si la palanca est� desactivada, resetear el temporizador
            timer = 0f;
            logroATostar.SetActive(false);
        }
        
    }

    // M�todo para instanciar el prefab en la posici�n deseada
    void InstanciarObjeto()
    {
        Instantiate(prefab, puntoInstanciacion.position, puntoInstanciacion.rotation);
    }
}
