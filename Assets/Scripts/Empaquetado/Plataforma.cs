using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Plataforma : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public List<GameObject> onBelt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Limpiar la lista de objetos nulos (destruidos)
        for (int i = onBelt.Count - 1; i >= 0; i--)
        {
            if (onBelt[i] == null)  // Si el objeto es nulo (destruido), lo eliminamos de la lista
            {
                onBelt.RemoveAt(i);
            }
            else
            {
                // Si el objeto es válido, cambiamos su velocidad
                onBelt[i].GetComponent<Rigidbody>().linearVelocity = speed * direction;
            }
        }
    }

    // When something collides with the belt
    private void OnCollisionEnter(Collision collision)
    {
        if (!onBelt.Contains(collision.gameObject))  // Aseguramos que no se añadan objetos repetidos
        {
            onBelt.Add(collision.gameObject);
        }
    }

    // When something leaves the belt
    private void OnCollisionExit(Collision collision)
    {
        if (onBelt.Contains(collision.gameObject))  // Aseguramos que solo eliminamos objetos que están en la lista
        {
            onBelt.Remove(collision.gameObject);
        }
    }
}
    