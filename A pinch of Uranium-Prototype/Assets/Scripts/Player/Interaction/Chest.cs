using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField] string prompt = "Interact";
    [SerializeField] InteractUI interactUI;

    public string interactionPrompt => prompt;

    public GameObject instance => this.gameObject;

    private void Awake()
    {
        enabled = true;
        interactUI.Hide();
    }

    // interaction window
    public void Interact()
    {
        Debug.Log("Damn you interacted with : " + this.name + " !");
    }

    // if selected, show the ui
    public void OnSelectForInteraction()
    {
        interactUI.Show(this);
    }

    // if deselected, hide the ui
    public void OnDeselectForInteraction()
    {
        interactUI.Hide();
    }
}
