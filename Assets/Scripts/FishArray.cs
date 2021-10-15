using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FishArray
{
    
    // 2D Array of Fish
    private GameObject[,] fishes = new GameObject[GameVariables.Rows, GameVariables.Columns];

    private GameObject backup1;
    private GameObject backup2;

    public GameObject this[int row, int column]
    {
        get
        {
            try
            {
                return fishes[row, column];
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }
        }

        set
        {
            fishes[row, column] = value;
        }
    }

    // Swap Function
    public void Swap(GameObject g1, GameObject g2)
    {
        backup1 = g1;
        backup2 = g2;

        var g1Fish = g1.GetComponent<Fish>();
        var g2Fish = g2.GetComponent<Fish>();
        
        // get Rows & Columns
        int g1Row = g1Fish.Row;
        int g1Column = g1Fish.Column;
        int g2Row = g2Fish.Row;
        int g2Column = g2Fish.Column;

        var temp = fishes[g1Row, g1Column];
        fishes[g1Row, g1Column] = fishes[g2Row, g2Column];
        fishes[g2Row, g2Column] = temp;

    }
    
} // FishArray
