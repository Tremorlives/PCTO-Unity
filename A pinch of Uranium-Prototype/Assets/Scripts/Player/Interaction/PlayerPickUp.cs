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
        
        if (Input.GetKeyDown(pickupKey))
        {
            // check if the player is holding something
            if(currentObjectGrabbable == null)
            {
                // if not grab the facing object
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward,out RaycastHit hit,pickUpDistance,pickupLayermask))
                {
                    Debug.Log("Hit: " + hit.collider.name);
                    Debug.DrawRay(Camera.main.transform.position, hit.point- Camera.main.transform.position, Color.green,1f);
                    
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
