using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck3 : MonoBehaviour
{

    private GameObject go;
    private float timePassed;
    private float time = 1f;
    private Rigidbody2D rb;
    public float speed;
    public float jump;

    // Start is called before the first frame update
    void Start()
    {        
    }

    // Update is called once per frame
    void Update()
    {
        moveX();
    }

    bool checkTime() {
        
        timePassed += Time.deltaTime;
        if(timePassed >= time) {
            timePassed = 0;
            return true;  
        }
        else {
            return false;
        }

    }

    void moveX() {        
                
        GetComponent<Transform>().Translate(Vector3.right * Time.deltaTime * speed);        
        if(GetComponent<Rigidbody2D>().position.x > 9f) {
            Destroy(gameObject);
            ScoreController.instance.missTarget(GetComponent<Rigidbody2D>().position.y);
        }
        
    }

}