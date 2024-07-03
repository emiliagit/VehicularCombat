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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AttackPlayer();
    }

    private void AttackPlayer()
    {
        aim.TurretAim();

        Vector3 directionToPlayer = (player.position - projectileSpawnPoint.position).normalized;
        if (Time.time >= nextFireTime)
        {
            GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.LookRotation(directionToPlayer));

            // Configurar la dirección y velocidad del proyectil
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = directionToPlayer * bulletForce;
            }

            nextFireTime = Time.time + 1f / fireRate; // Actualizar el tiempo para el próximo disparo
        }

    }
}
