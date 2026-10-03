using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private bool isPlayerNear = false;
    private GameObject player;

    void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            PlayerInventory playerInv = player.GetComponent<PlayerInventory>();
            if (playerInv != null)
            {
                playerInv.hasKey = true;
                UIManager.instance.ShowScrewdriverPrompt(false);
                Destroy(gameObject); // remove key
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            player = other.gameObject;
            UIManager.instance.ShowScrewdriverPrompt(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            player = null;
            UIManager.instance.ShowScrewdriverPrompt(false);
        }
    }
}
