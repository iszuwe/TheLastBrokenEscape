using UnityEngine;

public class PressurePlateScare : MonoBehaviour
{
    public GameObject scareObject; // The ghost or scare prefab
    public AudioSource scareSound; // Optional

    private bool triggered = false;

    void Start()
    {
        if (scareObject != null)
            scareObject.SetActive(false); // Hide ghost at start
    }

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
        {
            triggered = true;

            if (scareObject != null)
                scareObject.SetActive(true); // Show ghost

            if (scareSound != null)
                scareSound.Play();

            Invoke("HideScare", 2f); // 2 seconds

        }
    }

    void HideScare()
    {
        if (scareObject != null)
            scareObject.SetActive(false);
    }

}
