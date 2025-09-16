using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class VisionConoRenderer : MonoBehaviour
{
    public Transform objetivo;
    public float anguloVision = 90f;
    public float rangoVision = 5f;
    private LineRenderer lineRenderer;
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();

        // Nos aseguramos de inicializar bien el LineRenderer
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer no encontrado.");
            return;
        }

        lineRenderer.positionCount = 3;  // Tres puntos: izquierda, centro, derecha
        lineRenderer.loop = false;
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.useWorldSpace = true;
    }

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 3;
        lineRenderer.loop = false;
        lineRenderer.widthMultiplier = 0.05f;
        lineRenderer.useWorldSpace = true;
    }

    void Update()
    {
        if (lineRenderer == null || lineRenderer.positionCount < 3) return;

        Vector3 origen = transform.position;
        Vector3 frente = transform.up;

        Quaternion izquierda = Quaternion.Euler(0, 0, -anguloVision / 2);
        Quaternion derecha = Quaternion.Euler(0, 0, anguloVision / 2);

        Vector3 puntoIzquierdo = origen + (izquierda * frente) * rangoVision;
        Vector3 puntoDerecho = origen + (derecha * frente) * rangoVision;

        // Dibuja los 2 lados del cono
        lineRenderer.SetPosition(0, puntoIzquierdo);
        lineRenderer.SetPosition(1, origen);
        lineRenderer.SetPosition(2, puntoDerecho);
    }
}
