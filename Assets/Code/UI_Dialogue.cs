using UnityEngine;
using TMPro;

public class UI_Dialogue : MonoBehaviour
{
    public static UI_Dialogue Instance;

    public GameObject panel;
    public TextMeshProUGUI dialogueText;

    [TextArea(3, 6)]
    public string startText;

    private bool isActive = false;

    void Awake()
    {
        Instance = this;
        panel.SetActive(false);
    }

    void Start()
    {
        if (!string.IsNullOrEmpty(startText))
        {
            ShowText(startText);
        }
    }

    void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.E))
        {
            HideText();
        }
    }

    public void ShowText(string text)
    {
        panel.SetActive(true);
        dialogueText.text = text;
        isActive = true;
    }

    public void HideText()
    {
        panel.SetActive(false);
        isActive = false;
    }

    public bool IsDialogueActive()
    {
        return isActive;
    }
}



