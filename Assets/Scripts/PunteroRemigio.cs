using System.Collections;
using UnityEngine;

public class PunteroRemigio : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject objectToAppear1;
    public GameObject objectToAppear2;
    public GameObject objectToAppearEffect;
    public GameObject objectToDestroy;

    private bool effectActivated = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera") && !effectActivated)
        {
            if (objectToAppear1 != null)
                objectToAppear1.SetActive(true);

            if (objectToAppear2 != null)
                objectToAppear2.SetActive(true);

            if (objectToAppearEffect != null)
            {
                objectToAppearEffect.SetActive(true);
                StartCoroutine(DestroyEffectAfterTime(objectToAppearEffect, 0.5f));
            }

            if (objectToDestroy != null)
                Destroy(objectToDestroy);

            effectActivated = true;
        }
    }

    // CORRECTA: Recibe el objeto a eliminar por parámetro
    private IEnumerator DestroyEffectAfterTime(GameObject effectObject, float time)
    {
        yield return new WaitForSeconds(time);

        if (effectObject != null)
            Destroy(effectObject);

        // Si SOLO quieres que esto se active una vez, NO restaures effectActivated aquí.
        // Si quieres que pueda ocurrir otra vez, desactívalo aquí:
        // effectActivated = false;
    }
}
