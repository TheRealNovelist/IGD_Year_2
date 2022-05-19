using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuestMoodFeedback : MonoBehaviour
{
    [SerializeField] private MoodSettings _moodSettings;
    
    [Header("Textures")]
    [SerializeField] private Image _moodIcon;

    [SerializeField] private TextMeshProUGUI _moodText;
    [SerializeField] private Image _timerFill;

    [Header("Color Settings")]
    [SerializeField] private Color _normalColor = Color.green;
    [SerializeField] private Color _angryColor = Color.red;
    
    void Awake()
    {
        _timerFill.color = _normalColor;
    }
    
    public void ChangeMoodFeedback(Mood mood)
    {
        //_moodIcon.sprite = _moodSettings.GetMoodSprite(mood);

        _moodText.text = mood.ToString();
        
        switch (mood)
        {
            case Mood.Angry:
                _timerFill.color = _angryColor;
                break;
            default:
                _timerFill.color = _normalColor;
                break;
        }
    }

    public void UpdateCurrentFill(float amount)
    {
        _timerFill.fillAmount = amount;
    }
}
