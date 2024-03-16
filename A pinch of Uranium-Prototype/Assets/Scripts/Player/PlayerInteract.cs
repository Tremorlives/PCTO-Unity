using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [Header("Key Configuration")]
    [SerializeField] KeyCode interactionKey;
    [Header("Settings")]
    [SerializeField] float interactRadius;
    IInteractable selectedObj;
    void Update()
    {
        // handle interactable objects
        UpdateInteractableObjects();
        if (Input.GetKeyDown(interactionKey))
        {
            if(selectedObj != null)
            {
                selectedObj.Interact();
            }
        }
    }
    
    public void UpdateInteractableObjects()
    {
        // check for all the interactable items
        List<IInteractable> interactableItems = new List<IInteractable>();
        Collider[] interactedColliders = Physics.OverlapSphere(transform.position, interactRadius);
        foreach (Collider col in interactedColliders)
        {
            if (col.TryGetComponent<IInteractable>(out IInteractable interactable))
            {
                if (interactable.enabled)
                {
                    interactableItems.Add(interactable);
                }
            }
        }
        if(interactableItems.Count > 0)
        {
            // check for the closest one
            IInteractable closestItem = null;
            foreach (IInteractable item in interactableItems)
            {
                if (closestItem == null)
                {
                    closestItem = item;
                }
                else
                {
                    // check if it is closer than the other object
                    if ((item.instance.transform.position - transform.position).sqrMagnitude < (closestItem.instance.transform.position - transform.position).sqrMagnitude)
                    {
                        closestItem = item;
                    }
                }
            }
            // select the closest interactable
            if(selectedObj != closestItem)
            {
                if(selectedObj != null)
                {
                    selectedObj.OnDeselectForInteraction();
                }

                selectedObj = closestItem;
                selectedObj.OnSelectForInteraction();
            }
        }
        else
        {
            // deselect the object if outside of radius
            if(selectedObj != null)
            {
                selectedObj.OnDeselectForInteraction();
                selectedObj = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, interactRadius);
        if(selectedObj != null)
        {
            Gizmos.DrawLine(transform.position, selectedObj.instance.transform.position);
        }
    }
}
