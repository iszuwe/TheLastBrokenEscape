using UnityEngine;
using Cinemachine;

public class SecretDoorLever : MonoBehaviour
{
    public GameObject secretDoor; // The hidden door underground
    public float leverUpAngle = -45f;

    private bool isPlayerNear = false;
    private bool isActivated = false;

    private Quaternion startRotation;
    private Quaternion targetRotation;

    [Header("Cutscene Camera")]
    public CinemachineVirtualCamera secretDoorCam; // Drag this in Inspector
    public CinemachineVirtualCamera playerCam;     // Drag this in Inspector

    public float cutsceneDuration = 3f;

    public AudioSource doorSound;

    void Start()
    {
        startRotation = transform.rotation;
        targetRotation = transform.rotation * Quaternion.Euler(0, 0, leverUpAngle);

        // Debug checks for missing cameras
        if (secretDoorCam == null)
            Debug.LogError(" SecretDoorLever: secretDoorCam is NULL! Drag it in the Inspector.");
        if (playerCam == null)
            Debug.LogError(" SecretDoorLever: playerCam is NULL! Drag it in the Inspector.");
    }

    void Update()
    {
        if (isPlayerNear && !isActivated && Input.GetKeyDown(KeyCode.E))
        {
            isActivated = true;

            // Open the hidden door
            secretDoor.GetComponent<SecretDoor>().OpenDoor();

            // Hide UI prompt
            UIManager.instance.ShowLeverPrompt(false);

            // Play cutscene
            PlayDoorCutscene();
        }

        if (isActivated)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            isPlayerNear = true;
            UIManager.instance.ShowLeverPrompt(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            isPlayerNear = false;
            UIManager.instance.ShowLeverPrompt(false);
        }
    }

    void PlayDoorCutscene()
    {
        if (secretDoorCam == null || playerCam == null)
        {
            Debug.LogError(" SecretDoorLever: One of the cameras is NULL during PlayDoorCutscene!");
            return;
        }

        secretDoorCam.Priority = 11; // Switch to cutscene cam
        playerCam.Priority = 1;      // Lower player cam

         doorSound.Play();

        Invoke("ReturnToPlayerCamera", cutsceneDuration);
    }

    void ReturnToPlayerCamera()
    {
        if (secretDoorCam != null) secretDoorCam.Priority = 1;
        if (playerCam != null) playerCam.Priority = 11;
    }
}
