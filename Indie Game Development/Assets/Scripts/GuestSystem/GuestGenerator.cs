using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct WeightedMood
{
    public Mood moodToSet;
    public float weight;
}


public class GuestGenerator : MonoBehaviour
{
    [SerializeField] private List<WeightedMood> weightedMoods;

    private Mood GetRandomStartingMood()
    {
        Mood outputMood = Mood.Normal;

        float totalMoodWeight = 0f;
        foreach (WeightedMood mood in weightedMoods)
        {
            totalMoodWeight += mood.weight;
        }

        float randomWeightValue = Random.Range(1, totalMoodWeight + 1);

        foreach (WeightedMood mood in weightedMoods)
        {
            if (randomWeightValue <= mood.weight)
            {
                outputMood = mood.moodToSet;
                break;
            }
        }

        return outputMood;
    }

    private void SpawnGuest()
    {
        GameObject guestObject = new GameObject("Guest");
        guestObject.transform.parent = transform;

        Guest guest = guestObject.AddComponent<Guest>();
        guest.SetUpGuest(GetRandomStartingMood());

    }
}
