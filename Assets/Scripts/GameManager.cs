using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class GameManager : MonoBehaviour
{
    [Header("Palanca seccion 3")]
    public XRLever palanca;

    public void seccion3_4()
    {
        palanca.value = false;
    }


}
