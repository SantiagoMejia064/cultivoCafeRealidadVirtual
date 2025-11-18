using UnityEngine;

public class Animacion : MonoBehaviour
{
    [Header("Velocidad del agua")]
    public float speedX = 0.2f;   
    public float speedY = 0.0f;   

    private Renderer rend;
    private Vector2 offset;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        // materialito del agua
        if (rend.material != null)
            offset = rend.material.mainTextureOffset;
    }

    private void Update()
    {
        offset.x += speedX * Time.deltaTime;
        offset.y += speedY * Time.deltaTime;

        rend.material.mainTextureOffset = offset;
    }
}
