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

    private bool ContainsDestroyWholeRowColumnBonus(IEnumerable<GameObject> matches)
    {
        if (matches.Count() >= GameVariables.MinMatches) //////////////////////////// .Count | .Count()
        {
            foreach (var item in matches)
            {
                if (BonusTypeChecker.ContainsDestroyWholeRowColumn(item.GetComponent<Fish>().Bonus))
                {
                    return true;
                }
            }
        }
        
        return false;
    }

    private IEnumerable<GameObject> GetEntireRow(GameObject go)
    {
        List<GameObject> matches = new List<GameObject>();
        int row = go.GetComponent<Fish>().Row;
        for (int column = 0; column < GameVariables.Columns; column++)
        {
            matches.Add(fishes[row, column]);
        }

        return matches;
        
    } // Get-Entire-Row
    
    private IEnumerable<GameObject> GetEntireColumn(GameObject go)
    {
        List<GameObject> matches = new List<GameObject>();
        int column = go.GetComponent<Fish>().Column;
        for (int row = 0; row < GameVariables.Columns; row++)
        {
            matches.Add(fishes[row, column]);
        }

        return matches;
        
    } // Get-Entire-Column

    public void Remove(GameObject item)
    {
        fishes[item.GetComponent<Fish>().Row, item.GetComponent<Fish>().Column] = null;
    }

    public AlteredFishInfo Collapse(IEnumerable<int> columns)
    {
        AlteredFishInfo collapseInfo = new AlteredFishInfo();

        foreach (var column in columns)
        {
            for (int row = 0; row < GameVariables.Rows - 1; row++)
            {
                if (fishes[row, column] == null)
                {
                    for (int row2 = row + 1; row2 < GameVariables.Rows; row2++)
                    {
                        if (fishes[row2, column] != null)
                        {
                            fishes[row, column] = fishes[row2, column];
                            fishes[row2, column] = null;

                            if (row2 - row > collapseInfo.maxDistance)
                            {
                                collapseInfo.maxDistance = row2 - row;
                            }

                            fishes[row, column].GetComponent<Fish>().Row = row;
                            fishes[row, column].GetComponent<Fish>().Column = column;

                            collapseInfo.AddFish(fishes[row, column]);
                            break;
                        }
                    }
                }
            }
        }
        
        return collapseInfo;
    }

    public IEnumerable<FishInfo> GetEmptyItemsOnColumn(int column)
    {
        List<FishInfo> emptyItems = new List<FishInfo>();

        for (int row = 0; row < GameVariables.Rows; row++)
        {
            if (fishes[row, column] == null)
            {
                emptyItems.Add(new FishInfo(){Row = row, Column = column});
            }
        }

        return emptyItems;
    }

    public MatchesInfo GetMatches(GameObject go)
    {
        MatchesInfo matchesInfo = new MatchesInfo();

        // Horizontal Matches
        var horizontalMatches = GetMatchesHorizontally(go);
        if (ContainsDestroyWholeRowColumnBonus(horizontalMatches))
        {
            horizontalMatches = GetEntireRow(go);

            if (!BonusTypeChecker.ContainsDestroyWholeRowColumn(matchesInfo.BonusesContained))
            {
                matchesInfo.BonusesContained = BonusType.DestroyWholeRowColumn;
            }
        }
        
        matchesInfo.AddObjectRange(horizontalMatches);
        
        // vertical Matches
        var verticalMatches = GetMatchesVertically(go);
        if (ContainsDestroyWholeRowColumnBonus(verticalMatches))
        {
            verticalMatches = GetEntireColumn(go);

            if (!BonusTypeChecker.ContainsDestroyWholeRowColumn(matchesInfo.BonusesContained))
            {
                matchesInfo.BonusesContained = BonusType.DestroyWholeRowColumn;
            }
        }
        
        matchesInfo.AddObjectRange(verticalMatches);
        
        
        return matchesInfo;
    }

    public IEnumerable<GameObject> GetMatches(IEnumerable<GameObject> gos)
    {
        List<GameObject> matches = new List<GameObject>();

        foreach (var go in gos)
        {
           matches.AddRange(GetMatches(go).MatchedFish());
        }

        return matches.Distinct();
    }
    
} // FishArray






















