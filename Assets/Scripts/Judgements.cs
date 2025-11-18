using System;
using System.Collections.Generic;
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
    
public class Judgements
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
            return Judgement.Perfect;
        }
        // If hit time is within good range
        if (hitTime > -goodTime && hitTime < goodTime)
        {
            return Judgement.Good;
        }
        return Judgement.Miss;
    }
    
    /// <summary>
    /// Calculates the score with the given tally
    /// </summary>
    /// <param name="tally"></param>
    /// <returns></returns>
    public static double CalculateScore(Dictionary<Judgement, int> tally)
    {
        return (-0.05 * tally[Judgement.Miss] + 0.75 * tally[Judgement.Good] + 1 * tally[Judgement.Perfect]) / (tally[Judgement.Miss] + tally[Judgement.Good] + tally[Judgement.Perfect]);
    }
}