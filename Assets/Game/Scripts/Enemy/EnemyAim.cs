using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    public Transform player; // Referencia al Transform del jugador
    public Transform turret; // Referencia al Transform de la torreta
    public float rotationSpeed = 5f; // Velocidad de rotaci�n de la torreta

    void Update()
    {
        TurretAim();
    }

    public void TurretAim ()
    {

        if (player != null && turret != null)
        {
            // Calcula la direcci�n hacia el jugador
            Vector3 direction = player.position - turret.position;
            direction.y = 0; // Mant�n la rotaci�n en el plano horizontal

            // Calcula la rotaci�n deseada
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Rota la torreta suavemente hacia el jugador
            turret.rotation = Quaternion.Lerp(turret.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
