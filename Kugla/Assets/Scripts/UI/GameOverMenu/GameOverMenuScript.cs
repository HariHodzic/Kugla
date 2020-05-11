using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenuScript : MonoBehaviour
{
    void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
    }

    void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
