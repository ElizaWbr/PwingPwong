using UnityEngine;
using TMPro;

public class SaveSettingsButton : MonoBehaviour
{
    public TMP_Text playerOneSelectedLabel;
    public TMP_InputField playerOneInput;

    public TMP_Text playerTwoSelectedLabel;
    public TMP_InputField playerTwoInput;

    private string _defaultPlayerOne;
    private string _defaultPlayerTwo;

    private void Start()
    {
        _defaultPlayerOne = playerOneSelectedLabel.text;
        _defaultPlayerTwo = playerTwoSelectedLabel.text;

        string playerOneName = SaveController.Instance.GetPlayerName(true);
        if (playerOneName != string.Empty)
        { 
            playerOneSelectedLabel.text = playerOneName;
            playerOneInput.text = playerOneName;
        }
        else
        {
            ClearPlayerOne();
        }

        string playerTwoName = SaveController.Instance.GetPlayerName(false);
        if (playerTwoName != string.Empty)
        {
            playerTwoSelectedLabel.text = playerTwoName;
            playerTwoInput.text = playerTwoName;
        }
        else
        {
            ClearPlayerTwo();
        }
    }
    public void OnButtonClick()
    {
        string playerOneName = playerOneInput.text;
        playerOneSelectedLabel.text = playerOneName;
        SaveController.Instance.SavePlayerName(true, playerOneName);

        string playerTwoName = playerTwoInput.text;
        playerTwoSelectedLabel.text = playerTwoName;
        SaveController.Instance.SavePlayerName(false, playerTwoName);
    }

    private void ClearPlayerOne()
    {
        playerOneSelectedLabel.color = Color.white;
        playerOneSelectedLabel.text = _defaultPlayerOne;
        playerOneInput.text = "";
    }
    private void ClearPlayerTwo()
    {
        playerTwoSelectedLabel.color = Color.white;
        playerTwoSelectedLabel.text = _defaultPlayerTwo;
        playerTwoInput.text = "";
    }

    public void ClearData()
    {
        ClearPlayerOne();
        ClearPlayerTwo();
    }
}
