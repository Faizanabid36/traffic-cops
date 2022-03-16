using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin : MonoBehaviour
{
    // Start is called before the first frame update
   private void OnTriggerEnter(Collider other) {
       if(other.tag=="Player"){
        //    Debug.Log("coin collected ");
           gameObject.SetActive(false);
           if(gameObject.tag=="5rupees"){
              
               AudioManager.Instance.Play("5rupees");
           }
            if(gameObject.tag=="10rupees"){
               
               AudioManager.Instance.Play("10rupees");
           }
           if(gameObject.tag=="50rupees"){
               
               AudioManager.Instance.Play("50rupees");
           }
       }
   }
}
