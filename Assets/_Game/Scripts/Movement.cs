using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private InputAction thrust;
    [SerializeField] private InputAction rotate;
    [SerializeField] private float thrustForce = 100f;
    [SerializeField] private float rotateForce = 100f;
    [SerializeField] private AudioSource thrustSound;
    [SerializeField] private AudioClip mainEngine;
    [SerializeField] private ParticleSystem mainThrustParticles;
    [SerializeField] private ParticleSystem leftThrustParticles;
    [SerializeField] private ParticleSystem rightThrustParticles;
    
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        thrust.Enable();
        rotate.Enable();
    }

    private void FixedUpdate()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (thrust.IsPressed())
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustForce * Time.fixedDeltaTime);

        if (!thrustSound.isPlaying)
        {
            thrustSound.PlayOneShot(mainEngine);
        }
        if (!mainThrustParticles.isPlaying)
        {
            mainThrustParticles.Play();
        }
    }

    private void StopThrusting()
    {
        thrustSound.Stop();
        mainThrustParticles.Stop();
    }

    private void ProcessRotation()
    {
        float rotationValue = rotate.ReadValue<float>();

        if (rotationValue < 0)
        {
            RotateRight();
        }
        else if (rotationValue > 0)
        {
            RotateLeft();
        }
        else
        {
            StopRotating();
        }
    }

    private void RotateRight()
    {
        RotateRocket(rotateForce);

        if (!rightThrustParticles.isPlaying)
        {
            leftThrustParticles.Stop();
            rightThrustParticles.Play();
        }
    }

    private void RotateLeft()
    {
        RotateRocket(-rotateForce);

        if (!leftThrustParticles.isPlaying)
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Play();
        }
    }

    private void StopRotating()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }

    private void RotateRocket(float rotationVector)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward, rotationVector * Time.fixedDeltaTime);
        rb.freezeRotation = false;
    }
}
