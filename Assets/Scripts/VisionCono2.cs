using UnityEngine;

public class VisionCono2 : MonoBehaviour
{
    public Transform objetivo;
    public float anguloVision = 90f;
    public float rangoVision = 5f;
    public bool objetivoDetectado = false;

    void Update()
    {
        objetivoDetectado = EstaEnVision(transform.position, transform.up, objetivo.position, anguloVision, rangoVision);
        //Vector2 direccionAlObjetivo = objetivo.position - transform.position;
        //float distancia = direccionAlObjetivo.magnitude;

        //if (distancia > rangoVision)
        //{
        //    objetivoDetectado = false;
        //    return;
        //}

        //Vector2 frente = transform.up; // Asumimos que "up" es la dirección del frente
        //direccionAlObjetivo.Normalize();

        //float dot = Vector2.Dot(frente, direccionAlObjetivo);
        //float cosAnguloVision = Mathf.Cos(anguloVision * 0.5f * Mathf.Deg2Rad);

        //objetivoDetectado = dot >= cosAnguloVision;
    }
    /// <summary>
    /// Verifica si un objetivo está dentro del rango y ángulo de visión.
    /// </summary>
    public static bool EstaEnVision(Vector2 origen, Vector2 frente, Vector2 posicionObjetivo, float anguloVision, float rangoVision)
    {
        Vector2 direccionAlObjetivo = posicionObjetivo - origen;
        float distancia = direccionAlObjetivo.magnitude;

        if (distancia > rangoVision) return false;

        direccionAlObjetivo.Normalize();

        float dot = Vector2.Dot(frente.normalized, direccionAlObjetivo);
        float cosAnguloVision = Mathf.Cos(anguloVision * 0.5f * Mathf.Deg2Rad);

        return dot >= cosAnguloVision;
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
