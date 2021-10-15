using System;
using UnityEngine;


public static class GameVariables
{
    [Header("Rows & Columns")]
    public static int Rows = 12;
    public static int Columns = 8;
    
    [Header("Animation Duration")]
    public static float AnimationDuration = 0.2f;
    public static float MoveAnimationDuration = 0.05f;
    
    public static float ExplosionAnimationDuration = 0.3f; // -- change name to --- Clear animation

    public static float WaitBeforePotentialMatchesCheck = 2f; // Time waited when checking potential matches
    public static float OpacityAnimationDelay = 0.05f;

    public static int MinMatches = 3; // Minimum Matches to Destroy
    public static int MinMatchesForBonus = 4; // Minimum matches to get Bonus
    
    // Scores
    public static int Match3Score = 100;  // Score for a correct match
    public static int SubsequelMatchScore = 1000; // score for multiple matches | ------ Change name to "Multiple Match"
}
