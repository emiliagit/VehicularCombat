using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    public float rotationSpeed = 5f;  // Velocidad de rotación de la torreta
    public Transform turret;          // Referencia al transform de la torreta

    void Update()
    {
        RotateTurret();
    }

    void RotateTurret()
    {
        // Obtiene la posición del mouse en el mundo
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 targetPosition = hit.point;

            // Calcula la dirección hacia la cual debe girar la torreta
            Vector3 direction = targetPosition - turret.position;
            direction.y = 0;  // Mantén la rotación en el plano horizontal

            // Calcula la rotación objetivo
            Quaternion targetRotation = Quaternion.LookRotation(direction);

            // Interpola la rotación actual hacia la rotación objetivo
            turret.rotation = Quaternion.Lerp(turret.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
