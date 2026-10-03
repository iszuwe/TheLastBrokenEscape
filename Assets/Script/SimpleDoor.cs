using UnityEngine;
using Cinemachine;

public class SimpleDoor : MonoBehaviour
{
    private bool isPlayerNear = false;
    private bool isOpen = false;
    private bool isClosing = false;

    private Quaternion openRotation;
    private Quaternion closeRotation;

    public float openAngle = 90f;
    public float openSpeed = 2f;
    public float slamSpeed = 5f; // Faster for dramatic slam

    [Header("Audio")]
    public AudioSource doorOpenSound;
    public AudioSource doorSlamSound;

    [Header("Cutscene Camera")]
    public CinemachineVirtualCamera slamCam;  // DRAG in Inspector
    public CinemachineVirtualCamera playerCam; // DRAG in Inspector!

    public float cutsceneDuration = 2f;

    void Start()
    {
        closeRotation = transform.rotation;
        openRotation = transform.rotation * Quaternion.Euler(0, openAngle, 0);

        //  Debug checks
        if (slamCam == null)
            Debug.LogError(" SimpleDoor: slamCam is NULL! Drag it in Inspector!");

        if (playerCam == null)
            Debug.LogError(" SimpleDoor: playerCam is NULL! Drag it in Inspector!");
    }

    void Update()
    {
        if (isPlayerNear && !isOpen && Input.GetKeyDown(KeyCode.E))
        {
            isOpen = true;
            //doorOpenSound?.Play();
        }

        if (isOpen && !isClosing)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, openRotation, Time.deltaTime * openSpeed);
        }

        if (isClosing)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, closeRotation, Time.deltaTime * slamSpeed);
        }
    }

    public void SlamShut()
    {
        isClosing = true;
        isOpen = false; // Stop opening

        //doorSlamSound?.Play();

        if (slamCam != null && playerCam != null)
        {
            slamCam.Priority = 11;
            playerCam.Priority = 1;

            Invoke("ReturnToPlayerCam", cutsceneDuration);
        }
        else
        {
            Debug.LogError(" SimpleDoor: Camera references are NULL in SlamShut!");
        }
    }

    void ReturnToPlayerCam()
    {
        if (slamCam != null && playerCam != null)
        {
            slamCam.Priority = 1;
            playerCam.Priority = 11;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
