using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class Trashcan : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] List<string> validIDs;
    [SerializeField] int itemsNeeded;
    [SerializeField] TextMeshProUGUI displayText;

    [Header("On Completed")]
    [SerializeField] bool dropsItem;
    [SerializeField] GameObject completeVFX;
    [SerializeField] GameObject itemDrop;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Quaternion spawnRotation;
    [SerializeField] UnityEvent OnComplete;
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
        if(itemsNeeded > validItemList.Count)
        {
            displayText.text = validItemList.Count + " / " + itemsNeeded;
        }
        else
        {
            CompleteTrashCan();
        }
    }

    private void CompleteTrashCan()
    {
        Instantiate(completeVFX, transform.position, Quaternion.identity);
        foreach(var item in validItemList)
        {
            Destroy(item.gameObject);
        }
        if (dropsItem)
        {
            Instantiate(itemDrop, spawnPoint.transform.position, spawnRotation);
        }
        OnComplete.Invoke();
        Destroy(this.gameObject);
    }
}
