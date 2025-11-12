using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class KeyV3 : MonoBehaviour
{
    //program so that when the player is collided with, the key is destroyed and "hasKey" is gameManager is set to true.

    public GameManagerV3 gameManager;
    public AudioClip pickupSound;
    private AudioSource audioSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitialSetup();
    }

    private void InitialSetup()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManagerV3>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.hasKey = true;

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            audioSource.PlayOneShot(pickupSound);

            Destroy(gameObject, pickupSound.length);
        }
    }
}
