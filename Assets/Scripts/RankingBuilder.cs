using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RankingBuilder : MonoBehaviour
{
    [Header("Ranking")]
    public GameObject listItemPrefab;
    public bool isPlayersRanking = false;

    void Start()
    {
        LoadRanking();
    }

    public void LoadRanking()
    {
        ClearItems();
        BuildRank();
    }

    private void ClearItems() 
    {
        GridLayoutGroup gridLayout = GetComponent<GridLayoutGroup>();
        foreach (Transform item in gridLayout.transform)
        {
            Destroy(item.gameObject);
        }
    }

    private void BuildRank()
    {
        List<ScoreEntry> scores = getScores();

        int count = 0;

        foreach (var scoreEntry in scores)
        {
            if (count < 5)
            {
                BuildRankItem(scoreEntry.Name, scoreEntry.Score);
                count++;
            }
        }

        for (int i = count; i < 5; i++)
        {
            BuildRankItem("(Slot vazio)", 0);
        }
    }

    private List<ScoreEntry> getScores()
    {
        if (isPlayersRanking)
        {
            return ScoresSaveController.Instance.GetScoresPlayers();
        }
        else
        {
            return ScoresSaveController.Instance.GetScoresCoop();
        }
    }

    private void BuildRankItem(string name, int score)
    {
        GameObject newItem = Instantiate(listItemPrefab, gameObject.transform);

        /* Icon */
        //newItem.transform.GetChild(0);

        /* Score */
        newItem.transform.GetChild(1).GetComponent<TMP_Text>().text = score.ToString();

        /* Name */
        newItem.transform.GetChild(2).GetComponent<TMP_Text>().text = name;
    }
}
