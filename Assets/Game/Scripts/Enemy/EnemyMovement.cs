using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GridBrushBase;

public class EnemyMovement : MonoBehaviour
{
    #region Members
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float wheelRotationSpeed;
    [SerializeField] private float wheelTurnRotationSpeed;
    private float enemyGasDirection;
    private float rotationDirection;

    [Header("AI Behaviour")]
    [SerializeField] private float turnThreshold;
    [SerializeField] private float chasingDistance;
    [SerializeField] private float idleDistance;

    [Header("Wheels")]
    [SerializeField] private GameObject[] leftWheels;
    [SerializeField] private GameObject[] rightWheels;

    [Header("Groundcheck")]
    [SerializeField] private float groundcheckRayMaxDistance;
    [SerializeField] private Vector3 boxSize;

    [Header("References")]
    private Transform player;
    private Rigidbody rigidBody;
    public EnemyState enemyState;

    public enum EnemyState
    {
        Chasing,
        Stationary,
        Idle
    }
    #endregion

    private void Start()
    {
        enemyState = EnemyState.Stationary;

        enemyGasDirection = 0f;
        rotationDirection = 0f;

        rigidBody = GetComponent<Rigidbody>();

        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        SetState();
        RotateWheels(enemyGasDirection, rotationDirection);
    }

    private void FixedUpdate()
    {
        if (IsGrounded())
        {
            Debug.Log("IS grounded");
            BehaviourSet();
        }
    }

    private void SetState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer >= chasingDistance && distanceToPlayer < idleDistance)
            enemyState = EnemyState.Chasing;
        else if (distanceToPlayer >= idleDistance)
            enemyState = EnemyState.Idle;
        else
            enemyState = EnemyState.Stationary;
    }

    private void BehaviourSet()
    {
        Debug.Log("BehaviourSet Called");
        switch (enemyState)
        {
            case EnemyState.Chasing:
                SetMoveDirection();
                Debug.Log("Chasing Player");
                MoveTank(enemyGasDirection);
                RotateTank(rotationDirection);
                break;
            case EnemyState.Stationary:
                enemyGasDirection = 0f;
                rotationDirection = 0f;
                break;
        }
    }

    private void SetMoveDirection()
    {
        Debug.Log("Setting Move Direction");
        int forwardQuadrant = FindQuadrant(transform.forward);

        Vector3 playerDirection = player.position - transform.position;
        playerDirection.Normalize();

        int toPlayerQuadrant = FindQuadrant(playerDirection);

        if (forwardQuadrant == 1)
        {
            switch (toPlayerQuadrant)
            {
                case 1:
                    rotationDirection = SameQuadrantMoveDirection(transform.forward, playerDirection, forwardQuadrant);
                    enemyGasDirection = 1f;
                    break;
                case 2:
                    rotationDirection = -1f;
                    enemyGasDirection = 1f;
                    break;
                case 3:
                    rotationDirection = 0f;
                    enemyGasDirection = -1f;
                    break;
                case 4:
                    rotationDirection = 1f;
                    enemyGasDirection = 1f;
                    break;
            }
        }
        else if (forwardQuadrant == 2)
        {
            switch (toPlayerQuadrant)
            {
                case 1:
                    rotationDirection = 1f;
                    enemyGasDirection = 1f;
                    break;
                case 2:
                    rotationDirection = SameQuadrantMoveDirection(transform.forward, playerDirection, forwardQuadrant);
                    enemyGasDirection = 1f;
                    break;
                case 3:
                    rotationDirection = -1;
                    enemyGasDirection = 1f;
                    break;
                case 4:
                    rotationDirection = 0f;
                    enemyGasDirection = -1f;
                    break;
            }
        }
        else if (forwardQuadrant == 3)
        {
            switch (toPlayerQuadrant)
            {
                case 1:
                    rotationDirection = 0f;
                    enemyGasDirection = -1f;
                    break;
                case 2:
                    rotationDirection = 1f;
                    enemyGasDirection = 1f;
                    break;
                case 3:
                    rotationDirection = SameQuadrantMoveDirection(transform.forward, playerDirection, forwardQuadrant);
                    enemyGasDirection = 1f;
                    break;
                case 4:
                    rotationDirection = -1f;
                    enemyGasDirection = 1f;
                    break;
            }
        }
        else
        {
            switch (toPlayerQuadrant)
            {
                case 1:
                    rotationDirection = -1f;
                    enemyGasDirection = 1f;
                    break;
                case 2:
                    rotationDirection = 0f;
                    enemyGasDirection = -1f;
                    break;
                case 3:
                    rotationDirection = 1f;
                    enemyGasDirection = 1f;
                    break;
                case 4:
                    rotationDirection = SameQuadrantMoveDirection(transform.forward, playerDirection, toPlayerQuadrant);
                    enemyGasDirection = 1f;
                    break;
            }
        }
    }

    public int FindQuadrant(Vector3 vector)
    {
        int quadrant;

        if (vector.x >= 0f)
        {
            if (vector.z >= 0f)
                quadrant = 1;
            else
                quadrant = 4;
        }
        else
        {
            if (vector.z >= 0f)
                quadrant = 2;
            else
                quadrant = 3;
        }

        return quadrant;
    }

    private float SameQuadrantMoveDirection(Vector3 forward, Vector3 playerDir, int currentQuad)
    {
        float rotation = 0f;

        float xFwdQuad = forward.x;
        float xPlayerQuad = playerDir.x;

        float xDiff = Mathf.Abs(xFwdQuad - xPlayerQuad);

        if (currentQuad == 1 || currentQuad == 2)
        {
            if (xDiff >= turnThreshold)
            {
                if (xFwdQuad < xPlayerQuad)
                    rotation = 1f;
                else
                    rotation = -1f;
            }

        }
        else
        {
            if (xDiff >= turnThreshold)
            {
                if (xFwdQuad > xPlayerQuad)
                    rotation = 1f;
                else
                    rotation = -1f;
            }
        }

        return rotation;
    }

    private void MoveTank(float input)
    {
        Debug.Log("Moving Tank with input: " + input);
        Vector3 moveDirection = input * moveSpeed * Time.fixedDeltaTime * transform.forward;
        rigidBody.MovePosition(rigidBody.position + moveDirection);
    }

    private void RotateTank(float input)
    {
        Debug.Log("Rotating Tank with input: " + input);
        float rotation = input * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, rotation, 0f);

        rigidBody.MoveRotation(rigidBody.rotation * turnRotation);
    }

    private void RotateWheels(float moveInput, float rotationInput)
    {
        float wheelMoveRotation = moveInput * wheelRotationSpeed * Time.deltaTime;
        float wheelTurnRotation = rotationInput * wheelTurnRotationSpeed * Time.deltaTime;

        foreach (GameObject wheel in leftWheels)
        {
            if (wheel != null)
            {
                if (wheelMoveRotation != 0f)
                    wheel.transform.Rotate(wheelMoveRotation, 0f, 0f);
                else
                    wheel.transform.Rotate(wheelTurnRotation, 0f, 0f);
            }
        }

        foreach (GameObject wheel in rightWheels)
        {
            if (wheel != null)
            {
                if (wheelMoveRotation != 0f)
                    wheel.transform.Rotate(wheelMoveRotation, 0f, 0f);
                else
                    wheel.transform.Rotate(-wheelTurnRotation, 0f, 0f);
            }
        }
    }

    private bool IsGrounded()
    {
        if (Physics.BoxCast(transform.position, boxSize, -transform.up, transform.rotation, groundcheckRayMaxDistance))
            return true;
        else
            return false;
    }


}
