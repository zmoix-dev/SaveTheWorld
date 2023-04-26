using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelCollisionHandler : MonoBehaviour
{

    AudioSource audioSrc;
    MeshRenderer meshRenderer;

    void Start() {
        audioSrc = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
    }


    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Player") {
           // meshRenderer.enabled = false;
            audioSrc.Play();
            Destroy(gameObject);
        }
    }
}
