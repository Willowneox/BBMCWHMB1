using System;
using System.Collections.Generic;
using Unity.VisualScripting;
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

/// <summary>
/// The rank you get at the end of the level.
/// </summary>
public enum Rank
{
    A,
    B,
    C,
    D,
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
    /// Calculates the score with the given tally.
    /// </summary>
    /// <param name="tally"></param>
    /// <returns></returns>
    public static Rank CalculateScore(Dictionary<Judgement, int> tally)
    {
        int misses = tally[Judgement.Miss];
        int goods = tally[Judgement.Good];
        int perfects = tally[Judgement.Perfect];

        double score = (-0.05 * misses + 0.75 * goods + 1 * perfects) / (misses + goods + perfects);
        Debug.Log("Score for this level: " + score);
        if (score > 0.9)
        {
            return Rank.A;
        }
        if (score > 0.8)
        {
            return Rank.B;
        }
        if (score > 0.75)
        {
            return Rank.C;
        }
        return Rank.D;
    }
}