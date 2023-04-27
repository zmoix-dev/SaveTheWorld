using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
    private Rigidbody body;
    private AudioSource audioSrc;
    [SerializeField] AudioClip rocketBoost;
    [SerializeField] AudioClip rocketDown;
    [SerializeField] ParticleSystem mainBoostParticles;
    // left boost goes right, right boost goes left
    [SerializeField] ParticleSystem leftBoostParticles;
    [SerializeField] ParticleSystem rightBoostParticles;
    private bool wasBoosting;

    [SerializeField] float degRotation = 90f;
    [SerializeField] float boostSpd = 2500f;

    [SerializeField] bool isDebugEnabled = false;
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
        if (isDebugEnabled) {
            LoadNextLevel();
        }
        
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
            if(!mainBoostParticles.isPlaying) {
                mainBoostParticles.Play();
            }
        } else {
            if(wasBoosting) {
                audioSrc.Stop();
                audioSrc.PlayOneShot(rocketDown);
                mainBoostParticles.Stop();
                wasBoosting = false;
            }
        }
    }

    private void HandleRotation()
    {
        bool isRotateRight = Input.GetKey(KeyCode.D);
        bool isRotateLeft = Input.GetKey(KeyCode.A);
        HandleLeftRotation(isRotateLeft);
        HandleRightRotation(isRotateRight);
    }

    private void HandleRightRotation(bool isRotateRight)
    {
        if (isRotateRight)
        {
            ApplyRotation(-degRotation);
        }
        HandleParticles(leftBoostParticles, isRotateRight);
    }

    private void HandleLeftRotation(bool isRotateLeft)
    {
        if (isRotateLeft)
        {
            ApplyRotation(degRotation);
        }
        HandleParticles(rightBoostParticles, isRotateLeft);
    }

    private void HandleParticles(ParticleSystem particles, bool on) {
        if (on && !particles.isPlaying) {
            particles.Play();
        } else if (!on && particles.isPlaying) {
            particles.Stop();
        }
    }

    private void ApplyRotation(float rotate) {
        body.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotate * Time.deltaTime);
        body.freezeRotation = false;
    }

    private void LoadNextLevel() {
        if(Input.GetKey(KeyCode.L)) {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(++currentSceneIndex);
        }
    }
}
