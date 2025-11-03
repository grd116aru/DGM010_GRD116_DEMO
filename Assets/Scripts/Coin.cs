using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public AudioClip pickupSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.IncreaseScore(coinValue);

            // Hide coin so it looks collected
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            // Play sound
            audioSource.PlayOneShot(pickupSound);

            // Destroy after sound finishes
            Destroy(gameObject, pickupSound.length);
        }
    }
}
