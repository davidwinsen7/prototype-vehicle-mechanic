using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car Physics")]
    public float motorForce;
    public float brakingForce;
    public float steerAngle;

    [Header("Wheels")]
    public Transform frontLeftWheel;
    public Transform frontRightWheel;
    public Transform rearLeftWheel;
    public Transform rearRightWheel;

    [Header("Wheel Colliders")]
    public WheelCollider frontLeftWheelColl;
    public WheelCollider frontRightWheelColl;
    public WheelCollider rearLeftWheelColl;
    public WheelCollider rearRightWheelColl;

    float xInput;
    float zInput;
    bool isHandBraking;

    const string HORIZONTAL = "Horizontal";
    const string VERTICAL = "Vertical";

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        HandleMotors();
        HandleSteering();

        // Debug.Log("Is Handbraking = " + isHandBraking);
        // Debug.Log("Current zInput = " + zInput);
    }

    void HandleMotors()
    {
        rearLeftWheelColl.motorTorque = zInput * motorForce;
        rearRightWheelColl.motorTorque = zInput * motorForce;
       
        float currBrakeForce = isHandBraking ? brakingForce : 0f;
        ApplyBrake(currBrakeForce);
    }

    void ApplyBrake(float brakeForce)
    {
        rearLeftWheelColl.brakeTorque = brakeForce;
        rearRightWheelColl.brakeTorque = brakeForce;
        // frontLeftWheelColl.brakeTorque = brakeForce;
        // frontRightWheelColl.brakeTorque = brakeForce;
    }

    void HandleSteering()
    {
        float currSteerAngle = xInput * steerAngle;
        frontLeftWheelColl.steerAngle = currSteerAngle;
        frontRightWheelColl.steerAngle = currSteerAngle;

        UpdateWheels();
    }

    void UpdateWheels()
    {
        UpdateWheel(frontRightWheelColl, frontRightWheel);
        UpdateWheel(frontLeftWheelColl, frontLeftWheel);
        UpdateWheel(rearRightWheelColl, rearRightWheel);
        UpdateWheel(rearLeftWheelColl, rearLeftWheel);
    }

    void UpdateWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }

    void GetInput()
    {
        xInput = Input.GetAxis(HORIZONTAL);
        zInput = Input.GetAxis(VERTICAL);
        isHandBraking = Input.GetKey(KeyCode.Space);
    }
}
