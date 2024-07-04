using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyCount : MonoBehaviour
{
    public TextMeshProUGUI enemyCounterText;
    void Update()
    {
        CountEnemies();
    }

    void CountEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        int enemyCount = enemies.Length;
        enemyCounterText.text = "Enemies left: " + enemyCount;
    }
}
