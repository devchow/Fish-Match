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
        Bonus = BonusType.None;
    }
    
    // Comparing Fish
    public bool IsSameType(Fish newFish)
    {
        return string.Compare(this.Type, newFish.Type) == 0;
    }
    
    // custom constructor
    public void Initialize(string type, int row, int column)
    {
        Column = column;
        Row = row;
        Type = type;
    }
    
    // Swapping Rows & Columns
    public static void SwapRowColumn(Fish f1, Fish f2)
    {
        int temp = f1.Row;
        f1.Row = f2.Row;
        f2.Row = temp;

        temp = f1.Column;
        f1.Column = f2.Column;
        f2.Column = temp;
    }
}
