using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine. SceneManagement;

public class LevelWon : MonoBehaviour
{
    public Text  levelcompleted;
    public Button nextLevel;
    

    void OnTriggerEnter(Collider other) {
         if(other.gameObject.tag=="Player"){
           
             levelcompleted.gameObject.SetActive(true);
            StartCoroutine(StopForSecsCoroutine());
             
            
            // Debug.Log(("Scene Count: " + SceneManager.sceneCountInBuildSettings));
         }
         
     }
     public void NextLevel(){
         
         GameManager.nextLevel=true;

     }


     IEnumerator StopForSecsCoroutine()
    {
    
        yield return new WaitForSeconds(6);
        levelcompleted.gameObject.SetActive(false);
        nextLevel.gameObject.SetActive(true);

        
       
    }
}
