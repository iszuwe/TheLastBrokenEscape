using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public TextMeshProUGUI keyPromptText;
    public TextMeshProUGUI doorPromptText;
    public TextMeshProUGUI leverPromptText;
    public TextMeshProUGUI ScrewDriverPromptText;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        keyPromptText.gameObject.SetActive(false);
        doorPromptText.gameObject.SetActive(false);
        leverPromptText.gameObject.SetActive(false);
        ScrewDriverPromptText.gameObject.SetActive(false);
    }

    public void ShowKeyPrompt(bool show)
    {
        keyPromptText.gameObject.SetActive(show);
    }

    public void ShowScrewdriverPrompt(bool show)
    {
        ScrewDriverPromptText.gameObject.SetActive(show);
    }

    public void ShowDoorPrompt(string text)
    {
        doorPromptText.text = text;
        doorPromptText.gameObject.SetActive(true);
    }

    public void HideDoorPrompt()
    {
        doorPromptText.gameObject.SetActive(false);
    }

    public void ShowLeverPrompt(bool show)
    {
        leverPromptText.gameObject.SetActive(show);
    }
}
