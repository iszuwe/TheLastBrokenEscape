using UnityEngine;
using Cinemachine;


public class DoorInteraction : MonoBehaviour
{
    private bool isPlayerNear = false;
    private bool isOpen = false;
    private Quaternion targetRotation;

    [Header("Audio")]
    public AudioSource doorSound;

    [Header("Jumpscare Settings")]
    public GameObject ghostJumpscare;
    public float scareDuration = 3f;
    public CinemachineVirtualCamera jumpscareCam;   // Drag in Inspector 
    public CinemachineVirtualCamera playerCam;      // Will find at runtime 

    void Start()
    {
        targetRotation = transform.rotation * Quaternion.Euler(0, 90, 0);

        if (ghostJumpscare != null)
            ghostJumpscare.SetActive(false);

        // If playerCam not dragged, try to find it!
        if (playerCam == null)
        {
            playerCam = FindObjectOfType<CinemachineVirtualCamera>();

            if (playerCam == null)
                Debug.LogError(" DoorInteraction: playerCam is NULL — could not find any CinemachineVirtualCamera!");
            else
                Debug.Log(" DoorInteraction: playerCam found at runtime: " + playerCam.name);
        }

        if (jumpscareCam == null)
            Debug.LogError(" DoorInteraction: jumpscareCam is NULL — drag it in the Inspector!");
    }

    void Update()
    {
        if (isPlayerNear && !isOpen && Input.GetKeyDown(KeyCode.E))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            PlayerInventory inv = player.GetComponent<PlayerInventory>();

            if (inv != null && inv.hasKey)
            {
                isOpen = true;
                UIManager.instance.HideDoorPrompt();
                TriggerJumpscare();
            }
        }

        if (isOpen)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            GameObject player = other.gameObject;
            PlayerInventory inv = player.GetComponent<PlayerInventory>();

            if (inv != null && inv.hasKey)
                UIManager.instance.ShowDoorPrompt("Press E to break the door");
            else
                UIManager.instance.ShowDoorPrompt("Door is locked. Need to use something to open it.");
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

    void TriggerJumpscare()
    {
        if (ghostJumpscare != null)
        {
            ghostJumpscare.SetActive(true);

            if (jumpscareCam != null && playerCam != null)
            {
                jumpscareCam.Priority = 11;
                playerCam.Priority = 1;

                if (doorSound != null) doorSound.Play();
            }
            else
            {
                Debug.LogError(" doorinteraction: missing camera reference during jumpscare!");
            }

            Invoke("HideJumpscare", scareDuration);
            Invoke("ReturnToPlayerCamera", scareDuration);
        }
    }

    void HideJumpscare()
    {
        if (ghostJumpscare != null)
            ghostJumpscare.SetActive(false);
    }

    void ReturnToPlayerCamera()
    {
        if (jumpscareCam != null && playerCam != null)
        {
            jumpscareCam.Priority = 1;
            playerCam.Priority = 11;
        }
    }
}
