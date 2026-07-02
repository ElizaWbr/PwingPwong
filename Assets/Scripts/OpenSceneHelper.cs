using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSceneHelper : MonoBehaviour
{
    public bool isGameScene = false;
    public void OpenScene(string sceneToOpen)
    {
        SceneManager.LoadScene(sceneToOpen);

        if (isGameScene == true)
        {
            SaveController.Instance.SaveGameMode(sceneToOpen);
        }
    }
}
