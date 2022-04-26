using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Handle Reputation visual and feedback
/// </summary>
public class ReputationUI : MonoBehaviour
{
    public GameManager gameManager; //Game Manager

    public Slider slider; //Experience slider UI element
    public TextMeshProUGUI levelText; //Level text UI element

    private void Awake()
    {
        //If GameManager is not assigned, assign the default manager name
        if (!gameManager)
            gameManager = GameObject.Find("_GameManager").GetComponent<GameManager>();
    }

    //Set the slider visual to the amount of experience
    public void SetBarSize()
    {
        slider.value = gameManager.ReputationManager.GetExperienceNormalized();
        Debug.Log("Changed!");
    }

    //Set the text showing level number
    public void SetLevelNumber()
    {
        levelText.text = gameManager.ReputationManager.GetCurrentLevel().ToString();
    }

    //When the script first enable, subscribe the appropriate function to each event of the ReputationManager
    public void OnEnable()
    {
        ReputationManager.OnExperienceChanged += SetBarSize;
        ReputationManager.OnLevelChanged += SetLevelNumber;

        //Also set the current level and experience amount
        SetBarSize();
        SetLevelNumber();
    }

    //Unsubscribe when disable the script
    public void OnDisable()
    {
        ReputationManager.OnExperienceChanged -= SetBarSize;
        ReputationManager.OnLevelChanged -= SetLevelNumber;
    }
}
