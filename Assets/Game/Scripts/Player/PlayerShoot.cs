using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public GameObject projectilePrefab;

    public Transform firePoint;

    public float bulletForce = 7f;

    private float tiempoUltimoDisparo;

    [SerializeField] private float cadenciaDisparo = 0.3F;


    private void Update()
    {


        if (Input.GetMouseButtonDown(0) && Time.time - tiempoUltimoDisparo > cadenciaDisparo)
        {
            ShootProjectile();
        }
    }
    protected virtual void ShootProjectile()
    {
        //tiempoUltimoDisparo = Time.time;

        // Instancia la bala en el punto de disparo y con la rotación del punto de disparo
        GameObject bullet = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

        // Obtén el componente Rigidbody de la bala
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // Aplica una fuerza a la bala en la dirección del punto de disparo
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);

        Destroy(bullet, 3f);

    }
}
