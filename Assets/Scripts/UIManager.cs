using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GameObject gameOver;
    public GameObject playerCaught;

    public void GameIsOver() => gameOver.gameObject.SetActive(true);

    public void PlayerWasCaught() => playerCaught.gameObject.SetActive(true);

}
