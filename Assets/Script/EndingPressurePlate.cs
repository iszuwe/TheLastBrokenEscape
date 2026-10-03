using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using StarterAssets; //  Add this for Starter Assets namespace!

public class EndingPressurePlate : MonoBehaviour
{
    [Header("Cutscene Camera")]
    public CinemachineVirtualCamera endingCam;
    public CinemachineVirtualCamera playerCam;
    public float cutsceneDuration = 4f;

    [Header("Ending UI")]
    public GameObject endingPanel;

    private bool hasEnded = false;

    void OnTriggerEnter(Collider other)
    {
        if (hasEnded) return;

        if (other.CompareTag("Player"))
        {
            hasEnded = true;
            Debug.Log("Ending triggered!");

            if (endingCam != null && playerCam != null)
            {
                endingCam.Priority = 11;
                playerCam.Priority = 1;
            }

            if (endingPanel != null)
                endingPanel.SetActive(true);

            //  Correct: disable Starter Assets controller!
            ThirdPersonController controller = other.GetComponent<ThirdPersonController>();
            if (controller != null)
                controller.enabled = false;

            Invoke("ReturnToMainMenu", cutsceneDuration);
        }
    }

    void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
