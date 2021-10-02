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
    public float dashForce = 50f;
    public float deaccel = 5f;
    private Vector2 moveSpeed;

    private bool isAttacking;
    private bool hasDoubleJumped;
    
    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
        hasDoubleJumped = false;
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
        if(isGrounded){
            hasDoubleJumped = false;
        }
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
            //moveSpeed.x = -movementBase;
            rb.AddForce(Vector2.left * movementBase);
            updateFacing("left");
        } else if(Input.GetKey(KeyCode.D)){
            //moveSpeed.x = movementBase;
            rb.AddForce(Vector2.right * movementBase);
            updateFacing("right");
        } else{
            //moveSpeed.x = rb.velocity.x;
        }

        //JUMP
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            rb.AddForce(Vector2.up * jumpBase);
        } else if(Input.GetKeyDown(KeyCode.Space) && !isGrounded && !hasDoubleJumped){
            rb.AddForce(Vector2.up * jumpBase);
            hasDoubleJumped = true;
        }

        //ATTACK
        if(Input.GetKeyDown(KeyCode.Z) && !isAttacking){
            TriggerAnimation("word_attack");
        } else if (Input.GetKeyDown(KeyCode.X) && !isAttacking){
            TriggerAnimation("hug_attack");
        }

        if(Input.GetKeyDown(KeyCode.LeftShift)){
            if(isFacing == "right"){
                rb.AddForce(Vector2.right * dashForce);
            }else{
                rb.AddForce(Vector2.left * dashForce);
            }
            

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

    public void DoneAttacking(){
        isAttacking = false;
    }

    public void TriggerAnimation(string item){
        anim.SetTrigger(item);
        isAttacking = true;
    }

    void Movement(){
        //rb.velocity = new Vector3(rb.velocity.x -);
    }
}
