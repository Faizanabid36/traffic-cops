using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelWon : MonoBehaviour
{
    public Text levelcompleted;
    public Button nextLevel;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIManager.Instance.LevelCompleted();
            StartCoroutine(StopForSecsCoroutine());
        }

    }
    public void NextLevel() => GameManager.nextLevel = true;


    IEnumerator StopForSecsCoroutine()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
