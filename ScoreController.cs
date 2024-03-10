using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreController : MonoBehaviour
{

    public Text scoreTxt;
    private int score = 0;
    public GameObject plus50;
    public GameObject plus100;
    public GameObject plus150;
    public GameObject plus200;
    public GameObject plus1000;
    private List<GameObject> plusList = new List<GameObject>();
    public GameObject iconduckPrefab;
    public GameObject iconmissPrefab;
    private List<GameObject> livesList = new List<GameObject>();
    public static ScoreController instance;
    private int lives = 10;

    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
        float x = -6.2f;
        float y = -4.58f;
        for(int i = 0; i < lives; i++) {
            var duck = Instantiate(iconduckPrefab, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
            livesList.Add(duck);
            x += 0.6f;
        }

    }

     // Update is called once per frame
    void Update() 
    {
        for(int i = 0; i < plusList.Count; i++) {
            plusList[i].GetComponent<Transform>().Translate(Vector3.up * Time.deltaTime * 1f);   
            if(plusList[i].transform.position.y > 6f) {
                plusList.RemoveAt(i);                
            }
        }
    }

    public void printPoints(int points, Vector3 p) {
        Vector3 position = new Vector3(2f, 2f, 0f);
        GameObject plusText = null;
        switch(points) {
            case 50:
                plusText = Instantiate(plus50, position, Quaternion.identity) as GameObject;                
                break;
            case 100:
                plusText = Instantiate(plus100, position, Quaternion.identity) as GameObject;                
                break;
            case 150:
                plusText = Instantiate(plus150, position, Quaternion.identity) as GameObject;                
                break;
            case 200:
                plusText = Instantiate(plus200, position, Quaternion.identity) as GameObject;                
                break;
            case 1000:
                plusText = Instantiate(plus1000, position, Quaternion.identity) as GameObject;                
                break;
        }
        plusList.Add(plusText);

    }    

    public void updateScoreText(int value) {
        score += value;
        scoreTxt.text = score.ToString();
    }

    public void missTarget(float y) {
        
        lives--;
        int pos = lives;
        GameObject go = livesList[pos];
        SpriteRenderer sr = go.GetComponent<SpriteRenderer>();
        sr.sprite = iconmissPrefab.GetComponent<SpriteRenderer>().sprite;
        for(int i = 0; i < livesList.Count; i++) {
            livesList[i].transform.position = new Vector2(livesList[i].transform.position.x, livesList[i].transform.position.y);
        }
        if(y <= 2f) {
            GameController.instance.removeFromList("bottom");
        }
        else {
            GameController.instance.removeFromList("above");
        }
        if(lives == 0) {
            SceneManager.LoadScene("GameOver");
        }

    }

}