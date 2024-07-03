using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public Transform player;

    public EnemyAim aim;

    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform projectileSpawnPoint;
    public float fireRate = 1f;
    private float nextFireTime = 0f;
    public float bulletForce = 7f;

    public float detectionRadius = 6f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        aim.TurretAim();

        // Comprobar la distancia entre el enemigo y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            // Si el jugador está dentro del radio de detección, disparar proyectiles
            if (Time.time >= nextFireTime)
            {
                AttackPlayer();
                nextFireTime = Time.time + 1f / fireRate; // Actualizar el tiempo para el próximo disparo
            }
        }
        
    }

    private void AttackPlayer()
    {
        Vector3 directionToPlayer = (player.position - projectileSpawnPoint.position).normalized;
       
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.LookRotation(directionToPlayer));

            // Configurar la dirección y velocidad del proyectil
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = directionToPlayer * bulletForce;
            }

    }

    void OnDrawGizmosSelected()
    {
        // Dibujar el radio de detección en el editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}



