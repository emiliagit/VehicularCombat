using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float moveSpeed = 5.0f;
    public float rotationSpeed = 120.0f;
    public GameObject[] leftWheels;
    public GameObject[] rightWheels;

    public float wheelsRotationSpeed = 200.0f;

    private Rigidbody rb;
    private float moveInput;
    private float rotationInput;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = Input.GetAxis("Vertical");
        rotationInput = Input.GetAxis("Horizontal");

        RotationWheels(moveInput, rotationInput);

    }

    private void FixedUpdate()
    {
        MoveTankObj(moveInput);
        RotateTank(rotationInput);
    }

    void MoveTankObj(float input)
    {
        Vector3 moveDirection = transform.forward * input * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + moveDirection);
    }

    void RotateTank( float input)
    {
        float rotation = input * rotationSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, rotation, 0f);

        rb.MoveRotation(rb.rotation * turnRotation);
    }

    void RotationWheels(float moveInput, float rotationInput)
    {
        float wheelMoveRotation = moveInput * wheelsRotationSpeed * Time.deltaTime;
        float wheelTurnRotation = rotationInput * wheelsRotationSpeed * Time.deltaTime;

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
}
