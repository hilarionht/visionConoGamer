using UnityEngine;

public class VisionCono : MonoBehaviour
{
    //public Transform objetivo;
    //public float anguloVision = 90f;
    //public float rangoVision = 5f;
    //public float velocidadRotacion = 180f; // grados por segundo
    //private bool objetivoDetectado;

    //void Update()
    //{
    //    Vector2 direccionObjetivo = objetivo.position - transform.position;
    //    float distancia = direccionObjetivo.magnitude;

    //    if (distancia > rangoVision)
    //    {
    //        objetivoDetectado = false;
    //        return;
    //    }

    //    Vector2 frente = transform.up;
    //    Vector2 direccionNormalizada = direccionObjetivo.normalized;

    //    float dot = Vector2.Dot(frente, direccionNormalizada);
    //    float cosLimite = Mathf.Cos(anguloVision * 0.5f * Mathf.Deg2Rad);

    //    objetivoDetectado = dot >= cosLimite;

    //    if (objetivoDetectado)
    //    {
    //        // Calcula el ángulo hacia el objetivo
    //        float anguloObjetivo = Mathf.Atan2(direccionObjetivo.y, direccionObjetivo.x) * Mathf.Rad2Deg - 90f;
    //        Quaternion rotacionObjetivo = Quaternion.Euler(0, 0, anguloObjetivo);

    //        // Gira suavemente hacia el objetivo
    //        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotacionObjetivo, velocidadRotacion * Time.deltaTime);
    //    }
    //}

    //void OnDrawGizmos()
    //{
    //    if (objetivo == null) return;

    //    Vector3 origen = transform.position;
    //    Vector3 frente = transform.up;

    //    Quaternion izquierda = Quaternion.Euler(0, 0, -anguloVision / 2);
    //    Quaternion derecha = Quaternion.Euler(0, 0, anguloVision / 2);

    //    Vector3 puntoIzquierdo = origen + (izquierda * frente) * rangoVision;
    //    Vector3 puntoDerecho = origen + (derecha * frente) * rangoVision;

    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawLine(origen, puntoIzquierdo);
    //    Gizmos.DrawLine(origen, puntoDerecho);

    //    Gizmos.color = objetivoDetectado ? Color.green : Color.red;
    //    Gizmos.DrawLine(origen, objetivo.position);
    //}
    public Transform objetivo;
    public float anguloVision = 90f;
    public float rangoVision = 5f;
    public bool objetivoDetectado = false;

    void Update()
    {
        float rotacion = Input.GetAxis("Horizontal") * 100f * Time.deltaTime;
        transform.Rotate(0, 0, -rotacion);

        Vector2 direccionAlObjetivo = objetivo.position - transform.position;
        float distancia = direccionAlObjetivo.magnitude;

        if (distancia > rangoVision)
        {
            objetivoDetectado = false;
            return;
        }

        Vector2 frente = transform.up; // Asumimos que "up" es la dirección del frente
        direccionAlObjetivo.Normalize();

        float dot = Vector2.Dot(frente, direccionAlObjetivo);
        float cosAnguloVision = Mathf.Cos(anguloVision * 0.5f * Mathf.Deg2Rad);

        objetivoDetectado = dot >= cosAnguloVision;
    }

    private void OnDrawGizmos()
    {
        if (objetivo == null) return;

        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up * rangoVision);

        Quaternion izquierda = Quaternion.Euler(0, 0, -anguloVision / 2);
        Quaternion derecha = Quaternion.Euler(0, 0, anguloVision / 2);

        Vector3 dirIzquierda = izquierda * transform.up;
        Vector3 dirDerecha = derecha * transform.up;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + dirIzquierda * rangoVision);
        Gizmos.DrawLine(transform.position, transform.position + dirDerecha * rangoVision);

        Gizmos.color = objetivoDetectado ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, objetivo.position);
    }
}
