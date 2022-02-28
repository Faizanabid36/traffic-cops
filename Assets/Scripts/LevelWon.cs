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
            AudioManager.Instance.Stop("Day1");
            AudioManager.Instance.Stop("Traffic");
            AudioManager.Instance.Play("Win");
            AudioManager.Instance.Play("MaleCheer");
            
            GameManager.levelCompleted = true;
            UIManager.Instance.LevelCompleted();
            StartCoroutine(StopForSecsCoroutine());
        }

    }
    public void NextLevel() => GameManager.nextLevel = true;


    IEnumerator StopForSecsCoroutine()
    {
        yield return new WaitForSeconds(2);
        if(SceneManager.GetActiveScene().buildIndex==5){
            SceneManager.LoadScene(0);

        }
        else{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
