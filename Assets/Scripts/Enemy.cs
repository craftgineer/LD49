using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool weakToHug;
    public bool weakToWord;

    public bool canMove;
    public float moveSpeed;
    public bool facingRight;
	public Transform currentPoint;
    public Transform[] points;
    private int pointSelection;

    private float hitDelay = 1f;
    private float lastHit;

    void Start(){
        facingRight = false;
        if(canMove){
            pointSelection = 0;
		    currentPoint = points[pointSelection];
        }       
    }

    void OnCollisionEnter2D(Collision2D coll){
        if(coll.transform.tag == "Player"){
            Debug.Log("Enemy hit player");
            CharacterController.player.TakeDamage(25);
            lastHit = Time.time;
        }
    }

    void OnCollisionStay2D(Collision2D coll){
        if(coll.transform.tag == "Player" && ((Time.time - lastHit) > hitDelay)){
            Debug.Log("Enemy hit player again");
            CharacterController.player.TakeDamage(25);
            lastHit = Time.time;
        }
    }

    void OnTriggerEnter2D(Collider2D coll){
        if (weakToHug && coll.transform.tag == "Hug"){
            //TODO: Death animation
            Destroy(gameObject);

            AudioPlayer.Instance.PlaySoundByName("Death", null);
        } else if(weakToWord && coll.transform.tag == "Word"){
            //TODO: Death animation
            Destroy(gameObject);

            AudioPlayer.Instance.PlaySoundByName("Death", null);
        }
    }

    void Flip(){
		facingRight = !facingRight;
		Vector3 theScale = gameObject.transform.localScale;
		theScale.x *= -1;
		gameObject.transform.localScale = theScale;
	}

    // Update is called once per frame
	void FixedUpdate () {
        if(canMove){
            gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, currentPoint.position, Time.deltaTime * moveSpeed);

            float distance = Vector3.Distance(transform.position, currentPoint.position);
            if (distance <= moveSpeed) {
                pointSelection++;
                if (pointSelection == points.Length) {
                    pointSelection = 0;
                }

                currentPoint = points [pointSelection];
            }
            if (gameObject.transform.position.x < currentPoint.position.x && !facingRight) {
                Flip ();
            } else if (gameObject.transform.position.x > currentPoint.position.x && facingRight){
                Flip ();
            }
        }		
	}
}
