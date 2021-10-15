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

    public static IEnumerable<GameObject> GetPotentialMatches(FishArray fishes)
    {
		List<List<GameObject>> matches = new List<List<GameObject>>();

        for (int row = 0; row < GameVariables.Rows; row++)
        {
            for (int column = 0; column < GameVariables.Columns; column++)
            {
                var matches1 = CheckHorizontal(row, column, fishes);
                var matches2 = CheckHorizontal2(row, column, fishes);
                var matches3 = CheckHorizontal3(row, column, fishes);
                var matches4 = CheckVertical(row, column, fishes);
                var matches5 = CheckVertical2(row, column, fishes);
                var matches6 = CheckVertical3(row, column, fishes);
                
                if(matches1 != null) matches.Add(matches1);
                if(matches2 != null) matches.Add(matches2);
                if(matches3 != null) matches.Add(matches3);
                if(matches4 != null) matches.Add(matches4);
                if(matches5 != null) matches.Add(matches5);
                if(matches6 != null) matches.Add(matches6);

                if (matches.Count >= 3)
                    return matches[Random.Range(0, matches.Count - 1)];

                if (row >= GameVariables.Rows / 2 && matches.Count > 0 && matches.Count <= 2)
                    return matches[Random.Range(0, matches.Count - 1)];
            }
        }

        return null;
    }
    
    // Check Horizontal Matches
    public static List<GameObject> CheckHorizontal(int row, int column, FishArray fishes)
    {
        if (column <= GameVariables.Columns - 2)
        {
            if (fishes[row, column].GetComponent<Fish>().IsSameType(fishes[row, column + 1].GetComponent<Fish>()))
            {
                if (row >= 1 && column >= 1)
                {
                    if (fishes[row, column].GetComponent<Fish>().IsSameType(fishes[row - 1, column - 1].GetComponent<Fish>()))
                    {
                        return new List<GameObject>
                        {
                            fishes[row, column],
                            fishes[row, column + 1],
                            fishes[row - 1, column - 1]
                        };
                        
                        // scenario 2
                        if (row <= GameVariables.Rows - 2 && column >= 1)
                        {
                            if (fishes[row, column].GetComponent<Fish>()
                                .IsSameType(fishes[row + 1, column - 1].GetComponent<Fish>()))
                            {
                                return new List<GameObject>
                                {
                                    fishes[row, column],
                                    fishes[row, column + 1],
                                    fishes[row + 1, column - 1]
                                };
                            }
                        }
                    }
                }
            }
        }

        return null;
    }

    public static List<GameObject> CheckHorizontal2(int row, int column, FishArray fishes)
    {
        if (column <= GameVariables.Columns - 3)
        {
            if (fishes[row, column].GetComponent<Fish>().IsSameType(fishes[row, column + 1].GetComponent<Fish>()))
            {
                if (row >= 1 && column <= GameVariables.Columns - 3)
                {
                    // scenario 1
                    if (fishes[row, column].GetComponent<Fish>()
                        .IsSameType(fishes[row - 1, column + 2].GetComponent<Fish>()))
                    {
                        return new List<GameObject>
                        {
                            fishes[row, column],
                            fishes[row, column + 1],
                            fishes[row - 1, column + 2]
                        };
                        
                        // scenario 2
                        if (row <= GameVariables.Rows - 2 && column <= GameVariables.Columns -3)
                        {
                            if (fishes[row, column].GetComponent<Fish>().IsSameType(fishes[row + 1, column + 2].GetComponent<Fish>()))
                            {
                                return new List<GameObject>
                                {
                                    fishes[row, column],
                                    fishes[row, column + 1],
                                    fishes[row + 1, column + 2]
                                };
                            }
                        }
                    }
                }
            }
        }

        return null;
    }

    public static List<GameObject> CheckHorizontal3(int row, int column, FishArray fishes)
    {
        // first scenario
        if (column <= GameVariables.Columns - 4)
        {
            if (fishes[row, column].GetComponent<Fish>().IsSameType(fishes[row, column + 1].GetComponent<Fish>()) &&
                fishes[row, column].GetComponent<Fish>().IsSameType(fishes[row, column + 3].GetComponent<Fish>()))
            {
                return new List<GameObject>
                {
                    fishes[row, column],
                    fishes[row, column + 1],
                    fishes[row, column + 3]
                };
            }
        }
        
        // second scenario
        if (column >= 2 && column <= GameVariables.Columns - 2)
        {
            if (fishes[row, column].GetComponent<Fish>().IsSameType(fishes[row, column + 1].GetComponent<Fish>()) &&
                fishes[row, column].GetComponent<Fish>().IsSameType(fishes[row, column - 2].GetComponent<Fish>()))
            {
                return new List<GameObject>
                {
                    fishes[row, column],
                    fishes[row, column + 1],
                    fishes[row, column - 2]
                };
            }
        }

        return null;
    }
    
    // Check vertical Matches
    public static List<GameObject> CheckVertical(int row, int column, FishArray fishes)
    {
        if (row <= GameVariables.Rows - 2)
        {
            if (fishes[row, column].GetComponent<Fish>().IsSameType(fishes[row + 1, column].GetComponent<Fish>()))
            {
                if (column >= 1 && row >= 1)
                {
                    if (fishes[row, column].GetComponent<Fish>().IsSameType(fishes[row - 1, column - 1].GetComponent<Fish>()))
                    {
                        return new List<GameObject>
                        {
                            fishes[row , column],
                            fishes[row + 1, column],
                            fishes[row - 1, column - 1]
                        };
                        
                        // Scenario 2
                        if (column <= GameVariables.Columns - 2 && row >= 1)
                        {
                            if (fishes[row, column].GetComponent<Fish>().IsSameType(fishes[row - 1, column + 1].GetComponent<Fish>()))
                            {
                                return new List<GameObject>
                                {
                                    fishes[row , column],
                                    fishes[row + 1, column],
                                    fishes[row - 1, column + 1]
                                }; 
                            }
                        }
                    }
                }
            }
        }
        return null;
    }
    
    public static List<GameObject> CheckVertical2(int row, int column, FishArray fishes)
    {
        if (row <= GameVariables.Rows - 3)
        {
            if (fishes[row, column].GetComponent<Fish>().IsSameType(fishes[row + 1, column].GetComponent<Fish>()))
            {
                if (column >= 1)
                { // scenario 1
                    if (fishes[row, column].GetComponent<Fish>()
                        .IsSameType(fishes[row + 2, column - 1].GetComponent<Fish>()))
                    {
                        return new List<GameObject>
                        {
                            fishes[row , column],
                            fishes[row + 1, column],
                            fishes[row + 1, column - 1]
                        };
                        
                        // Scenario 2
                        if (column <= GameVariables.Columns - 2)
                        {
                            if (fishes[row, column].GetComponent<Fish>().IsSameType(fishes[row + 2, column + 1].GetComponent<Fish>()))
                            {
                                return new List<GameObject>
                                {
                                    fishes[row , column],
                                    fishes[row + 1, column],
                                    fishes[row + 2, column + 1]
                                };
                            }
                        }
                    }
                }
            }
        }

        return null;
    }
    
    public static List<GameObject> CheckVertical3(int row, int column, FishArray fishes)
    {
        if (row <= GameVariables.Rows - 4)
        {
            if (fishes[row, column].GetComponent<Fish>().IsSameType(fishes[row + 1, column].GetComponent<Fish>()) &&
                fishes[row, column].GetComponent<Fish>().IsSameType(fishes[row + 3, column].GetComponent<Fish>()))
            {
                return new List<GameObject>
                {
                    fishes[row , column],
                    fishes[row + 1, column],
                    fishes[row + 3, column]
                };
                
                // 2nd Scenario
                if (row >= 2 && row <= GameVariables.Rows - 2)
                {
                    if (fishes[row, column].GetComponent<Fish>()
                            .IsSameType(fishes[row + 1, column].GetComponent<Fish>()) &&
                        fishes[row, column].GetComponent<Fish>()
                            .IsSameType(fishes[row - 2, column].GetComponent<Fish>()))
                    {
                        return new List<GameObject>
                        {
                            fishes[row , column],
                            fishes[row + 1, column],
                            fishes[row - 2, column]
                        };
                    }
                }
            }
        }
        
        return null;
    }
    

} // Match Checker




















