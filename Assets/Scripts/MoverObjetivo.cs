using UnityEngine;

public class MoverObjetivo : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButton(0)) // botón izquierdo
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = 0;
            transform.position = mouse;
        }
    }
}
