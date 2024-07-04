using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePlayer : MonoBehaviour
{
    public GameObject firePrefab;



    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player"))
        {
            GameObject fire = Instantiate(firePrefab, transform.position, Quaternion.identity);
            Destroy(fire, 1f);
            Debug.Log("Colision");
            Destroy(gameObject);
        }

        if (other.gameObject.TryGetComponent(out EnemyHealth enemy))
        {
            enemy.TakeDamage(10);
            Debug.Log("da�o de 10 a enemigo");
        }

    }
}
