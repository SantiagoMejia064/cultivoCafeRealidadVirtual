using UnityEngine;

public class Tolva : MonoBehaviour
{
    [Header("Shake Settings")]
    public float shakeIntensity = 0.05f;
    public float shakeDuration = 0.2f;
    public float shakeSpeed = 20f;

    private Vector3 originalPosition;
    private bool isShaking = false;

    private void Start()
    {
        originalPosition = transform.localPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("cafe"))
        {
            if (!isShaking)
            {
                StartCoroutine(Shake());
            }
        }
    }

    private System.Collections.IEnumerator Shake()
    {
        isShaking = true;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Mathf.Sin(Time.time * shakeSpeed) * shakeIntensity;
            float y = Mathf.Cos(Time.time * shakeSpeed) * shakeIntensity;

            transform.localPosition = originalPosition + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Regresar a posición original
        transform.localPosition = originalPosition;
        isShaking = false;
    }
}
