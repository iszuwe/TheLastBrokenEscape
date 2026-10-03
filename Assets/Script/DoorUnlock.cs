using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    private bool isUnlocked = false;
    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = transform.rotation * Quaternion.Euler(0, 90, 0); // open 90 degrees
    }

    public void UnlockDoor()
    {
        isUnlocked = true;
        GetComponent<AudioSource>()?.Play(); // play creak sound
    }

    void Update()
    {
        if (isUnlocked)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        }
    }
}
