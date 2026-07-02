using TMPro;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public bool isCoop = false;
    public OpenSceneHelper openSceneHelper;
    public string endGameScene = "End_Menu";
    public string defaultPlayerOneLabel = "Jogador Um";
    public string defaultPlayerTwoLabel = "Jogador Dois";

    [Header("Game Objects")]
    public GameObject playerOnePaddle;
    public GameObject playerTwoPaddle;
    public BallController ballController;

    [Header("UI Texts")]
    public TMP_Text textPointsCoop;
    public TMP_Text textPointsPlayerOne;
    public TMP_Text textPointsPlayerTwo;

    [Header("Initial Positions")]
    public Vector3 playerOneInitialPosition;
    public Vector3 playerTwoInitialPosition;

    [Header("Points")]
    public int coopPoints;
    public int playerOnePoints;
    public int playerTwoPoints;
    public int winningPoints = 5;
    public int diffPoints = 2;

    private bool _isPlayerOneAuto;
    private bool _isPlayerTwoAuto;

    void Start()
    {
        ResetGame();
        ApplyColorsToUi();

        _isPlayerOneAuto = playerOnePaddle.GetComponent<PlayerPaddleController>().isAuto;
        _isPlayerTwoAuto = playerTwoPaddle.GetComponent<PlayerPaddleController>().isAuto;

        //openSceneHelper.OpenScene("Menu");
    }

    public void ResetGame()
    {
        playerOnePaddle.transform.position = playerOneInitialPosition;
        playerTwoPaddle.transform.position = playerTwoInitialPosition;

        ballController.ResetBall();

        ResetScores();
    }

    private void ApplyColorsToUi()
    {
        if (isCoop == false)
        {
            Color colorPlayerOne = SaveController.Instance.GetPlayerColor(true);
            textPointsPlayerOne.color = colorPlayerOne;

            Color colorPlayerTwo = SaveController.Instance.GetPlayerColor(false);
            textPointsPlayerTwo.color = colorPlayerTwo;
        }
    }

    /* Comp */

    public void ScorePlayerOne()
    {
        if (isCoop)
        {
            EndCoop();
        }
        else
        {
            playerOnePoints++;
            textPointsPlayerOne.text = playerOnePoints.ToString();
            CheckWin();
        }
    }
    public void ScorePlayerTwo()
    {
        if (isCoop)
        {
            EndCoop();
        }
        else
        {
            playerTwoPoints++;
            textPointsPlayerTwo.text = playerTwoPoints.ToString();
            CheckWin();
        }
    }

    public void CheckWin()
    {
        if (playerOnePoints < winningPoints && playerTwoPoints < winningPoints)
        {
            return;
        }

        bool winPlayerOne = (playerOnePoints - playerTwoPoints) > diffPoints;
        bool winPlayerTwo = (playerTwoPoints - playerOnePoints) > diffPoints;

        if (winPlayerOne || winPlayerTwo)
        {
            bool winBot = winPlayerOne ? _isPlayerOneAuto : _isPlayerTwoAuto;
            string winnerName = winBot ? "Bot :3" : SaveController.Instance.GetPlayerName(winPlayerOne);

            Color winnerColor = SaveController.Instance.GetPlayerColor(winPlayerOne);

            if (winnerName == string.Empty)
            {
                winnerName = winPlayerOne ? defaultPlayerOneLabel : defaultPlayerTwoLabel;
                winnerColor = Color.white;
            }
            SaveController.Instance.SaveWinner(winnerName);
            SaveController.Instance.SaveWinnerColor(winnerColor);

            /* We dont want to save bot scores */
            int filterOne = _isPlayerOneAuto ? 0 : playerOnePoints;
            int filterTwo = _isPlayerTwoAuto ? 0 : playerTwoPoints;
            ScoresSaveController.Instance.SaveInRankingPlayers(filterOne, filterTwo);

            openSceneHelper.OpenScene(endGameScene);
        }
    }

    /* Coop */
    public void PaddlesCollided()
    {
        if (isCoop)
        {
            ScoreCoop();
        }
    }
    private void ScoreCoop()
    {
        coopPoints++;
        textPointsCoop.text = coopPoints.ToString();
    }
    private void EndCoop()
    {
        ScoresSaveController.Instance.SaveInRankingCoop(coopPoints);
        openSceneHelper.OpenScene(endGameScene);
    }

    /* Reset */
    public void ResetScores()
    {
        if (isCoop)
        {
            coopPoints = 0;
            textPointsCoop.text = coopPoints.ToString();
        }
        else
        {
            playerOnePoints = 0;
            textPointsPlayerOne.text = playerOnePoints.ToString();

            playerTwoPoints = 0;
            textPointsPlayerTwo.text = playerTwoPoints.ToString();
        }

        SaveController.Instance.SaveWinner("");
        ScoresSaveController.Instance.ClearNewInRankingCoop();
    }
}
