using System.Collections;
using UnityEngine;

public class PunteroRemigio : MonoBehaviour
{
    [Header("GameObjects")]
    public GameObject objectToAppear;
    public GameObject objectToAppearEffect;
    public GameObject objectToDestroy;

    private bool effectActivated = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera") && !effectActivated)
        {
            if (objectToAppear != null)
                objectToAppear.SetActive(true);

            if (objectToAppearEffect != null)
            {
                objectToAppearEffect.SetActive(true);
                StartCoroutine(DestroyEffectAfterTime(objectToAppearEffect, 1f));
            }

            if (objectToDestroy != null)
                Destroy(objectToDestroy);

            effectActivated = true;
        }
    }

    // Recibe el objeto a eliminar por parámetro
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
