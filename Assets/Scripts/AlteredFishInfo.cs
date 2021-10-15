using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlteredFishInfo
{
    private List<GameObject> newFish;

    public int maxDistance
    {
        get;
        set;
    }

    public AlteredFishInfo()
    {
        newFish = new List<GameObject>();
    }

    public IEnumerable<GameObject> AlteredFish()
    {
        //get{ return newFish.Distinct(); }  ////////////////////////////////////////// ERROR------------------
        return newFish.Distinct();
    }

    // Add GameObject if its not in List
    public void AddFish(GameObject go)
    {
        if (!newFish.Contains(go))
        {
            newFish.Add(go);
        }
    }

} // Altered-Fish-Info

































