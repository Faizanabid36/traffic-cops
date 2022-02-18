using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public static bool gameOver = false;

    public static bool nextLevel = false,levelWon = false;

    public Text gameOverText;

    private Scene scene;

    private void Update()
    {
        if (gameOver) gameOverText.gameObject.SetActive(true);
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

    //  void Levelwon(){
    //     if(levelWon){
    //         levelWon=false;
    //     }
    //  }
    void NextLevel()
    {
        nextLevel = false;
        scene = SceneManager.GetActiveScene();
        if ((scene.buildIndex <= SceneManager.sceneCountInBuildSettings))
        {
            Debug.Log("counttt " + SceneManager.sceneCountInBuildSettings);
            SceneManager
                .LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }
}
