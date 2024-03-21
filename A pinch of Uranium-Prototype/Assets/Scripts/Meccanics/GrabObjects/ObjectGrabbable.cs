using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Outline))]
public class ObjectGrabbable : MonoBehaviour
{
    [SerializeField] float lerpPositionValue = 10f;
    [SerializeField] bool setKinematicOnGrab = true;
    Outline outline;
    Rigidbody m_rb;
    Transform currentGrabPoint;
    bool isSelected, toBeDeselected, isBeingHeld;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.isKinematic = false;
        m_rb.interpolation = RigidbodyInterpolation.Interpolate;
        outline = GetComponent<Outline>();
        bool isSelected = false;
        bool toBeDeselected = true;
    }
    public void Grab(Transform objectGrabPointTransform)
    {
        currentGrabPoint = objectGrabPointTransform;
        m_rb.useGravity = false;
        isBeingHeld = true;
        if (setKinematicOnGrab) m_rb.isKinematic = true;
    }
    public void Drop()
    {
        m_rb.velocity = Vector3.zero;
        currentGrabPoint = null;
        m_rb.useGravity = true;
        isBeingHeld = false;
        if (setKinematicOnGrab) m_rb.isKinematic = false;
    }
    private void Update()
    {
        if(isSelected && !toBeDeselected || isBeingHeld)
        {
            outline.enabled = true;
            toBeDeselected = true;
            isSelected = true;
        }else if(isSelected && toBeDeselected && !isBeingHeld)
        {
            outline.enabled = false;
            isSelected = false;
            toBeDeselected = false;
        }
        else
        {
            isSelected = false;
            outline.enabled = false;
        }
    }
    public void SelectForThisFrame()
    {
        isSelected = true;
        toBeDeselected = false;
    }
    private void FixedUpdate()
    {
        if(currentGrabPoint != null)
        {
            Vector3 lerpedPosition = Vector3.Lerp(transform.position, currentGrabPoint.transform.position, Time.deltaTime * lerpPositionValue);
            m_rb.MovePosition(lerpedPosition);
        }
    }
}
