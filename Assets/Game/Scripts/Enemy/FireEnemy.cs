using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemy : MonoBehaviour
{
    public GameObject firePrefab;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Colision con player");
            GameObject fire = Instantiate(firePrefab, transform.position, Quaternion.identity);
            Destroy(fire, 1f);
            Destroy(gameObject);
        }
        if (other.gameObject.TryGetComponent(out PlayerHealth player))
        {
            player.RecibirDanio(10);
            Debug.Log("vida restada");
        }

    }
}
