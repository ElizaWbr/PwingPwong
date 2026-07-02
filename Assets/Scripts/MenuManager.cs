using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Menu")]
    public GameObject menuScene;
    public GameObject lastWinnerContainer;
    public TMP_Text lastWinnerText;

    [Header("GameMode")]
    public GameObject gameModeScene;

    [Header("Settings")]
    public SaveSettingsButton saveSettings;
    public GameObject settingsScene;
    public TMP_InputField playerOneName;
    public TMP_InputField playerTwoName;

    [Header("Ranking")]
    public RankingBuilder rankingBuilderPlayers;
    public RankingBuilder rankingBuilderCoop;
    public GameObject rankingSelectorScene;
    public GameObject rankingPlayersScene;
    public GameObject rankingCoopScene;
    public Dictionary<int, string> dictionaryRanking;

    void Start()
    {
        GoToMenu();
    }
    public void GoToMenu()
    {
        disableAll();
        menuScene.SetActive(true);
    }
    public void GoToGameMode()
    {
        disableAll();
        gameModeScene.SetActive(true);
    }
    public void GoToSettings()
    {
        disableAll();
        settingsScene.SetActive(true);
    }
    public void GoToRankingSelector()
    {
        disableAll();
        rankingSelectorScene.SetActive(true);
    }
    public void GoToRankingPlayers()
    {
        disableAll();
        rankingPlayersScene.SetActive(true);
    }
    public void GoToRankingCoop()
    {
        disableAll();
        rankingCoopScene.SetActive(true);
    }
    private void disableAll()
    {
        menuScene.SetActive(false);
        gameModeScene.SetActive(false);
        settingsScene.SetActive(false);
        rankingSelectorScene.SetActive(false);
        rankingPlayersScene.SetActive(false);
        rankingCoopScene.SetActive(false);
    }
    public void ClearSave()
    {
        SaveController.Instance.ClearSave();

        saveSettings.ClearData();
        rankingBuilderPlayers.LoadRanking();
        rankingBuilderCoop.LoadRanking();
        ResetWinner();
    }
    public void ResetWinner()
    {
        lastWinnerContainer.SetActive(false);
        lastWinnerText.text = "";
    }
}
