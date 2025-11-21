using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class CoffeeFruit : MonoBehaviour
{
    [Header("Anclaje a la rama")]
    [Tooltip("Punto en la rama donde se pega el fruto (lo asigna el spawner)")]
    public Transform branchAttachPoint;          // en la rama

    [Tooltip("Punto en el fruto donde nace el rabito (hijo del fruto)")]
    public Transform stemPoint;                  // en el prefab

    [Header("Resistencia al tirar")]
    public float requiredPullDistance = 0.20f;   // Distancia para arrancar
    public float springStiffness = 12f;          // Qué tan duro se siente
    public float maxStretchScale = 0.15f;        // Escala extra máxima (15%)

    private XRGrabInteractable grab;
    private Rigidbody rb;
    private Transform interactorTransform;
    private bool isAttached = true;

    private Vector3 initialLocalPos;
    private Quaternion initialLocalRot;
    private Vector3 initialLocalScale;

    [Header("Doblado de rama (opcional)")]
    public Transform branchBendBone;      // el bone de la rama que quieres doblar
    public float maxBendAngle = 15f;      // grados máximos de doblado

    private Quaternion branchOriginalRot;

    public AudioSource sonido;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<XRGrabInteractable>();

        grab.selectEntered.AddListener(OnGrabbed);
        grab.selectExited.AddListener(OnReleased);

        // Mientras está pegado, lo controlamos nosotros
        grab.trackPosition = false;
        grab.trackRotation = false;

        // Pegado a la rama SIN física
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    private void Start()
    {
        initialLocalPos = transform.localPosition;
        initialLocalRot = transform.localRotation;
        initialLocalScale = transform.localScale;

        if (branchBendBone != null)
            branchOriginalRot = branchBendBone.localRotation;
    }

    private void OnDestroy()
    {
        grab.selectEntered.RemoveListener(OnGrabbed);
        grab.selectExited.RemoveListener(OnReleased);
    }

    // ===== Llamado por el spawner después de instanciar =====
    public void AlignStemWithBranch()
    {
        if (branchAttachPoint == null || stemPoint == null) return;

        // Queremos que el stemPoint del fruto caiga justo en el punto de la rama
        Vector3 offset = transform.position - stemPoint.position;
        transform.position = branchAttachPoint.position + offset;
    }

    // ===== Eventos de agarre =====
    private void OnGrabbed(SelectEnterEventArgs args)
    {
        interactorTransform = args.interactorObject.transform;
        StopAllCoroutines();
        StartCoroutine(ResistAndDetachCoroutine());

        sonido.Play();
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        interactorTransform = null;

        if (isAttached && branchAttachPoint != null)
        {
            // Sigue pegado a la rama: vuelve a su lugar original
            transform.SetParent(branchAttachPoint);
            transform.localPosition = initialLocalPos;
            transform.localRotation = initialLocalRot;
            transform.localScale = initialLocalScale;

            // Sin física mientras está en la rama
            rb.isKinematic = true;
            rb.useGravity = false;

            // Volvemos a controlar la posición nosotros
            grab.trackPosition = false;
            grab.trackRotation = false;

            if (branchBendBone != null)
                branchBendBone.localRotation = branchOriginalRot;
        }
        else
        {
            // 🔥 Ya NO está attached → lo arrancaste
            // Al soltarlo, ahora sí cae por gravedad
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }


    // ===== Corutina de resistencia =====
    private IEnumerator ResistAndDetachCoroutine()
    {
        isAttached = true;

        while (isAttached && interactorTransform != null && branchAttachPoint != null)
        {
            Vector3 branchPos = branchAttachPoint.position;
            Vector3 handPos = interactorTransform.position;

            Vector3 dir = handPos - branchPos;
            float distance = dir.magnitude;

            // 0 = pegado | 1 = distancia requerida
            float t = Mathf.Clamp01(distance / requiredPullDistance);

            // Doblado de la rama según cuánto tiras
            if (branchBendBone != null)
            {
                float bend = Mathf.Lerp(0f, maxBendAngle, t);
                branchBendBone.localRotation = branchOriginalRot * Quaternion.Euler(bend, 0f, 0f);
            }

            // Posición intermedia tipo resorte
            Vector3 targetPos = Vector3.Lerp(branchPos, handPos, t);
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * springStiffness);

            // Rotación: el fruto mira hacia la mano
            if (dir.sqrMagnitude > 0.0001f)
            {
                Quaternion targetRot = Quaternion.LookRotation(dir, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * springStiffness);
            }

            // Estiramiento ligero
            float stretchAmount = 1f + (t * maxStretchScale);
            transform.localScale = initialLocalScale * stretchAmount;

            // Ya lo jaló suficiente -> se arranca
            if (distance >= requiredPullDistance)
            {
                DetachFromBranch();
            }

            yield return null;
        }
    }

    // ===== Se desprende de la rama =====
    private void DetachFromBranch()
    {
        isAttached = false;

        transform.SetParent(null);
        transform.localScale = initialLocalScale;

        // Ahora XR sigue la mano directamente
        grab.trackPosition = true;
        grab.trackRotation = true;

        // OJO: AQUÍ YA NO TOCAMOS isKinematic NI GRAVEDAD

        if (branchBendBone != null)
            branchBendBone.localRotation = branchOriginalRot;
    }
}
