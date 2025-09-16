using UnityEngine;

public class VisionConoAutoGiroMovimiento : MonoBehaviour
{
    public Transform objetivo;
    public float anguloVision = 90f;
    public float rangoVision = 5f;
    public float velocidadRotacion = 180f;  // grados por segundo
    public float velocidadMovimiento = 2f;   // unidades por segundo

    private bool objetivoDetectado;

    void Update()
    {
        Vector2 direccionObjetivo = objetivo.position - transform.position;
        float distancia = direccionObjetivo.magnitude;

        if (distancia > rangoVision)
        {
            objetivoDetectado = false;
            return;
        }

        Vector2 frente = transform.up;
        Vector2 direccionNormalizada = direccionObjetivo.normalized;

        float dot = Vector2.Dot(frente, direccionNormalizada);
        float cosLimite = Mathf.Cos(anguloVision * 0.5f * Mathf.Deg2Rad);

        objetivoDetectado = dot >= cosLimite;

        if (objetivoDetectado)
        {
            // ROTACIÓN SUAVE
            float anguloObjetivo = Mathf.Atan2(direccionObjetivo.y, direccionObjetivo.x) * Mathf.Rad2Deg - 90f;
            Quaternion rotacionObjetivo = Quaternion.Euler(0, 0, anguloObjetivo);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacionObjetivo, velocidadRotacion * Time.deltaTime);

            // MOVIMIENTO ADELANTE
            float anguloDiferencia = Quaternion.Angle(transform.rotation, rotacionObjetivo);
            if (anguloDiferencia < 5f) // Solo avanza si está bien alineado
            {
                transform.position += transform.up * velocidadMovimiento * Time.deltaTime;
            }
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
