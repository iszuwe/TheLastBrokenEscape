using UnityEngine;

public class UndergroundKeyPickup : MonoBehaviour
{
    private bool isPlayerNear = false;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            UndergroundInventory inv = player.GetComponent<UndergroundInventory>();
            if (inv != null)
            {
                inv.hasUndergroundKey = true;
                UIManager.instance.ShowKeyPrompt(false);
                Destroy(gameObject); // remove key from scene
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            UIManager.instance.ShowKeyPrompt(true); // e.g., "Press E to pick up key"
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            UIManager.instance.ShowKeyPrompt(false);
        }
    }
}
