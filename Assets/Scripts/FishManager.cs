using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class FishManager : MonoBehaviour
{
    [Header("Score Text")]
    public Text scoreText;
    private int _score;

    public FishArray fishes;

    private Vector2 BottoomRight = new Vector2(-2.37f, -4.27f);
    private Vector2 FishSize = new Vector2(0.7f, 0.7f);

    private GameState state = GameState.None;
    private GameObject hitGo = null;

    private Vector2[] SpwanPos;

    private GameObject[] FishPrefabs;
    private GameObject[] ExplosionPrefabs; /////////////// - To be Renamed to Clear Prefabs
    private GameObject[] BonusPrefabs;

    private IEnumerator CheckPotentialMatchesCoroutine;
    private IEnumerator AnimatePotentialMatchesCoroutine;

    private IEnumerable<GameObject> potentialMatches;

    public SoundManager soundManager;
    
    // Update

    private void InitializeVariables()
    {
        _score = 0;
        ShowScore();
    }

    private void ShowScore()
    {
        scoreText.text = "Score " + _score.ToString();
    }

    private void IncreaseScore(int amount)
    {
        _score += amount;
        ShowScore();
    }

    private void DestroyAllFish()
    {
        for (int row = 0; row < GameVariables.Rows; row++)
        {
            for (int column = 0; column < GameVariables.Columns; column++)
            {
                Destroy(fishes[row, column]);
            }
        }
    }

    private void InitializeTypesOnPrefabShapesAndBonuses()
    {
        foreach (var item in FishPrefabs)
        {
            item.GetComponent<Fish>().Type = item.name;
        }

        for (int i = 0; i <BonusPrefabs.Length; i++)
        {
            BonusPrefabs[i].GetComponent<Fish>().Type = FishPrefabs[i].name;
        }
    }

    private void InstantiateAndPlaceNewFish(int row, int column, GameObject newFish)
    {
        GameObject go = Instantiate(newFish, BottoomRight + new Vector2(column * FishSize.x, row * FishSize.y), Quaternion.identity) as GameObject;
        
        go.GetComponent<Fish>().Initialize(newFish.GetComponent<Fish>().Type, row, column);

        fishes[row, column] = go;
    }
}


















