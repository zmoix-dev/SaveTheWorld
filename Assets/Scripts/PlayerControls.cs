using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody body;
    private AudioSource audioSrc;
    [SerializeField] AudioClip rocketBoost;
    [SerializeField] AudioClip rocketDown;
    private bool wasBoosting;

    [SerializeField] float degRotation = 90f;
    [SerializeField] float boostSpd = 2500f;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
        wasBoosting = false;
    }

    // Update is called once per frame
    void Update()
    {
        Boost();
        HandleRotation();
    }

    private void Boost() {
        bool isBoosting = Input.GetKey(KeyCode.Space);
        if (isBoosting) {
            body.AddRelativeForce(Vector3.up * Time.deltaTime * boostSpd);
            if (!wasBoosting) {
                audioSrc.Stop();
                audioSrc.PlayOneShot(rocketBoost);
                wasBoosting = true;
            }
        } else {
            if(wasBoosting) {
                audioSrc.Stop();
                audioSrc.PlayOneShot(rocketDown);
                wasBoosting = false;
            }
        }
    }

    private void HandleRotation() {
        bool isRotateRight = Input.GetKey(KeyCode.D);
        bool isRotateLeft = Input.GetKey(KeyCode.A);

        if (isRotateLeft) {
            ApplyRotation(degRotation);
        }
        if (isRotateRight) {
            ApplyRotation(-degRotation);
        }
    }

    private void ApplyRotation(float rotate) {
        body.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotate * Time.deltaTime);
        body.freezeRotation = false;
    }
}
