using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
    public bool enabled { get; set; }

    public string interactionPrompt { get;}

    public GameObject instance { get; }

    public void Interact();

    public void OnSelectForInteraction();

    public void OnDeselectForInteraction();

}
