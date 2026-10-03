using UnityEngine;

public class DoorSlamPressurePlate : MonoBehaviour
{
    public SimpleDoor doorToClose;

    private bool triggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;
            doorToClose.SlamShut();
        }
    }
}
