using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // components
    private Rigidbody2D rb;
    private CapsuleCollider2D cr;
    public CircleCollider2D groundCheckCollider;
    public LayerMask isGround;
    private Animator anim;

    private bool isGrounded;
    private string isFacing;
    private Vector3 curScale;

    public GameObject[] positiveWords;

    public float movementBase = 5f;
    public float jumpBase = 10f;
    private Vector2 moveSpeed;
    
    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
        isFacing = "right";
        curScale = gameObject.transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        cr = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();
        moveSpeed = new Vector2();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        PlayerInput();
        Movement();
    }

    void GroundCheck(){
        isGrounded = Physics2D.IsTouchingLayers(groundCheckCollider, isGround);
    }

    void updateFacing(string current){
		if (current != isFacing) {
			isFacing = current;

			curScale.x *= -1;
			gameObject.transform.localScale = curScale;
		}
	}

    void PlayerInput()
    {
        //MOVE
        if(Input.GetKey(KeyCode.A)){
            moveSpeed.x = -movementBase;
            updateFacing("left");
        } else if(Input.GetKey(KeyCode.D)){
            moveSpeed.x = movementBase;
            updateFacing("right");
        } else{
            moveSpeed.x = 0;
        }

        if(Input.GetKey(KeyCode.Space) && isGrounded){
            moveSpeed.y = jumpBase;
        }else{
            moveSpeed.y = rb.velocity.y;
        }
    }

    public void DisplayPositiveWord(){
        int random = Random.Range(0, 3);
        positiveWords[random].SetActive(true);
    }

    public void DisablePositiveWord(){
        foreach(GameObject word in positiveWords){
            word.SetActive(false);
        }
    }

    void Movement(){
        rb.velocity = moveSpeed;

    }
}
