using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrafterItem : MonoBehaviour
{
    public string ID;
    public bool outPutItem = false;
    public GameObject toInstantiate;
    [Tooltip("This is an offset to make rotated objects appear correctly")]
    public Quaternion rotOffset;
}
