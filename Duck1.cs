using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duck1 : MonoBehaviour
{

    private GameObject go;
    private float timePassed;
    private float time = 1f;
    private bool isFloating = false;
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
        if(checkTime()) {
            isFloating = !isFloating;
        }
        if(isFloating) {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
        else {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<Transform>().Translate(Vector3.up * Time.deltaTime * jump);
        }
        if(GetComponent<Rigidbody2D>().position.x > 9f) {
            Destroy(gameObject);
            ScoreController.instance.missTarget(GetComponent<Rigidbody2D>().position.y);
        }
        
    }

}