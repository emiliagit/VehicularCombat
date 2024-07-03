using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GridBrushBase;

public class EnemyMovement : MonoBehaviour
{
    public EnemyState currentState;
    public Transform player;
    public float detectionRange = 20f;
    public float stopDistance = 5f;
    public float moveSpeed = 5f;
    public float rotationSpeed = 3f;

    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                LookForPlayer();
                break;
            case EnemyState.Chasing:
                ChasePlayer();
                break;
           
        }
    }

    private void LookForPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer >= stopDistance && distanceToPlayer <= detectionRange)
        {
            currentState = EnemyState.Chasing;
        }
       
    }

    private void ChasePlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stopDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * moveSpeed * Time.deltaTime;

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            currentState = EnemyState.Idle;
        }


    }


}
