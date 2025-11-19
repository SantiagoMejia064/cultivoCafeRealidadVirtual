using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class Pistola : MonoBehaviour
{
    public GameObject ShootFx, HitFx;
    public Transform firePoint;
    public LineRenderer line;

    //esta variable es la referencia al componente de XRI para ser agarrado
    private XRGrabInteractable grab;
    private Rigidbody rb;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();
    }


    void OnEnable()
    {
        grab.activated.AddListener(OnActivated); //El evento de activación para disparar
    }

    void OnDisable()
    {
        grab.activated.RemoveListener(OnActivated);
    }

    //Metodo que es el evento de activación, osea que inicie el disparo
    private void OnActivated(ActivateEventArgs _)
    {
        StartCoroutine(Disparo());
    }

    private IEnumerator Disparo()
    {

        RaycastHit hit;
        bool hitInfo = Physics.Raycast(firePoint.position, firePoint.forward, out hit, 50f);
        Instantiate(ShootFx, firePoint.position, Quaternion.identity);
        if (hitInfo)
        {
            line.SetPosition(0, firePoint.position);
            line.SetPosition(1, hit.point);
            Instantiate(HitFx, hit.point, Quaternion.identity);
        }
        else
        {
            line.SetPosition(0, firePoint.position);
            line.SetPosition(1, firePoint.position + firePoint.forward * 20f);
        }

        line.enabled = true;
        yield return new WaitForSeconds(0.02f);
        line.enabled = false;
    }
}