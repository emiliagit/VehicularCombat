using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyCount : MonoBehaviour
{
    public TextMeshProUGUI enemyCounterText;

    public PlayerHealth playerHealth;

    void Update()
    {
        CountEnemies();

    }

    public void CountEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int enemyCount = enemies.Length;
        enemyCounterText.text = "Enemies left: " + enemyCount;

        if(enemyCount == 0 && playerHealth.hp > 1)
        {
            SceneManager.LoadScene("Victory");
        }
    }
    
}
