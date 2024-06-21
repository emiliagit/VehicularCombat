using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;

    // La distancia a la que la cámara debe seguir al jugador.
    public Vector3 offset;

    // Velocidad de seguimiento de la cámara.
    public float followSpeed = 5f;

    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        // Si no se ha definido un offset, lo establecemos a la distancia actual entre la cámara y el jugador.
        if (offset == Vector3.zero && player != null)
        {
            offset = transform.position - player.position;
        }
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // Calcula la posición objetivo de la cámara.
            Vector3 targetPosition = player.position + offset;

            // Suaviza el movimiento de la cámara.
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, followSpeed * Time.fixedDeltaTime);
        }
    }
}
