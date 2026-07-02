using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ScoresSaveController : MonoBehaviour
{
    [Header("Ranking Players")]
    public string rankingPlayersKey = "ranking_players";
    public string newRankingPlayersKey = "new_rank_players";

    [Header("Ranking Coop")]
    public string rankingCoopKey = "ranking_coop";
    public string newRankingCoopKey = "new_rank_coop";

    private List<ScoreEntry> _scorePlayersList;
    private List<ScoreEntry> _scoreCoopList;

    public static ScoresSaveController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        _scorePlayersList = GetScoresPlayers();
        _scoreCoopList = GetScoresCoop();
    }

    /* SAVE */

    /* SAVE in ranking */

    /* PLAYERS */
    public void SaveInRankingPlayers(int newScorePlayerOne, int newScorePlayerTwo)
    {
        string playerOne = SaveController.Instance.GetPlayerName(true, "Jogador Um");
        string playerTwo = SaveController.Instance.GetPlayerName(false, "Jogador Dois");

        List<ScoreEntry> scores = _scorePlayersList;

        scores = AddPlayerToRanking(playerOne, newScorePlayerOne, scores);
        scores = AddPlayerToRanking(playerTwo, newScorePlayerTwo, scores);

        SaveList(rankingPlayersKey, scores);
    }

    private List<ScoreEntry> AddPlayerToRanking(string playerName, int newScore, List<ScoreEntry> scoresList) 
    {
        if (newScore <= 0)
        {
            return scoresList;
        }

        int position = scoresList.FindIndex(x => x.Name == playerName);

        if (position >= 0)
        {
            ScoreEntry playerScore = scoresList.Find(x => x.Name == playerName);
            if (playerScore.Score < newScore)
            {
                playerScore.Score = newScore;
            }
        }
        else
        {
            scoresList.Add(new ScoreEntry { Name = playerName, Score = newScore });
        }
        return scoresList;
    }

    /* COOP */
    public void SaveInRankingCoop(int newScore)
    {
        ClearNewInRanking(rankingCoopKey);

        /* Pega o nome da dupla */

        string playerOne = SaveController.Instance.GetPlayerName(true, "Jogador Um");
        string playerTwo = SaveController.Instance.GetPlayerName(false, "Jogador Dois");
        string newName = $"{playerOne} e {playerTwo}";

        /* Se o score não foi suficiente: */
        if (newScore <= 0)
        {
            SaveNewInRanking(newRankingCoopKey, newScore, 0, newName);
            return;
        }

        _scoreCoopList = _scoreCoopList.OrderByDescending(x => x.Score).ToList();

        /* Se já temos 5 scores e o novo não supera o pior deles, não salva */
        if (_scoreCoopList.Count >= 5)
        {
            int lowestScore = _scoreCoopList.Last().Score;
            if (newScore < lowestScore)
            {
                SaveNewInRanking(newRankingCoopKey, newScore, 0, newName);
            }
        }

        _scoreCoopList.Add(new ScoreEntry { Name = newName, Score = newScore });
        _scoreCoopList = _scoreCoopList.OrderByDescending(x => x.Score).Take(5).ToList();

        int position = _scoreCoopList.FindIndex(x => x.Name == newName && x.Score == newScore);
        if (position >= 0)
        {
            SaveNewInRanking(newRankingCoopKey, newScore, position + 1, newName);
        }

        SaveList(rankingCoopKey, _scoreCoopList);
    }

    /* SAVE new in ranking */

    private void SaveNewInRanking(string key, int score, int position, string name)
    {
        if (position > 0)
        {
            PlayerPrefs.SetString(key, "Novo recorde! " + name + " estão em " + position.ToString() + "° lugar com " + score.ToString() + " pontos!");
        }
        else
        {
            PlayerPrefs.SetString(key, "A pontuação " + score.ToString() + " não foi alta o suficiente para entrar no ranking :(");
        }
    }

    /* GET */

    /* GET scores */
    public List<ScoreEntry> GetScoresPlayers()
    {
        return GetScores(rankingPlayersKey);
    }
    public List<ScoreEntry> GetScoresCoop()
    {
        return GetScores(rankingCoopKey);
    }
    private List<ScoreEntry> GetScores(string key)
    {
        string json = PlayerPrefs.GetString(key, "");

        if (string.IsNullOrEmpty(json))
        {
            return new List<ScoreEntry>();
        }

        List<ScoreEntry> scores = JsonUtility.FromJson<ScoreList>(json).Scores;
        return scores.OrderByDescending(x => x.Score).ToList();
    }

    /* GET new in ranking */
    public string GetNewInRanking()
    {
        return GetPrefs(newRankingPlayersKey);
    }
    public string GetNewInRankingCoop()
    {
        return GetPrefs(newRankingCoopKey);
    }

    /* CLEAR new in ranking */
    public void ClearNewInRankingPlayer()
    {
        ClearNewInRanking(newRankingPlayersKey);
    }
    public void ClearNewInRankingCoop()
    {
        ClearNewInRanking(newRankingCoopKey);
    }
    private void ClearNewInRanking(string key)
    {
        SetPrefs(key, "");
    }

    /* GET SET PREFS */
    private void SaveList(string key, List<ScoreEntry> scores)
    {
        ScoreList scoreList = new ScoreList { Scores = scores };
        string json = JsonUtility.ToJson(scoreList);

        SetPrefs(key, json);
    }
    private void SetPrefs(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
    }
    private string GetPrefs(string key)
    {
        return PlayerPrefs.GetString(key, "");
    }
}
