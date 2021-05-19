using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(EdgeCollider2D), typeof(HingeJoint2D))]
public class LineMotor : MonoBehaviour, IMotor
{
    [Header("References")]

    private Rigidbody2D rigidBody;
    private EdgeCollider2D edgeCollider;
    private HingeJoint2D motor;

    private void Start()
    {
        // Get references
        rigidBody = GetComponent<Rigidbody2D>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        motor = GetComponent<HingeJoint2D>();
    }

    public Rigidbody2D GetRigidBody()
    {
        return rigidBody;
    }

    public void ConnectRigidBody(Rigidbody2D bodyConnect)
    {
        motor.connectedBody = bodyConnect;
    }

    public void SetPoints(Vector2[] points)
    {
        edgeCollider.SetPoints(new List<Vector2>(points));
    }

    public void SetSpeed(float speed)
    {
        JointMotor2D m = motor.motor;

        m.motorSpeed = speed;

        motor.motor = m;
    }

    public void SetForce(float force)
    {
        JointMotor2D m = motor.motor;

        m.maxMotorTorque = force;

        motor.motor = m;
    }
}
