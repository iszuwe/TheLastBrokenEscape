using UnityEngine;
using Cinemachine;

public class FinalEscapeLever : MonoBehaviour
{
    public EscapeFrontDoor frontDoor;   // Drag your front door controller here
    public float leverUpAngle = -45f;

    private bool isPlayerNear = false;
    private bool isActivated = false;
    private Quaternion startRotation;
    private Quaternion targetRotation;

    [Header("Cutscene Camera")]
    public CinemachineVirtualCamera doorCutsceneCam;  // Drag in Inspector
    public CinemachineVirtualCamera playerCam;        // Drag in Inspector
    public float cutsceneDuration = 3f;

    void Start()
    {
        startRotation = transform.rotation;
        targetRotation = transform.rotation * Quaternion.Euler(0, 0, leverUpAngle);

        if (doorCutsceneCam == null)
            Debug.LogError(" FinalEscapeLever: doorCutsceneCam is NULL! Drag it in Inspector.");

        if (playerCam == null)
            Debug.LogError(" FinalEscapeLever: playerCam is NULL! Drag it in Inspector.");
    }

    void Update()
    {
        if (isPlayerNear && !isActivated && Input.GetKeyDown(KeyCode.E))
        {
            isActivated = true;
            UIManager.instance.ShowLeverPrompt(false);

            if (frontDoor != null)
            {
                frontDoor.UnlockAndOpenDoor();
            }
            else
            {
                Debug.LogError("FinalEscapeLever: frontDoor reference is NULL!");
            }

            if (doorCutsceneCam != null && playerCam != null)
            {
                doorCutsceneCam.Priority = 11;
                playerCam.Priority = 1;

                Invoke("ReturnToPlayerCam", cutsceneDuration);
            }
            else
            {
                Debug.LogError(" FinalEscapeLever: Missing camera references!");
            }
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

    void ReturnToPlayerCam()
    {
        if (doorCutsceneCam != null && playerCam != null)
        {
            doorCutsceneCam.Priority = 1;
            playerCam.Priority = 11;
        }
    }
}
