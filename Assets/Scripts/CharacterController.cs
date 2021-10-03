using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public static CharacterController player;
    // components
    private Rigidbody2D rb;
    private CapsuleCollider2D cr;
    public Collider2D groundCheckCollider;
    public LayerMask isGround;
    private Animator anim;

    private bool isGrounded;
    private string isFacing;
    private Vector3 curScale;

    public GameObject[] positiveWords;
    public GameObject itemHeld;
    public GameObject hugObject;
    public GameObject wordObject;

    public float movementBase = 5f;
    public float jumpBase = 10f;
    public float dashForce = 50f;
    public float pickupTime;
    

    public float health;
    public float maxHealth;

    private bool isAttacking;
    private bool hasDoubleJumped;

    public bool wordUnlocked;
    public bool doubleJumpUnlocked;
    public bool regenUnlocked;
    public bool dashUnlocked;
    public bool hugUnlocked;

    public bool isDead;
    
    void Awake(){
        player = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        wordObject.SetActive(false);
        hugObject.SetActive(false);
        isGrounded = false;
        hasDoubleJumped = false;
        isFacing = "right";
        curScale = gameObject.transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        cr = GetComponent<CapsuleCollider2D>();
        anim = GetComponent<Animator>();

        wordUnlocked = false;
        doubleJumpUnlocked = false;
        regenUnlocked = false;
        dashUnlocked = false;
        hugUnlocked = false;

        pickupTime = Time.time;  
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        PlayerInput();
    }

    void LateUpdate(){
        if(regenUnlocked && !isDead){
            Regenerate();
        }
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
            rb.AddForce(Vector2.left * movementBase);
            updateFacing("left");
        } else if(Input.GetKey(KeyCode.D)){
            rb.AddForce(Vector2.right * movementBase);
            updateFacing("right");
        }

        //JUMP
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
            rb.AddForce(Vector2.up * jumpBase);
        } else if(doubleJumpUnlocked && Input.GetKeyDown(KeyCode.Space) && !isGrounded && !hasDoubleJumped){
            rb.AddForce(Vector2.up * jumpBase);
            hasDoubleJumped = true;
        }

        //ATTACK
        if(wordUnlocked && Input.GetKeyDown(KeyCode.Z) && !isAttacking){
            TriggerAnimation("word_attack");
        } else if (hugUnlocked && Input.GetKeyDown(KeyCode.X) && !isAttacking){
            TriggerAnimation("hug_attack");
        }

        if(dashUnlocked && Input.GetKeyDown(KeyCode.LeftShift)){
            if(isFacing == "right"){
                rb.AddForce(Vector2.right * dashForce);
            }else{
                rb.AddForce(Vector2.left * dashForce);
            }
        }

        //ITEM
        if(itemHeld != null && Input.GetKey(KeyCode.E)){
            DropItem();
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

    public void TakeDamage(int value){
        health -= value;
        if(health <= 0){
            //TODO: dead
        }
    }

    public void HoldItem(GameObject item){
        if(Time.time - pickupTime > 1f){
            item.transform.parent = this.transform;
            itemHeld = item;
            pickupTime = Time.time;
        }
    }

    private void DropItem(){
        if(Time.time - pickupTime > 1f){
            itemHeld.transform.parent = null;
            itemHeld = null;
            pickupTime = Time.time;
        }
    }

    public void Regenerate(){
        if(health < 100){
            health += 1;
        }
    }
}
