using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour
{
    [Header("Key Configuration")]
    [SerializeField] KeyCode pickupKey;

    [Header("Settings")]
    [SerializeField] LayerMask pickupLayermask;
    [SerializeField] float pickUpDistance;

    [Header("References")]
    [SerializeField] Transform objectGrabPointTransform;
    ObjectGrabbable currentObjectGrabbable;
    private void Update()
    {
        if(currentObjectGrabbable == null)
        {
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit objHit, pickUpDistance, pickupLayermask))
            {
                if (objHit.collider.TryGetComponent<ObjectGrabbable>(out ObjectGrabbable selectedObject))
                {
                    selectedObject.SelectForThisFrame();
                }
            }
        }
        if (Input.GetKeyDown(pickupKey))
        {
            // check if the player is holding something
            if(currentObjectGrabbable == null)
            {
                // if not grab the facing object
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,out RaycastHit hit,pickUpDistance,pickupLayermask))
                {
                    if (hit.collider.TryGetComponent<ObjectGrabbable>(out currentObjectGrabbable))
                    {
                        currentObjectGrabbable.Grab(objectGrabPointTransform);
                    }
                }
            }
            else
            {
                // if yes, drop the object
                currentObjectGrabbable.Drop();
                currentObjectGrabbable = null;
            }
        }
    }
}
