using UnityEngine;

public class EscapeFrontDoor : MonoBehaviour
{
    private bool isUnlocked = false;
    private bool isOpening = false;

    private Quaternion[] targetRotations;

    [Header("Multiple Doors")]
    public Transform[] doorPanels;   // Drag your 4 door panels here
    public float openAngle = 90f;
    public float openSpeed = 2f;

    [Header("Audio")]
    public AudioSource doorOpenSound;

    private bool isPlayerNear = false;

    void Start()
    {
        targetRotations = new Quaternion[doorPanels.Length];
        for (int i = 0; i < doorPanels.Length; i++)
        {
            targetRotations[i] = doorPanels[i].rotation * Quaternion.Euler(0, openAngle, 0);
        }
    }

    void Update()
    {
        if (isOpening)
        {
            for (int i = 0; i < doorPanels.Length; i++)
            {
                doorPanels[i].rotation = Quaternion.Slerp(
                    doorPanels[i].rotation,
                    targetRotations[i],
                    Time.deltaTime * openSpeed
                );
            }
        }

        // Show locked message if player is near but door is locked
        if (isPlayerNear && !isUnlocked)
        {
            UIManager.instance.ShowDoorPrompt("The door is locked.");
        }
        else if (!isPlayerNear)
        {
            UIManager.instance.HideDoorPrompt();
        }
    }

    public void UnlockAndOpenDoor()
    {
        isUnlocked = true;
        isOpening = true;
        //doorOpenSound?.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            isPlayerNear = true;
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
