using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static bool gameOver = false;
    public Text gameOverText;

    private void Update()
    {
        if (gameOver)
            gameOverText.gameObject.SetActive(true);
        if (gameOver && Input.GetButtonDown("Fire1"))
        {
            gameOver = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    
}
