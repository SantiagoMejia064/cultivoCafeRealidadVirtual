using System.Collections.Generic;
using UnityEngine;

public class CoffeeTreeFruitSpawner : MonoBehaviour
{
    [Header("Puntos donde aparecerán los frutos (en las ramas)")]
    public List<Transform> spawnPoints = new List<Transform>();

    [Header("Prefabs de frutos")]
    public GameObject ripeFruitPrefab;      // Rojo
    public GameObject semiRipeFruitPrefab;  // Pintón
    public GameObject greenFruitPrefab;     // Verde

    [Header("Aparición")]
    [Range(0f, 1f)]
    public float spawnProbability = 1f;

    [Tooltip("Tamaño objetivo del fruto en el mundo (metros)")]
    public Vector3 fruitWorldScale = new Vector3(0.03f, 0.03f, 0.03f); // PRUEBA 0.03 o 0.02

    private void Start()
    {
        SpawnFruits();
    }

    public void SpawnFruits()
    {
        foreach (Transform point in spawnPoints)
        {
            if (point == null) continue;
            if (Random.value > spawnProbability) continue;

            GameObject prefab = GetRandomFruitPrefab();
            if (prefab == null) continue;

            // Instanciamos como hijo de la rama
            GameObject fruit = Instantiate(prefab, point);

            // Posición en el punto
            fruit.transform.localPosition = Vector3.zero;

            // Rotación ALEATORIA para que se vean naturales
            fruit.transform.localRotation = Quaternion.Euler(
                Random.Range(0f, 360f),
                Random.Range(0f, 360f),
                Random.Range(0f, 360f)
            );

            // 🔥 ESCALA CORREGIDA EN FUNCIÓN DEL PADRE
            Vector3 parentScale = point.lossyScale;
            // Evitar divisiones por 0
            parentScale.x = Mathf.Approximately(parentScale.x, 0f) ? 1f : parentScale.x;
            parentScale.y = Mathf.Approximately(parentScale.y, 0f) ? 1f : parentScale.y;
            parentScale.z = Mathf.Approximately(parentScale.z, 0f) ? 1f : parentScale.z;

            Vector3 localScale;
            localScale.x = fruitWorldScale.x / parentScale.x;
            localScale.y = fruitWorldScale.y / parentScale.y;
            localScale.z = fruitWorldScale.z / parentScale.z;

            fruit.transform.localScale = localScale;

            // Configurar script del fruto
            CoffeeFruit f = fruit.GetComponent<CoffeeFruit>();
            if (f != null)
            {
                f.branchAttachPoint = point;
                f.AlignStemWithBranch();
            }
        }
    }

    private GameObject GetRandomFruitPrefab()
    {
        int r = Random.Range(0, 3);
        switch (r)
        {
            case 0: return ripeFruitPrefab;
            case 1: return semiRipeFruitPrefab;
            case 2: return greenFruitPrefab;
        }
        return ripeFruitPrefab;
    }
}
