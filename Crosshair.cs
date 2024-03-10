using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed;
    private bool hasTarget = false;
    private GameObject target;
    public Sprite duckNewSprite;
    public AudioSource shotSound;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameObject.transform.position = new Vector2(-0.25f, -1.05f);
        shotSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        moveCrosshair();
        if(Input.GetKeyDown("space")) {
            if(hasTarget && target != null) {
                destroyTarget(target);   
            }
        }
    }

    void moveCrosshair() {
        
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        float x = gameObject.transform.position.x;

        if(v > 0f && transform.position.y <= 0.55f) {
            gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + (Time.deltaTime * 4f));
        }
        if(v < 0f && transform.position.y >= -2.9f) {
            gameObject.transform.position = new Vector2(transform.position.x, transform.position.y - (Time.deltaTime * 4f));
            return;
        }       
        
        if(h > 0f && x < 8.45f) {
            gameObject.transform.position = new Vector2(transform.position.x + (speed * Time.deltaTime), transform.position.y);
        }
        if(h < 0f && x > -8.45f) {
            gameObject.transform.position = new Vector2(transform.position.x + (-speed * Time.deltaTime), transform.position.y);
        }

    }

    void OnTriggerEnter2D(Collider2D collider) {
        
        if(collider.gameObject.tag == "Duck1_Tag" || collider.gameObject.tag == "Duck2_Tag" 
        || collider.gameObject.tag == "Duck3_Tag" || collider.gameObject.tag == "Duck4_Tag"
        || collider.gameObject.tag == "Bonus_Tag") {
            hasTarget = true;
            target = collider.gameObject;
        }
        
    }

    void OnTriggerExit2D(Collider2D collider) {
        
        hasTarget = false;
        target = null;

    }

    void destroyTarget(GameObject target) {
        
        SpriteRenderer[] sprites = target.transform.parent.gameObject.GetComponentsInChildren<SpriteRenderer>();
        sprites[2].sprite = duckNewSprite;
        if(target.tag == "Duck1_Tag") {
            ScoreController.instance.updateScoreText(50);
            ScoreController.instance.printPoints(50, gameObject.transform.position);
            remove(target);
        }
        if(target.tag == "Duck2_Tag") {
            ScoreController.instance.updateScoreText(100);
            ScoreController.instance.printPoints(100, gameObject.transform.position);
            remove(target);
        }
        if(target.tag == "Duck3_Tag") {
            ScoreController.instance.updateScoreText(150);
            ScoreController.instance.printPoints(150, gameObject.transform.position);
            remove(target);
        }
        if(target.tag == "Duck4_Tag") {
            ScoreController.instance.updateScoreText(200);
            ScoreController.instance.printPoints(200, gameObject.transform.position);
            remove(target);
        }
        if(target.tag == "Bonus_Tag") {
            ScoreController.instance.updateScoreText(1000);
            ScoreController.instance.printPoints(1000, gameObject.transform.position);
            remove(target);
        }
        shotSound.Play();

    }

    void remove(GameObject target) {
        
        if(target.tag != "Bonus_Tag") {            
            float y = target.transform.parent.GetComponent<Rigidbody2D>().position.y;
            if (y < -3f) {
                GameController.instance.removeFromList("bottom");
            }
            else {
                GameController.instance.removeFromList("above");
            }
            Destroy(target.transform.parent.gameObject, 1f);
            Destroy(target);
        }
        else {
            Destroy(target.transform.parent.gameObject, 1f);
            Destroy(target);
        }

    }

}