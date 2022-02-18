using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine. SceneManagement;

public class LevelWon : MonoBehaviour
{
    public Text levelcompleted;
    

    void OnTriggerEnter(Collider other) {
         if(other.gameObject.tag=="Player"){
           
             levelcompleted.gameObject.SetActive(true);
            GameManager.nextLevel=true;
            // Debug.Log(("Scene Count: " + SceneManager.sceneCountInBuildSettings));
         }
         
     }
}
