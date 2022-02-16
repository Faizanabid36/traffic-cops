using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelWon : MonoBehaviour
{
    public Text levelcompleted;
    // Start is called before the first frame update
    

      void OnTriggerEnter(Collider other) {
         if(other.gameObject.tag=="Player"){
             Debug.Log("collided");
             levelcompleted.gameObject.SetActive(true);
            GameManager.levelWon=true;
         }
         
     }
}
