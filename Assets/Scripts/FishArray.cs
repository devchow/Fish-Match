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
    
    // Undo Swap Function
    public void UndoSwap()
    {
        Swap(backup1, backup2);
        
    } //Undo-Swap
    
    // Get Horizontal Matches
    private IEnumerable<GameObject> GetMatchesHorizontally(GameObject go)
    {
        List<GameObject> matches = new List<GameObject>();
        matches.Add(go);

        var fish = go.GetComponent<Fish>();
        
        // Search Left
        if (fish.Column != 0)
        {
            for (int column = fish.Column - 1; column >= 0; column--)
            {
                if (fishes[fish.Row, column].GetComponent<Fish>().IsSameType(fish))
                {
                    matches.Add(fishes[fish.Row, column]);
                }
                else
                {
                    break;
                }
            }
        } // S_L
        
        // Search Right
        if (fish.Column != GameVariables.Columns - 1)
        {
            for (int column = fish.Column + 1; column < GameVariables.Columns; column++)
            {
                if (fishes[fish.Row, column].GetComponent<Fish>().IsSameType(fish))
                {
                    matches.Add(fishes[fish.Row, column]);
                }
                else
                {
                    break;
                }
            }
        } // S-R

        if (matches.Count < GameVariables.MinMatches)
        {
            matches.Clear();
        }

        return matches.Distinct(); // return non-duplicate matches
        
    } // Get-Matches-Horizontally
    
    // Get Vertical Matches
    private IEnumerable<GameObject> GetMatchesVertically(GameObject go)
    {
        List<GameObject> matches = new List<GameObject>();
        matches.Add(go);

        var fish = go.GetComponent<Fish>();
        
        // Search Bottom
        if (fish.Row != 0)
        {
            for (int row = fish.Row - 1; row >= 0; row--)
            {
                if (fishes[row, fish.Column].GetComponent<Fish>().IsSameType(fish))
                {
                    matches.Add(fishes[row, fish.Column]);
                }
                else
                {
                    break;
                }
            }
        } // S_B
        
        // Search Top
        if (fish.Row != GameVariables.Rows - 1)
        {
            for (int row = fish.Column + 1; row < GameVariables.Columns; row++)
            {
                if (fishes[row, fish.Column].GetComponent<Fish>().IsSameType(fish))
                {
                    matches.Add(fishes[row, fish.Column]);
                }
                else
                {
                    break;
                }
            }
        } // S-T

        if (matches.Count < GameVariables.MinMatches)
        {
            matches.Clear();
        }

        return matches.Distinct();  // return non-duplicate matches
        
    } // Get-Matches-Vertically
    
} // FishArray
