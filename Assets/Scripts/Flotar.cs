using UnityEngine;

public class Flotar : MonoBehaviour
{
    public Agua agua; 
    public float flotacionOffset = 0.5f; 
    private Rigidbody rb; 
    public float smoothSpeed = 5f;

    private bool enAgua = false; // Variable para saber si estamos en el agua

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void FixedUpdate()
    {
        if (enAgua)
        {
            // Solo flotamos si estamos en agua
            float waterHeight = agua.GetWaterHeight(transform.position.x, transform.position.z);
            Vector3 targetPosition = new Vector3(transform.position.x, waterHeight + flotacionOffset, transform.position.z);

            // Suavizamos el movimiento del objeto
            Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.fixedDeltaTime);
            rb.MovePosition(smoothPosition);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("agua"))
        {
            enAgua = true; // Empieza a flotar al entrar en contacto con el agua
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("agua"))
        {
            enAgua = false; // Deja de flotar cuando sale del agua
        }
    }
}
