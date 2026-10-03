using UnityEngine;

public class SecretDoor : MonoBehaviour
{
    private bool isOpen = false;
    private Quaternion targetRotation;

    void Start()
    {
        targetRotation = transform.rotation * Quaternion.Euler(0, 0, 80); // Open by rotating 90 degrees
    }

    public void OpenDoor()
    {
        isOpen = true;
    }

    void Update()
    {
        if (isOpen)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        }
    }
}
