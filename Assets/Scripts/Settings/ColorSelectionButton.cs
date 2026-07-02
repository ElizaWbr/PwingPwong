using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorSelectionButton : MonoBehaviour
{
    public Button uiButton;
    public TMP_Text colorLabel;
    public bool isPlayerOne = false;
    private void Start()
    {
        Color color = SaveController.Instance.GetPlayerColor(isPlayerOne);
        colorLabel.color = color;
    }
    public void OnButtonClick()
    {
        Color color = uiButton.colors.normalColor;
        colorLabel.color = color;
        SaveController.Instance.SavePlayerColor(isPlayerOne, color);
    }
}
