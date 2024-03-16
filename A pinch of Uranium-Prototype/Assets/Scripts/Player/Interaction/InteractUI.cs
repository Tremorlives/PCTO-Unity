using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractUI : MonoBehaviour
{
    [SerializeField] GameObject container;
    [SerializeField] TextMeshProUGUI promptText;

    public void Show(IInteractable interactable)
    {
        container.SetActive(true);
        promptText.text = interactable.interactionPrompt;
    }

    public void Hide()
    {
        container.SetActive(false);
        promptText.text = "";
    }
}
