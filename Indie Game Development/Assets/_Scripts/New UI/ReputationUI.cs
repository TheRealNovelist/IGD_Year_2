using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Handle Reputation visual and feedback
/// </summary>
public class ReputationUI : MonoBehaviour
{
    [SerializeField] [InlineEditor]
    private Reputation _reputation; //Using ScriptableObject Reputation to dictate the current data

    [SerializeField] private Slider slider; //Experience slider UI element
    [SerializeField] private TextMeshProUGUI levelText; //Level text UI element

    //Set the slider visual to the amount of experience
    public void SetBarSize()
    {
        slider.value = _reputation.GetExperienceNormalized();
        Debug.Log("Changed!");
    }

    //Set the text showing level number
    public void SetLevelNumber()
    {
        levelText.text = _reputation.GetCurrentLevel().ToString();
    }

    //When the script first enable, subscribe the appropriate function to each event of the ReputationManager
    public void OnEnable()
    {
        _reputation.OnExperienceChanged += SetBarSize;
        _reputation.OnLevelChanged += SetLevelNumber;

        //Also set the current level and experience amount
        SetBarSize();
        SetLevelNumber();
    }

    //Unsubscribe when disable the script
    public void OnDisable()
    {
        _reputation.OnExperienceChanged -= SetBarSize;
        _reputation.OnLevelChanged -= SetLevelNumber;
    }
}
