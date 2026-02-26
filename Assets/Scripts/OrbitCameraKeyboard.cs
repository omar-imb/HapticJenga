using UnityEngine;

public class OrbitCameraSmooth : MonoBehaviour
{
    public Transform target;

    [Header("Distancia")]
    public float distance = 6f;
    public float minDistance = 3f;
    public float maxDistance = 10f;
    public float zoomSpeed = 5f;

    [Header("Velocidades")]
    public float rotationSpeed = 120f;
    public float verticalSpeed = 3f;

    [Header("Altura")]
    public float minHeight = 1f;
    public float maxHeight = 8f;

    [Header("Suavizado")]
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

    void Start()
    {
        targetHeight = transform.position.y;
        targetDistance = distance;

        currentHeight = targetHeight;
        currentDistance = targetDistance;
    }

    void LateUpdate()
    {
        if (!target) return;

        // ?? ROTACIÓN (FLECHAS INVERTIDAS)
        // ROTACIÓN (Flechas + A/D)
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            targetAngle += rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            targetAngle -= rotationSpeed * Time.deltaTime;


        // ?? MOVIMIENTO VERTICAL
        // ALTURA (Flechas + W/S)
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            targetHeight += verticalSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            targetHeight -= verticalSpeed * Time.deltaTime;

        targetHeight = Mathf.Clamp(targetHeight, minHeight, maxHeight);


        // ?? ZOOM (Q acercar, E alejar)
        if (Input.GetKey(KeyCode.Q))
            targetDistance -= zoomSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.E))
            targetDistance += zoomSpeed * Time.deltaTime;

        targetDistance = Mathf.Clamp(targetDistance, minDistance, maxDistance);


        // ?? SUAVIZADO
        currentAngle = Mathf.SmoothDamp(currentAngle, targetAngle, ref angleVelocity, smoothTime);
        currentHeight = Mathf.SmoothDamp(currentHeight, targetHeight, ref heightVelocity, smoothTime);
        currentDistance = Mathf.SmoothDamp(currentDistance, targetDistance, ref distanceVelocity, smoothTime);


        // ?? POSICIÓN CIRCULAR
        float x = target.position.x + Mathf.Sin(currentAngle * Mathf.Deg2Rad) * currentDistance;
        float z = target.position.z + Mathf.Cos(currentAngle * Mathf.Deg2Rad) * currentDistance;

        Vector3 desiredPosition = new Vector3(x, currentHeight, z);
        transform.position = desiredPosition;

        transform.LookAt(new Vector3(target.position.x, currentHeight, target.position.z));
    }
}