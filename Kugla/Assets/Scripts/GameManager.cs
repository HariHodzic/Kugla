using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameEnded = false;
   public void GameOver()
    {

        FindObjectOfType<Score>().GameOn = false;
        if (!gameEnded) {
            gameEnded = true;
            Debug.Log("GEJM OVER BIC");
            Invoke("Restart", 2f);
        }
    }

    private void Restart    ()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
