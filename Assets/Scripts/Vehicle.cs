using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Vehicle : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float acceleration = 1.0f;
    public float rotateSpeed = 1.0f;
    public float deacceleration = 3f;
    public float reverseAcceleration = 0.8f;
    public float sideDrag = 1f;
    public float drag = 0.1f;

    public bool isAccelerating;

    public ParticleSystem driftSmoke;
    public TrailRenderer driftTrail1;
    public TrailRenderer driftTrail2;

    public AnimationCurve pitchCurve;
    public AnimationCurve rotateSpeedCurve;

    public Transform[] wheels;

    Rigidbody rb;
    AudioSource engineSound;
    float speedRatio;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        engineSound = GetComponent<AudioSource>();
        rb.maxLinearVelocity = maxSpeed;
    }

    void Update()
    {
        print((int)(rb.velocity.magnitude) + " m/s");
        speedRatio = rb.velocity.magnitude / maxSpeed;
        engineSound.pitch = pitchCurve.Evaluate(speedRatio);

        var localVelocity = transform.InverseTransformVector(rb.velocity);

        //drag
        rb.velocity += -transform.right * localVelocity.x * sideDrag * Time.deltaTime;

        print(localVelocity);

        // Check for drifting
        bool isDrifting = Mathf.Abs(localVelocity.x) > 1f;

        // drifting
        if (isDrifting && isAccelerating)
        {
            print("drift");
            driftSmoke.gameObject.SetActive(true);
            driftTrail1.gameObject.SetActive(true);
            driftTrail2.gameObject.SetActive(true);
        }
        else
        {
            driftSmoke.gameObject.SetActive(false);
            driftTrail1 .gameObject.SetActive(false);
            driftTrail2.gameObject.SetActive(false);
        }

        
        isAccelerating = false;
    }

    public void Steer(float value)
    {
        value = Mathf.Clamp();

        transform.Rotate(0, value * rotateSpeed * rotateSpeedCurve.Evaluate(speedRatio) * Time.deltaTime, 0);
    }

    public void Accelerate()
    {
        rb.velocity += transform.forward * acceleration * Time.deltaTime;
        isAccelerating = true;
    }

    public void Brake()
    {
        var acc = speedRatio > 0 ? deacceleration : reverseAcceleration;
        rb.velocity += -transform.forward * acc * Time.deltaTime;
    }
}