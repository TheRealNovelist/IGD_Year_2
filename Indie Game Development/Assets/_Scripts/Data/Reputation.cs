///Adapted from Code Monkey's Level System
///Links: https://www.youtube.com/watch?v=kKCLMvsgAR0&t=749s

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// Manager of Reputation system, control the amount of experience player accumulated and the level of the player.
/// </summary>
[CreateAssetMenu(menuName = "Player Data/Reputation", fileName = "New Reputation Asset")]
public class Reputation : ScriptableObject
{
    //Events
    public event Action OnLevelChanged;
    public event Action OnExperienceChanged;
    
    [SerializeField] 
    private int _currentLevelIndex; //The current level
    [SerializeField]
    private int _totalExperience; //The current amount of experience

    [Serializable]
    public class Level
    {
        [TableColumnWidth(20)]
        public int experienceNeeded;
        [TableColumnWidth(60)]
        public List<Unlockable> unlockables;
    }
    
    [SerializeField] [TableList(ShowIndexLabels = true)]
    private List<Level> _allLevels;
    
    //Load data from save load system. 
    public void LoadReputation(int level, int experience)
    {
        //Reset to 0 to begin initialization
        _currentLevelIndex = 0;
        _totalExperience = 0;
        
        //Set current level index
        _currentLevelIndex = level;
        
        //Set all unlockables state according to the current level
        for (int i = 0; i < _allLevels.Count; i++)
        {
            foreach (var unlockable in _allLevels[i].unlockables)
            {
                //If the loop check is smaller than the current level, unlock the element
                if (i <= _currentLevelIndex)
                {
                    unlockable.Unlock();
                }
                //Else lock it
                else
                {
                    unlockable.Lock();
                }
            }
        }
        
        //Add any redundant experience
        AddExperience(experience);
    }

    //Retrieve data for save load system
    public void RetrieveData(out int level, out int experience)
    {
        level = _currentLevelIndex;
        experience = _totalExperience;
    }

    //Add experience
    public void AddExperience(int amount)
    {
        //If the player is at max level, break out of the function
        if (IsMaxLevel())
        {
            return;
        }

        //Add the amount passed in to the total experience.
        _totalExperience += amount;

        //While the experience still larger than the current experience to next level, increase the level
        //Also invoke any method subscribing to the Level changed.
        while (_totalExperience >= GetExperienceToNextLevel(_currentLevelIndex))
        {
            _totalExperience -= GetExperienceToNextLevel(_currentLevelIndex);
            _currentLevelIndex++;
            OnLevelChanged?.Invoke();

            foreach (var unlockable in _allLevels[_currentLevelIndex].unlockables)
            {
                unlockable.Unlock();
            }
        }

        //Invoke any method subscribed to the amount of experience changed
        OnExperienceChanged?.Invoke();
    }

    public int GetCurrentLevel()
    {
        return _currentLevelIndex;
    }

    //Return a number between 0 to 1 representing the percentage of the current experience to the next level.
    public float GetExperienceNormalized()
    {
        //Return the full bar in the UI if the level is maxed
        if (IsMaxLevel())
        {
            return 1f;
        }

        return (float)_totalExperience / GetExperienceToNextLevel(_currentLevelIndex);
    }

    //Get amount of experience to next level of the level passed in
    public int GetExperienceToNextLevel(int level)
    {
        if (level + 1 < _allLevels.Count)
        {
            return _allLevels[level + 1].experienceNeeded;
        }

        Debug.Log("Invalid Level" + level + 1);
        //Extreme case fallback (code should not reach this place)
        return 100;
    }

    //Check if the player reached max level
    public bool IsMaxLevel()
    {
        return _currentLevelIndex == _allLevels.Count - 1;
    }
}

