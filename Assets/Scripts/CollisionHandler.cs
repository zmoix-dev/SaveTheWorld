using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] AudioClip crashClip;
    [SerializeField] AudioClip clearClip;
    [SerializeField] ParticleSystem clearParticles;
    [SerializeField] ParticleSystem crashParticles;

    Rigidbody body;
    private AudioSource audioSrc;

    bool levelResolved;
    bool ignoreCollision;
    

    void Start() {
        body = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
        levelResolved = false;
        ignoreCollision = false;
    }

    void Update()
    {
        ToggleCollision();
    }

    private void ToggleCollision()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ignoreCollision = !ignoreCollision;
        }
    }

    void OnCollisionEnter(Collision other) {
        if(levelResolved || ignoreCollision) {
            return;
        }
        
        switch (other.gameObject.tag) {
            case "Friendly": 
                break;
            case "Fuel":
                break;
            case "Finish":
                StartVictorySequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void StartCrashSequence() {
        gameObject.GetComponent<PlayerControls>().enabled = false;
        PlayCrashAssets();
        HidePlayerModel();
        Invoke("ReloadLevel", 1f);
        levelResolved = true;
    }

    private void PlayCrashAssets() {
        audioSrc.Stop();
        audioSrc.PlayOneShot(crashClip);
        crashParticles.Play();
    }

    private void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void StartVictorySequence() {
        gameObject.GetComponent<PlayerControls>().enabled = false;
        PlayClearAssets();
        HidePlayerModel();
        Invoke("LoadNextLevel", 1f);
        levelResolved = true;
    }

    private void PlayClearAssets() {
        audioSrc.Stop();
        audioSrc.PlayOneShot(clearClip);
        clearParticles.Play();
    }

    private void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(++currentSceneIndex);
    }

    private void HidePlayerModel() {
        MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>();
        foreach (MeshRenderer r in renderers) {
            r.enabled = false;
        }
        // turn off movement so the camera doesn't keep moving
        body.freezeRotation = true;
        body.useGravity = false;
    }
}
