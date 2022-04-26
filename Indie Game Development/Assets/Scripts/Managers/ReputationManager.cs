///Adapted from Code Monkey's Level System
///Links: https://www.youtube.com/watch?v=kKCLMvsgAR0&t=749s

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Manager of Reputation system, control the amount of experience player accumulated and the level of the player.
/// </summary>
public class ReputationManager
{
    //Events
    public static event Action OnLevelChanged;
    public static event Action OnExperienceChanged;

    //Array contained all hardcoded required experience within the game
    //Will replace with backend and networking in the future
    private static readonly int[] experienceToNextLevel = new[] { 0, 100, 300, 500, 700, 1000};

    private int level; //The current level
    private int experience; //The current amount of experience

    //Constructor to construct the ReputationManager, setting default values
    public ReputationManager(int level, int experience)
    {
        this.level = level;
        this.experience = experience;
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
        experience += amount;

        //While the experience still larger than the current experience to next level, increase the level
        //Also invoke any method subscribing to the Level changed.
        while (experience >= GetExperienceToNextLevel(level))
        {
            experience -= GetExperienceToNextLevel(level);
            level++;
            OnLevelChanged?.Invoke();
        }

        //Invoke any method subscribed to the amount of experience changed
        OnExperienceChanged?.Invoke();
    }

    public int GetCurrentLevel()
    {
        return level;
    }

    //Return a number between 0 to 1 representing the percentage of the curent experience to the next level.
    public float GetExperienceNormalized()
    {
        //Return the full bar in the UI if the level is maxed
        if (IsMaxLevel())
        {
            return 1f;
        }
        else
        {
            return (float)experience / GetExperienceToNextLevel(level);
        }
    }

    //Get amount of experience to next level of the level passed in
    public int GetExperienceToNextLevel(int level)
    {
        if (level < experienceToNextLevel.Length)
        {
            return experienceToNextLevel[level];
        }
        else
        {
            Debug.Log("Invalid Level" + level);
            //Extreme case fallback (code should not reach this place)
            return 100;
        }
    }

    //Overload of the IsMaxLevel(int level)
    public bool IsMaxLevel()
    {
        return IsMaxLevel(level);
    }

    public bool IsMaxLevel(int level)
    {
        //Array start at 0, the length of the array - 1 is the maximum level
        return level == experienceToNextLevel.Length - 1;
    }
}

