using UnityEngine;

public class Agua : MonoBehaviour
{
    [Header("Ondas")]
    [SerializeField] private float amplitude = 0.05f;   
    [SerializeField] private float frequency = 1.5f;    
    [SerializeField] private float speed = 1.0f;        

    [Header("Segundo patrón (mezclar ondas)")]
    [SerializeField] private float amplitude2 = 0.03f;
    [SerializeField] private float frequency2 = 1.0f;
    [SerializeField] private float speed2 = 0.7f;

    private Mesh mesh;
    private Vector3[] baseVertices;
    private Vector3[] workingVertices;

    private int frameSkip = 2;  
    private int currentFrame = 0; 

    private void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
        workingVertices = new Vector3[baseVertices.Length];
        mesh.MarkDynamic(); // Optimiza el mesh para cambios frecuentes
    }

    private void Update()
    {
        if (currentFrame % frameSkip == 0)
        {
            ApplyWaves();
        }

        currentFrame++;
    }

    private void ApplyWaves()
    {
        float t = Time.time;
        Vector3 center = transform.position; 

        for (int i = 0; i < baseVertices.Length; i++)
        {
            Vector3 v = baseVertices[i];
            float x = v.x - center.x;  
            float z = v.z - center.z;  
            
            float distanceFromCenter = Mathf.Sqrt(x * x + z * z);

            float wave1 = Mathf.Sin(distanceFromCenter * frequency + t * speed);
            float wave2 = Mathf.Sin(distanceFromCenter * frequency2 + t * speed2);

            float y = wave1 * amplitude + wave2 * amplitude2;

            v.y = y;
            workingVertices[i] = v;
        }

        mesh.vertices = workingVertices;
        mesh.RecalculateNormals(); 
    }

    public float GetWaterHeight(float xPosition, float zPosition)
    {
        Vector3 position = new Vector3(xPosition, 0, zPosition);
        float t = Time.time;
        Vector3 center = transform.position;

        // Calculo la distancia desde el centro
        float x = position.x - center.x;
        float z = position.z - center.z;
        float distanceFromCenter = Mathf.Sqrt(x * x + z * z);

        float wave1 = Mathf.Sin(distanceFromCenter * frequency + t * speed);
        float wave2 = Mathf.Sin(distanceFromCenter * frequency2 + t * speed2);
        float height = wave1 * amplitude + wave2 * amplitude2;

        return height;
    }
}
