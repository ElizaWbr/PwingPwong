using TMPro;
using UnityEngine;

public class EndMenuManager : MonoBehaviour
{
    public OpenSceneHelper openSceneHelper;
    public string defaultScene = "Menu";

    [Header("Last winner")]
    public GameObject theWinner;

    void Start()
    {
        DisplayLastWinner();
    }
    private void DisplayLastWinner() 
    {
        theWinner.SetActive(true);

        string lastWinner = SaveController.Instance.GetWinner();
        string newInRanking = ScoresSaveController.Instance.GetNewInRankingCoop();

        TMP_Text theText = theWinner.GetComponent<TMP_Text>();

        if (newInRanking != string.Empty)
        {
            theText.text = newInRanking;
        }
        else if (lastWinner != string.Empty)
        {
            Color colorWinner = SaveController.Instance.GetWinnerColor();

            theText.text = "Vencedor: " + lastWinner;
            theText.color = colorWinner;
        }
    }
    public void RestartMode()
    {
        string gameModeScene = SaveController.Instance.GetGameMode();

        if (gameModeScene == string.Empty)
        {
            gameModeScene = defaultScene;
        }
        openSceneHelper.OpenScene(gameModeScene);
    }
}
