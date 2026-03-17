using UnityEngine;

public class EscenarioRotator : MonoBehaviour
{
    [Header("Referencias")]
    public Transform target;          // El centro del Jenga (puede ser el Contenedor)
    public Transform escenarioJenga;  // El objeto "Contenedor_Jenga" que creamos

    [Header("Configuración de Órbita")]
    public float distance = 6f;
    public float zoomSpeed = 5f;
    public float rotationSpeed = 120f;
    public float verticalSpeed = 3f;
    public float minHeight = 1f;
    public float maxHeight = 8f;
    public float smoothTime = 0.1f;

    private float targetAngle;
    private float targetHeight;
    private float targetDistance;

    private float currentAngle;
    private float currentHeight;
    private float currentDistance;

    private float angleVelocity;
    private float heightVelocity;
    private float distanceVelocity;

    // NUEVO: Para bloquear/desbloquear bloques
    private Rigidbody[] blockRBs;
    private bool blocksLocked = false;

    void Start()
    {
        targetHeight = transform.position.y;
        targetDistance = distance;
        currentHeight = targetHeight;
        currentDistance = targetDistance;

        // NUEVO: Buscar todos los rigidbodies dentro del escenario Jenga
        if (escenarioJenga != null)
        {
            blockRBs = escenarioJenga.GetComponentsInChildren<Rigidbody>();
        }
    }

    void LateUpdate()
    {
        if (!escenarioJenga || !target) return;

        // NUEVO: Detectar si el usuario está girando la torre
        bool isRotating =
            Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) ||
            Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);

        // NUEVO: Bloquear/desbloquear bloques según si gira
        if (isRotating && !blocksLocked)
        {
            SetBlocksKinematic(true);
        }
        else if (!isRotating && blocksLocked)
        {
            SetBlocksKinematic(false);
        }

        // 1. INPUTS (Rotación, Altura y Zoom)
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            targetAngle += rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            targetAngle -= rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            targetHeight += verticalSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            targetHeight -= verticalSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Q)) targetDistance -= zoomSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.E)) targetDistance += zoomSpeed * Time.deltaTime;

        // 2. SUAVIZADO
        currentAngle = Mathf.SmoothDamp(currentAngle, targetAngle, ref angleVelocity, smoothTime);
        currentHeight = Mathf.SmoothDamp(currentHeight, targetHeight, ref heightVelocity, smoothTime);
        currentDistance = Mathf.SmoothDamp(currentDistance, targetDistance, ref distanceVelocity, smoothTime);

        // 3. APLICAR ROTACIÓN AL ESCENARIO
        // En lugar de mover la cámara en círculos, rotamos el escenario sobre el eje Y
        escenarioJenga.rotation = Quaternion.Euler(0, currentAngle, 0);

        // 4. POSICIÓN DE LA CÁMARA (Fija en X y Z, solo cambia altura y distancia)
        // La cámara siempre mira al frente, el mundo es el que gira
        transform.position = new Vector3(target.position.x, currentHeight, target.position.z - currentDistance);
        transform.LookAt(new Vector3(target.position.x, currentHeight, target.position.z));
    }

    // NUEVO: Método para bloquear/desbloquear bloques
    void SetBlocksKinematic(bool state)
    {
        if (blockRBs == null) return;

        foreach (Rigidbody rb in blockRBs)
        {
            if (rb != null)
            {
                rb.isKinematic = state;

                // Al soltar, limpiar pequeñas velocidades raras
                if (!state)
                {
                    rb.linearVelocity = Vector3.zero;
                    rb.angularVelocity = Vector3.zero;
                }
            }
        }

        blocksLocked = state;
    }
}