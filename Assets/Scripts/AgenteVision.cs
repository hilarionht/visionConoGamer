using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class AgenteVision : MonoBehaviour
{
    public Transform objetivo;
    public float anguloVision = 90f;
    public float rangoVision = 5f;
    public float velocidadRotacion = 180f;  // grados/seg
    public float velocidadMovimiento = 2f;  // unidades/seg

    private bool objetivoDetectado;
    public Text textoCoordenadas;

    private LineRenderer lineaDireccion;

    void Start()
    {
        lineaDireccion = GetComponent<LineRenderer>();
        lineaDireccion.positionCount = 2;
        lineaDireccion.startWidth = 0.05f;
        lineaDireccion.endWidth = 0.05f;
        lineaDireccion.material = new Material(Shader.Find("Sprites/Default"));
        lineaDireccion.startColor = Color.green;
        lineaDireccion.endColor = Color.green;
    }

    void Update()
    {
        if (objetivo == null) return;

        Vector2 inicio = transform.position;
        Vector2 fin = objetivo.position;
        Vector2 direccion = fin - inicio;
        float distancia = direccion.magnitude;

        // Mostrar coordenadas
        if (textoCoordenadas != null)
        {
            textoCoordenadas.text =
                $"Inicio: ({inicio.x:F2}, {inicio.y:F2})\n" +
                $"Fin: ({fin.x:F2}, {fin.y:F2})\n" +
                $"Dirección: ({direccion.x:F2}, {direccion.y:F2})";
        }

        if (distancia > rangoVision)
        {
            objetivoDetectado = false;
            lineaDireccion.enabled = false;
            return;
        }

        Vector2 frente = transform.up;
        float dot = Vector2.Dot(frente, direccion.normalized);
        float cosLimite = Mathf.Cos(anguloVision * 0.5f * Mathf.Deg2Rad);

        objetivoDetectado = dot >= cosLimite;

        if (objetivoDetectado)
        {
            // Gira hacia el objetivo
            float anguloObjetivo = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotacionObjetivo = Quaternion.Euler(0, 0, anguloObjetivo);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacionObjetivo, velocidadRotacion * Time.deltaTime);

            // Avanza si está alineado
            float anguloDiferencia = Quaternion.Angle(transform.rotation, rotacionObjetivo);
            float distanciaMinima = 0.5f;

            if (anguloDiferencia < 5f && distancia > distanciaMinima)
            {
                transform.position += transform.up * velocidadMovimiento * Time.deltaTime;
            }

            // Dibujar línea
            lineaDireccion.enabled = true;
            lineaDireccion.SetPosition(0, transform.position);
            lineaDireccion.SetPosition(1, objetivo.position);
        }
        else
        {
            lineaDireccion.enabled = false;
        }
    }

    void OnDrawGizmos()
    {
        if (objetivo == null) return;

        Vector3 origen = transform.position;
        Vector3 frente = transform.up;

        Quaternion izquierda = Quaternion.Euler(0, 0, -anguloVision / 2);
        Quaternion derecha = Quaternion.Euler(0, 0, anguloVision / 2);

        Vector3 puntoIzquierdo = origen + (izquierda * frente) * rangoVision;
        Vector3 puntoDerecho = origen + (derecha * frente) * rangoVision;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(origen, puntoIzquierdo);
        Gizmos.DrawLine(origen, puntoDerecho);

        Gizmos.color = objetivoDetectado ? Color.green : Color.red;
        Gizmos.DrawLine(origen, objetivo.position);
    }
}
