using UnityEngine;

public class boton : MonoBehaviour
{
    [Header("Animación cilindros separador")]
    [SerializeField] private Animator cilindro1;
    [SerializeField] private Animator cilindro2;

    public void StarAnimRodillos()
    {
        cilindro1.Play("Separador1");
        cilindro2.Play("Separador2");

    }

}
