using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
public class Trashcan : MonoBehaviour
{
    [SerializeField] List<string> validIDs;
    [SerializeField] int itemsNeeded;
    [SerializeField] TextMeshProUGUI displayText;
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
            if (validIDs.Contains(item.itemID))
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
        if(itemsNeeded >= validItemList.Count)
        {
            displayText.text = validItemList.Count + " / " + itemsNeeded;
        }
        else
        {
            displayText.text = "Trashcan is full";
        }
    }
}
