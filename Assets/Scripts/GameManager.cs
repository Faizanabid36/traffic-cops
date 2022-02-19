using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static bool gameOver = false;

    public static bool nextLevel = false, levelWon = false;

    private Scene scene;

    private void Update()
    {
        if (gameOver)
            UIManager.Instance.GameIsOver();
        if (gameOver && Input.GetButtonDown("Fire1"))
        {
            gameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (nextLevel)
        {
            NextLevel();
        }
    }

    void NextLevel()
    {
        nextLevel = false;
        scene = SceneManager.GetActiveScene();
        if ((scene.buildIndex <= SceneManager.sceneCountInBuildSettings))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
