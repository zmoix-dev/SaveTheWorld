using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{

    [SerializeField] AudioClip crashClip;
    [SerializeField] AudioClip clearClip;

    Rigidbody body;
    private AudioSource audioSrc;

    bool levelResolved;
    

    void Start() {
        body = GetComponent<Rigidbody>();
        audioSrc = GetComponent<AudioSource>();
        levelResolved = false;
    }

    void OnCollisionEnter(Collision other) {
        if(levelResolved) {
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
        PlayCrashSound();
        Invoke("ReloadLevel", 1f);
        levelResolved = true;
    }

    private void PlayCrashSound() {
        audioSrc.Stop();
        audioSrc.PlayOneShot(crashClip);
    }

    private void ReloadLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    private void StartVictorySequence() {
        gameObject.GetComponent<PlayerControls>().enabled = false;
        PlayVictorySound();
        Invoke("LoadNextLevel", 1f);
        levelResolved = true;
    }

    private void PlayVictorySound() {
        audioSrc.Stop();
        audioSrc.PlayOneShot(clearClip);
    }

    private void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(++currentSceneIndex);
    }
}
