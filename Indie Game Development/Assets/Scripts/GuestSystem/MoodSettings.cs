using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Mood
{
    Angry,
    Sad,
    Normal,
    Happy,
    Satisfied
}

[CreateAssetMenu(fileName = "New Mood Settings", menuName = "Settings/Mood Settings")]
public class MoodSettings : ScriptableObject
{
    public List<MoodData> MoodList;
    
    [System.Serializable]
    public struct MoodData
    {
        public Mood mood;
        public float weight;
    
        public Sprite sprite;
    }
    
    public Mood GetRandomMood()
    {
        Mood outputMood = Mood.Normal;

        float totalMoodWeight = 0f;
        foreach (MoodData moodData in MoodList)
        {
            totalMoodWeight += moodData.weight;
        }

        float randomWeightValue = Random.Range(1, totalMoodWeight + 1);

        foreach (MoodData moodData in MoodList)
        {
            if (randomWeightValue <= moodData.weight)
            {
                outputMood = moodData.mood;
                break;
            }
        }
        return outputMood;
    }

    private MoodData GetMoodData(Mood mood)
    {
        foreach (MoodData moodData in MoodList)
        {
            if (moodData.mood == mood)
            {
                return moodData;
            }
        }
        
        Debug.LogError("Code shouldn't reach this point!");
        return MoodList[0];
    }
    
    public Sprite GetMoodSprite(Mood mood)
    {
        return GetMoodData(mood).sprite;
    }
}
