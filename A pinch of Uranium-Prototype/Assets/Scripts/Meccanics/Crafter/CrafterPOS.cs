using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrafterPOS : MonoBehaviour
{
    public List<string> ValidIDs;
    public bool IsFull = false;
    [Tooltip("this is only for reference purposes, leave empty in the editor"), HideInInspector]
    public GameObject displayedObject;
}
