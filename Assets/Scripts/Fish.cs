using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public BonusType Bonus
    {
        get;
        set; 
    }

    public int Row
    {
        get;
        set;
    }

    public int Column
    {
        get;
        set;
    }

    public string Type
    {
        get;
        set;
    }

    public Fish()
    {
        
    }
}
