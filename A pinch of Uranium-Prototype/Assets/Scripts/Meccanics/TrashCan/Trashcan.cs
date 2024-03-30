using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(Collider))]
public class Trashcan : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] List<string> validIDs;
    [SerializeField] int itemsNeeded;
    [SerializeField] TextMeshProUGUI displayText;

    [Header("On Completed")]
    [SerializeField] GameObject completeVFX;
    [SerializeField] GameObject itemDrop;
    List<GameObject> validItemList;
    int currentItemAmount = 0;
    private void Awake()
    {
        validItemList = new List<GameObject>();
        currentItemAmount = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<JunkItem>(out JunkItem item))
        {
            if (validIDs.Intersect(item.itemIDs).Any())
            {
                validItemList.Add(other.gameObject);
                currentItemAmount++;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (validItemList.Contains(other.gameObject))
        {
            validItemList.Remove(other.gameObject);
            currentItemAmount--;
        }
    }
    private void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        Debug.Log(validItemList.Count);
        if(itemsNeeded > validItemList.Count)
        {
            displayText.text = validItemList.Count + " / " + itemsNeeded;
        }
        else
        {
            displayText.text = "FULL!";
            // Drop the object and summon an effect
            CompleteTrashCan();
        }
    }

    private void CompleteTrashCan()
    {
        Instantiate(completeVFX, transform.position, Quaternion.identity);
        GameObject drop = Instantiate(itemDrop, transform.position, Quaternion.identity);
        Destroy(this);
        Debug.Log("Destroyed Trash Can!");
    }
}
