using UnityEngine;

public class Flotar : MonoBehaviour
{
    public Agua agua; 
    public float flotacionOffset = 0.5f; 
    private Rigidbody rb; 
    public float smoothSpeed = 5f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    private void FixedUpdate()
    {
        float waterHeight = agua.GetWaterHeight(transform.position.x, transform.position.z);
        Vector3 targetPosition = new Vector3(transform.position.x, waterHeight + flotacionOffset, transform.position.z);

        //Suavizo el movimiento del tio
        Vector3 smoothPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.fixedDeltaTime);
        rb.MovePosition(smoothPosition);
    }
}
