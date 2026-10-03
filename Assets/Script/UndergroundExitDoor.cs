using UnityEngine;

public class UndergroundExitDoor : MonoBehaviour
{
    private bool isPlayerNear = false;
    private bool isOpen = false;
    private Quaternion targetRotation;

    public float openAngle = 90f;
    public float openSpeed = 2f;
    public AudioSource doorSound;

    void Start()
    {
        targetRotation = transform.rotation * Quaternion.Euler(0, openAngle, 0);
    }

    void Update()
    {
        if (isPlayerNear && !isOpen && Input.GetKeyDown(KeyCode.E))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            UndergroundInventory inv = player.GetComponent<UndergroundInventory>();

            if (inv != null && inv.hasUndergroundKey)
            {
                isOpen = true;
                //doorSound?.Play();
                UIManager.instance.HideDoorPrompt();
            }
            else
            {
                UIManager.instance.ShowDoorPrompt("The door is locked. Find the key.");
            }
        }

        if (isOpen)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * openSpeed);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;

            GameObject player = other.gameObject;
            UndergroundInventory inv = player.GetComponent<UndergroundInventory>();

            if (inv != null && inv.hasUndergroundKey)
                UIManager.instance.ShowDoorPrompt("Press E to open the door");
            else
                UIManager.instance.ShowDoorPrompt("The door is locked. Find the key.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            UIManager.instance.HideDoorPrompt();
        }
    }
}
