using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private const int MAX_PER_LINE = 5;
    private int maxLineBelow = 0;
    private int maxLineAbove = 0;
    private Vector3 POS_ABOVE_START = new Vector3(-9.55f, 0.55f, 0f);
    private Vector3 POS_BELOW_START = new Vector3(-9.55f, -1.15f, 0f);    
    
    private float MAX_TIME_CREATE = 0.8f;
    private float timer;
    private List<GameObject> targetsList = new List<GameObject>();
    public GameObject Duck1;
    public GameObject Duck2;
    public GameObject Duck3;
    public GameObject Duck4;
    public GameObject TargetBonus;
    public static GameController instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;        
    }

    // Update is called once per frame
    void Update()
    {
        if(calculateTime()) {
            createTargets();
            createTargetBonus();
        }
    }

    private GameObject createTarget() {
    
        int i = Random.Range(0, 4);
        switch(i) {
            case 0:
                return Duck1;            
            case 1:
                return Duck2;
            case 2:
                return Duck3;
            case 3:
                return Duck4;
            default:
                return Duck1;
        }

    }

    void createTargets() {
        
        int line = Random.Range(0, 2);
        int position = Random.Range(0, 2);
        GameObject targetType = createTarget();
        GameObject target = null;

        if(line == 0) {
            if(maxLineBelow < MAX_PER_LINE) {
                target = GameObject.Instantiate(targetType,
                        position == 0 ? POS_BELOW_START : POS_ABOVE_START, 
                        Quaternion.identity) as GameObject;                
                maxLineBelow++;
                targetsList.Add(target);        
            }
        }
        else if(line == 1) {
            if(maxLineAbove < MAX_PER_LINE) {
                target = Instantiate(targetType, 
                            position == 0 ? POS_BELOW_START : POS_ABOVE_START, 
                            Quaternion.identity) as GameObject;               
                maxLineAbove++;
                targetsList.Add(target);
            }
        }
        
        if(target != null) {            
            SpriteRenderer[] sprites;
            sprites = target.GetComponentsInChildren<SpriteRenderer>();
            if(sprites.Length > 2 && position == 0) {
                sprites[0].sortingOrder = 6;
                sprites[1].sortingOrder = 6;
                sprites[2].sortingOrder = 6;
                target.GetComponent<Renderer>().sortingOrder = 6;
            }
            else {
                sprites[0].sortingOrder = 4;
                sprites[1].sortingOrder = 4;
                sprites[2].sortingOrder = 4;
                target.GetComponent<Renderer>().sortingOrder = 4;
            }
        }        

    }

    void createTargetBonus() {

        int i = Random.Range(1, 9);
        if(i == 7) {
            GameObject bonus = GameObject.Instantiate(TargetBonus,
                new Vector3(-9.55f, -2.7f, 0f),
                Quaternion.identity) as GameObject; 
        }

    }

    bool calculateTime() {
        
        timer += Time.deltaTime;
        if(timer >= MAX_TIME_CREATE) {            
            timer = 0f;
            return true;
        }
        return false;

    }

    public void removeFromList(string pos) {
        
        if(pos == "bottom") {
            maxLineBelow--;
        }
        else {
            maxLineAbove--;
        }

    }

}