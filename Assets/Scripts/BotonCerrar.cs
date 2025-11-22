using UnityEngine;
using UnityEngine.UI;

public class BotonCerrar : MonoBehaviour
{
    [Header("Botón de Cerrar")]
    public Button botonCerrar;  // El botón que cierra la aplicación

    private void Start()
    {
        // Verificar si el botón está asignado
        if (botonCerrar != null)
        {
            // Asignar la función para cerrar la aplicación al hacer clic
            botonCerrar.onClick.AddListener(CerrarAplicacion);
        }
    }

    // Función que se llama cuando se hace clic en el botón
    void CerrarAplicacion()
    {
        // Si estamos en el editor de Unity, detener la simulación
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si estamos en una versión de compilación (build), cerrar la aplicación
        Application.Quit();
#endif
    }
}
