using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float currentHealth;


    public GameObject deathEffect;

    private void Start()
    {

    }


    private void Update()
    {
        if (currentHealth <= 0)
        {
            Die();
        }
    }
    public void TakeDamage(float damage)
    {

        currentHealth -= damage;

    }


    private void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Debug.Log("animacion muerte instanciada");
        }

        Destroy(gameObject);
    }
}
