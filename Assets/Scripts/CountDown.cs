using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class CountDown : MonoBehaviour
{

    public GameObject textDisplay;
    public Text timeUp;
    public int secsLeft = 20;
    public bool didTimeMinus = false;
    void Start()
    {
        Debug.Log("HERE");
        textDisplay.GetComponent<Text>().text = "00: " + secsLeft;
    }

    // Update is called once per frame
    void Update()
    {
        if (didTimeMinus == false && secsLeft > 0)
        {
            StartCoroutine(CountDowns());
        }

    }

    IEnumerator CountDowns()
    {
        didTimeMinus = true;
        yield return new WaitForSeconds(1);
        secsLeft -= 1;

        if (secsLeft < 10)
        {
            textDisplay.GetComponent<Text>().text = "00: 0" + secsLeft;
        }
        else
        {
            textDisplay.GetComponent<Text>().text = "00: " + secsLeft;
        }
        if (secsLeft == 0)
        {
            timeUp.gameObject.SetActive(true);
            GameManager.gameOver = true;
        }
        didTimeMinus = false;
    }
}
