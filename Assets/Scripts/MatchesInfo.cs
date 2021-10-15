using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MatchesInfo
{
    private List<GameObject> matches;

    public BonusType BonusesContained
    {
        set;
        get;
    }

    public MatchesInfo()
    {
        matches = new List<GameObject>();
        BonusesContained = BonusType.None;
    }

    public IEnumerable<GameObject> MatchedFish()
    {
        // get
        // { return matches.Distinct();                //////////////////////////////////////////////////////////////
        // }
        
        return matches.Distinct();
        
    } //--------------------------------------

    public void AddObject(GameObject go)
    {
        if (!matches.Contains(go))
        {
            matches.Add(go);
        }
    }

    public void AddObjectRange(IEnumerable<GameObject> gos)
    {
        foreach (var item in gos)
        {
            AddObject(item);
        }
    }
    
    
} // Matches-Info
