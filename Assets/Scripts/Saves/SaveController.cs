using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SaveController : MonoBehaviour
{
    [Header("Settings")]
    public string playerOneKey = "player_one";
    public string playerTwoKey = "player_two";

    [Header("Winner")]
    public string winnerKey = "winner";

    [Header("Game Mode")]
    public string gameModeKey = "game_mode";

    private Color defaultColor = Color.white;

    private List<ScoreEntry> _scoreList;

    public static SaveController Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /* Players Name */
    public void SavePlayerName(bool isPlayerOne, string name)
    {
        string key = isPlayerOne ? playerOneKey : playerTwoKey;
        PlayerPrefs.SetString(key + "_name", name);
    }
    public string GetPlayerName(bool isPlayerOne, string defaultValue = "")
    {
        string key = isPlayerOne ? playerOneKey : playerTwoKey;
        return PlayerPrefs.GetString(key + "_name", defaultValue);
    }

    /* Players Color */
    public void SavePlayerColor(bool isPlayerOne, Color color)
    {
        string key = isPlayerOne ? playerOneKey : playerTwoKey;
        SaveColor(key, color);
    }
    public Color GetPlayerColor(bool isPlayerOne)
    {
        string key = isPlayerOne ? playerOneKey : playerTwoKey;
        return GetColor(key);
    }

    /* Winner */
    public void SaveWinner(string winner)
    {
        PlayerPrefs.SetString(winnerKey, winner);
    }
    public string GetWinner()
    {
        return PlayerPrefs.GetString(winnerKey, "");
    }

    public void SaveWinnerColor(Color color)
    {
        SaveColor(winnerKey, color);
    }
    public Color GetWinnerColor()
    {
        return GetColor(winnerKey);
    }

    /* Game mode */
    public void SaveGameMode(string gameMode)
    {
        PlayerPrefs.SetString(gameModeKey, gameMode);
    }
    public string GetGameMode()
    {
        return PlayerPrefs.GetString(gameModeKey, "");
    }

    /* Helper to save and get colors */
    public void SaveColor(string key, Color color)
    {
        PlayerPrefs.SetFloat(key + "_color_R", color.r);
        PlayerPrefs.SetFloat(key + "_color_G", color.g);
        PlayerPrefs.SetFloat(key + "_color_B", color.b);
        PlayerPrefs.SetFloat(key + "_color_A", color.a);
    }
    public Color GetColor(string key)
    {
        float r = PlayerPrefs.GetFloat(key + "_color_R", 0);
        float g = PlayerPrefs.GetFloat(key + "_color_G", 0);
        float b = PlayerPrefs.GetFloat(key + "_color_B", 0);
        float a = PlayerPrefs.GetFloat(key + "_color_A", 0);

        if (a == 0)
        {
            return defaultColor;
        }
        return new Color(r, g, b, a);
    }

    /* Clear save */
    public void ClearSave()
    {
        PlayerPrefs.DeleteAll();
    }
}
