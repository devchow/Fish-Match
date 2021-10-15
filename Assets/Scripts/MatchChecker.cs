using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchChecker
{
    public static IEnumerator AnimatePotentialMatches(IEnumerable<GameObject> potentialMatches)
    {
        for (float i = 1.0f; i >= 0.3f; i -= 0.1f)
        {
            foreach (var item in potentialMatches)
            {
                Color c = item.GetComponent<SpriteRenderer>().color;
                c.a = i; // Setting Color Aplha
            }

            yield return new WaitForSeconds(GameVariables.OpacityAnimationDelay);
        }
        
        for (float i = 0.3f; i <= 1.0f; i += 0.1f)
        {
            foreach (var item in potentialMatches)
            {
                Color c = item.GetComponent<SpriteRenderer>().color;
                c.a = i; // Setting Color Aplha
            }
            
            yield return new WaitForSeconds(GameVariables.OpacityAnimationDelay);
        }
    } // PotentialAnimation
    
    // Checking if Fish are Horizontal or Vertical Neighbours
    public static bool AreHorizontalVerticalNeighbours(Fish f1, Fish f2)
    {
        return (f1.Column == f2.Column || f1.Row == f2.Row) && Mathf.Abs(f1.Column - f2.Column) <= 1 &&
               Mathf.Abs(f1.Row - f2.Row) <= 1;
    }
    
} // Match Checker
