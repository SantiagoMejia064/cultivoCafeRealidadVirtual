using UnityEngine;

public class FuncionTolva : MonoBehaviour
{
    [Header("Panel que quiero controlar")]
    public GameObject panel;

    void Start()
    {
        // Panel apagado al iniciar
        if (panel != null)
            panel.SetActive(false);
    }

    // Llamado desde el evento del botón
    public void ActivarPanel()
    {
        if (panel != null)
            panel.SetActive(true);
    }

    // (Opcional) si algún día quieres ocultarlo desde un botón
    public void DesactivarPanel()
    {
        if (panel != null)
            panel.SetActive(false);
    }

    // (Opcional) activar/desactivar con un solo método
    public void TogglePanel()
    {
        if (panel != null)
            panel.SetActive(!panel.activeSelf);
    }
}
