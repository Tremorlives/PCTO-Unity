using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ObjectGrabbable : MonoBehaviour
{
    [SerializeField] float lerpPositionValue = 10f;
    [SerializeField] bool setKinematicOnGrab = true;
    Rigidbody m_rb;
    Transform currentGrabPoint;
    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_rb.isKinematic = false;
        m_rb.interpolation = RigidbodyInterpolation.Interpolate;
    }
    public void Grab(Transform objectGrabPointTransform)
    {
        currentGrabPoint = objectGrabPointTransform;
        m_rb.useGravity = false;
        if (setKinematicOnGrab) m_rb.isKinematic = true;
    }
    public void Drop()
    {
        m_rb.velocity = Vector3.zero;
        currentGrabPoint = null;
        m_rb.useGravity = true;
        if (setKinematicOnGrab) m_rb.isKinematic = false;
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
