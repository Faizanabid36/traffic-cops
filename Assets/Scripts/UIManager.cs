using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject timeOver;
    public GameObject gameOver;
    public GameObject playerCaught;
    public GameObject levelCompleted;

    public void TimeIsOver() => timeOver.gameObject.SetActive(true);

    public void GameIsOver() => gameOver.gameObject.SetActive(true);

    public void PlayerWasCaught() => playerCaught.gameObject.SetActive(true);

    public void LevelCompleted() => levelCompleted.gameObject.SetActive(true);

}
