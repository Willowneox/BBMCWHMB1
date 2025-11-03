using System;
using UnityEngine;

/// <summary>
/// An enum representing the different possible judgements.
/// </summary>
public enum Judgement
{
    Miss,
    Good,
    Perfect
}
    
class Judgements
{
    // Timings were decided by Kai.
    /// Time required for a good in milliseconds
    public const int goodTime = 80;
    /// Time required for a perfect in milliseconds
    public const int perfectTime = 40;

    /// <summary>
    /// Gets the rating for a hit time.
    /// The hit time must be in milliseconds.
    /// </summary>
    /// <param name="hitTime"></param>
    /// <returns></returns>
    public static Judgement GetRating(double hitTime)
    {
        // If hit time is within perfect range
        if (hitTime > -perfectTime && hitTime < perfectTime)
        {
            Debug.Log("Perfect");
            return Judgement.Perfect;
        }
        // If hit time is within good range
        if (hitTime > -goodTime && hitTime < goodTime)
        {
            Debug.Log("Good");
            return Judgement.Good;
        }
        Debug.Log("Miss");
        return Judgement.Miss;
    }
}