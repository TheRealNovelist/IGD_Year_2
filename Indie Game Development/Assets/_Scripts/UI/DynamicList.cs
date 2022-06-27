using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Script to populate UI element dynamically, changing the container size in the process.
/// Use best with ScrollRect component
/// </summary>
public class DynamicList : MonoBehaviour
{
    [Header("Content Setup")]
    [Tooltip("The rect storing all the content")]
    [SerializeField] private GameObject contentHolder;
    [Tooltip("Prefab to add to the list")]
    [SerializeField] private GameObject contentToSpawn;

    [Space]

    [Tooltip("Default if the list is empty. Not required")]
    [SerializeField] private GameObject emptyContent;

    [Space]

    [Tooltip("The maximum amount of content in the list. Uncapped if 0")]
    [SerializeField] private int maxContentAmount = 0;

    [Space]

    [SerializeField] private List<GameObject> contentList; //The entire list of content

    private RectTransform contentHolderRect => contentHolder.GetComponent<RectTransform>(); //Rect transform to modify of the content
    private float contentHeight => contentToSpawn.GetComponent<RectTransform>().rect.height; //The height of the content prefab

    //Reset size of the rect at the start of the game
    //Also pause the editor when the component required is not filled out
    public void Start()
    {
        if (!contentHolder || !contentToSpawn)
        {
            Debug.Log("DynamicList (" + gameObject.name + "): Please populate the correct component into the fields");
            Debug.Break();
        }
        
        contentHolderRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentHeight * contentList.Count);
    }

    //Add content to the list and save a reference of the content
    public GameObject AddContentToList()
    {
        if (IsListFull())
        {
            Debug.Log("DynamicList (" + gameObject.name + "): The list is full");
            return null;
        }

        //Create the UI Element parented to the content holder, and add it to the list
        GameObject content = Instantiate(contentToSpawn, contentHolder.transform);
        contentList.Add(content);

        if (contentList.Count > 0)
        {
            ToggleEmptyContent(false);
        }

        content.name = "Content " + contentList.IndexOf(content);

        ChangeContentRectHeight(contentHeight);

        return content;
    }

    //Remove content based on what is passed in (destroy both the UI element and remove from the total list)
    public void RemoveContentFromList(GameObject contentToRemove)
    {
        //Return if the list is empty
        if (contentList.Count <= 0)
        {
            Debug.Log("DynamicList (" + gameObject.name + "): No element currently in the list");
            return;
        }

        //Return if the content passed in is not in the list
        if (!contentList.Contains(contentToRemove))
        {
            Debug.Log("DynamicList (" + gameObject.name + "): The element is not in the list");
            return;
        }

        contentList.Remove(contentToRemove);
        Destroy(contentToRemove);

        if (contentList.Count == 0)
        {
            ToggleEmptyContent(true);
        }

        ChangeContentRectHeight(-contentHeight);
    }

    public void RemoveContentFromList(int index)
    {
        //Return if the list is empty
        if (contentList.Count <= 0)
        {
            Debug.Log("DynamicList (" + gameObject.name + "): No element currently in the list");
            return;
        }

        //Return if the content passed in is not in the list
        if (index > contentList.Count - 1 || index < 0)
        {
            Debug.Log("DynamicList (" + gameObject.name + "): Index is out of bound");
            return;
        }

        GameObject objectToRemove = contentList[index];

        contentList.Remove(objectToRemove);
        Destroy(objectToRemove);

        if (contentList.Count == 0)
        {
            ToggleEmptyContent(true);
        }

        ChangeContentRectHeight(-contentHeight);
    }

    //Toggle the default empty object
    public void ToggleEmptyContent(bool state)
    {
        if (emptyContent)
        {
            emptyContent.SetActive(state);
        }
    }
    
    //Change the rect size of the content holder.
    public void ChangeContentRectHeight(float height)
    {
        contentHolderRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, contentHolderRect.rect.height + height);
    }

    //Return the fullness state of the list. If the maxContentAmount == 0, the list is uncapped and always return as not full.
    public bool IsListFull()
    {
        //Uncapped list, return as not full
        if (maxContentAmount == 0)
        {
            return false;
        }
        //If the content amount is still under the max amount, return not full

        return maxContentAmount <= contentList.Count;
    }

    //Change the max size of the list
    public void SetMaxAmount(int amount)
    {
        //Return if the passed amount is less than 0 (invalid)
        if (amount < 0)
        {
            Debug.Log("DynamicList (" + gameObject.name + "): MaxAmount invalid");
            return;
        }

        //If the amount passed in is less than current max amount (or the maxContentAmount == 0 and passed in is not 0), remove the content from max to new amount 
        if (amount < maxContentAmount || (maxContentAmount == 0 && amount != 0))
        {
            for (int i = contentList.Count - 1; i > amount; i--)
            {
                RemoveContentFromList(i);
            }
        }

        //Set new max amount
        maxContentAmount = amount; 
    }
} 
