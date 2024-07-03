using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAim : MonoBehaviour
{
    public Transform player; // Referencia al Transform del jugador
    public Transform turret; // Referencia al Transform de la torreta
    public float rotationSpeed = 5f; // Velocidad de rotación de la torreta

    void Update()
    {
        TurretAim();
    }

    public void TurretAim ()
    {

        if (player != null && turret != null)
        {
            // Calcula la dirección hacia el jugador
            Vector3 direction = player.position - turret.position;
            direction.y = 0; // Mantén la rotación en el plano horizontal

            // Calcula la rotación deseada
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Rota la torreta suavemente hacia el jugador
            turret.rotation = Quaternion.Lerp(turret.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
