using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingRockOscillator : MonoBehaviour
{

    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float movementPeriod = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (movementPeriod < Mathf.Epsilon) {
            return;
        }
        float cyclePosition = Time.time / movementPeriod; // calculate how far game time is into the current movement period
        const float tau = Mathf.PI * 2;

        float sinWavePosition = Mathf.Sin(cyclePosition * tau); // going from -1 to 1
        movementFactor = (sinWavePosition + 1f) / 2; // recalculated to go from 0 to 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
