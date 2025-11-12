using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    //[SerializeField] private bool triggerActivate;
    public PlayerControllerV3 playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerV3>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //triggerActivate = true;

        if (other.gameObject.CompareTag("Platform"))
        {
            playerController.isGrounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        //triggerActivate = false;

        if (other.gameObject.CompareTag("Platform"))
        {
            playerController.isGrounded = false;
        }
    }
}
