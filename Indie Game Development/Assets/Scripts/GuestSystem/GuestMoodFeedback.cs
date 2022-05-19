using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuestMoodFeedback : MonoBehaviour
{
    [SerializeField] private GuestMoodBehaviour guestMoodBehaviour;

    [SerializeField] private MoodSettings _moodSettings;
    [SerializeField] private Image _moodIcon;
    
    [SerializeField] private Image _timerFill;

    [SerializeField] private Color _normalColor;
    [SerializeField] private Color _angryColor;
    
    void Awake()
    {
        _timerFill.color = _normalColor;
        guestMoodBehaviour.OnMoodChanged += ChangeMoodTimer;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCurrentFill();
    }

    void ChangeMoodTimer(Mood mood)
    {
        //_moodIcon.sprite = _moodSettings.GetMoodSprite(mood);

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

    void UpdateCurrentFill()
    {
        _timerFill.fillAmount = guestMoodBehaviour.GetMoodTimeNormalized();
    }
}
