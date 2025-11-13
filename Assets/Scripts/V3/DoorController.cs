using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject targetDoor;
    public GameObject player;

    public Vector3 targetDoorPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (targetDoor != null)
        {
            targetDoorPosition = targetDoor.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnterDoor()
    {
        player.transform.position = new Vector3(targetDoorPosition.x, targetDoorPosition.y, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerControllerV3>().doorController = this;
        }
    }
}
