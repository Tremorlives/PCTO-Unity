using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class Crafter : MonoBehaviour,IInteractable
{
    [Header("Settings")]
    [SerializeField] CrafterPOS[] positions;
    [SerializeField] CrafterPOS finalPos;
    [SerializeField] CrafterItem outputItem;
    List<GameObject> validItemList = new List<GameObject>();
    List<CrafterItem> crafterItems = new List<CrafterItem>();
    bool isFull = false;
    bool isCompleted = false;

    [Header("Interaction")]
    [SerializeField] string prompt = "Interact";
    [SerializeField] InteractUI interactUI;

    public string interactionPrompt => prompt;

    public GameObject instance => this.gameObject;

    private void Awake()
    {
        enabled = true;
        interactUI.Hide();
        isFull = false;
        isCompleted = false;
    }
    #region ItemRecognition
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<CrafterItem>(out CrafterItem item))
        {
            foreach(var pos in positions)
            {
                if (pos.ValidIDs.Contains(item.ID))
                {
                    validItemList.Add(item.gameObject);
                    crafterItems.Add(item);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (validItemList.Contains(other.gameObject))
        {
            validItemList.Remove(other.gameObject);
            crafterItems.Remove(other.GetComponent<CrafterItem>());
        }
    }
    #endregion
    #region CustomMethods
    // called when the crafter is interacted
    public void PushAll()
    {
        if(crafterItems.Count > 0)
        {
            // it's necessary to cache the list of items, as the list is being modified inside the loop
            List<CrafterItem> chachedItems = new List<CrafterItem>(crafterItems);
            foreach (var validCraftitem in chachedItems)
            {
                PushObject(validCraftitem);
            }
        }
        else
        {
            Debug.Log("Bro the crafter is empty");
        }
    }

    // tries to put the object into the crafter
    public void PushObject(CrafterItem item)
    {
        foreach (var pos in positions)
        {
            if (pos.ValidIDs.Contains(item.ID))
            {
                if (!pos.IsFull)
                {
                    // spawn the object
                    GameObject instantiated = Instantiate(item.toInstantiate, pos.transform.position, item.rotOffset);
                    // set the spawn pos
                    pos.IsFull = true;
                    pos.displayedObject = instantiated;
                    // destroy the object used for the craft
                    Destroy(item.gameObject);
                    validItemList.Remove(item.gameObject);
                    crafterItems.Remove(item);
                }
            }
        }
    }
    //checks if all the positions are full
    public void CheckPositions()
    {
        bool isFull = true;
        foreach(var pos in positions)
        {
            if (!pos.IsFull)
            {
                isFull = false;
            }
        }
        this.isFull = isFull;
    }
    // craft the final item
    private void CraftFinalItem()
    {
        foreach (var pos in positions)
        {
            Destroy(pos.displayedObject);
        }
        // spawn an effect
        Instantiate(outputItem.gameObject, finalPos.transform.position, outputItem.rotOffset);
        isCompleted = true;
    }
    #endregion
    public void Interact()
    {
        CheckPositions();
        if (isFull && !isCompleted)
        {
            CraftFinalItem();
        }
        else
        {
            PushAll();
        }
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
