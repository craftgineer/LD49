using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public Transform spawnPoint;
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

    public Image healthbar;

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
        if(!isDead){
            GroundCheck();
            PlayerInput();
        }     
    }

    void LateUpdate(){
        if(regenUnlocked && !isDead){
            Regenerate();
        }
    }

    public void Respawn(){
        transform.position = spawnPoint.position;
        health = maxHealth;
        UpdateHealthBar();
        isDead = false;
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

            AudioPlayer.Instance.PlaySoundByName("Jump", null);
        } else if(doubleJumpUnlocked && Input.GetKeyDown(KeyCode.Space) && !isGrounded && !hasDoubleJumped){
            rb.AddForce(Vector2.up * jumpBase);
            hasDoubleJumped = true;

            AudioPlayer.Instance.PlaySoundByName("DoubleJump", null);
        }

        //ATTACK
        if(wordUnlocked && Input.GetKeyDown(KeyCode.Z) && !isAttacking){
            TriggerAnimation("word_attack");

            AudioPlayer.Instance.PlaySoundByName("PrettyWords", null);
        } else if (hugUnlocked && Input.GetKeyDown(KeyCode.X) && !isAttacking){
            TriggerAnimation("hug_attack");

            AudioPlayer.Instance.PlaySoundByName("Hugs", null);
        }

        if(dashUnlocked && Input.GetKeyDown(KeyCode.LeftShift)){
            if(isFacing == "right"){
                rb.AddForce(Vector2.right * dashForce);
            }else{
                rb.AddForce(Vector2.left * dashForce);
            }

            AudioPlayer.Instance.PlaySoundByName("Dash", null);
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
        if(!isDead){
            health -= value;
            UpdateHealthBar();
            if(health <= 0){
                isDead = true;
                GameManager.gm.ToggleDeadMenu();
                AudioPlayer.Instance.PlaySoundByName("Death", null);
            } else{
                AudioPlayer.Instance.PlaySoundByName("Hit", null);
            }
        }
        
    }

    public void HoldItem(GameObject item){
        if(Time.time - pickupTime > 1f){
            item.transform.parent = this.transform;
            itemHeld = item;
            pickupTime = Time.time;

            AudioPlayer.Instance.PlaySoundByName("Confirm", null);
        }
    }

    private void DropItem(){
        if(Time.time - pickupTime > 1f){
            itemHeld.transform.parent = null;
            itemHeld = null;
            pickupTime = Time.time;

            AudioPlayer.Instance.PlaySoundByName("Cancel", null);
        }
    }

    public void Regenerate(){
        if(health < 100){
            health += 1 * Time.deltaTime;
            UpdateHealthBar();
        }
    }

    public bool CheckAbility(int npcNumber){
        bool answer = false;
        switch(npcNumber){
            case 1:
                answer = wordUnlocked;
                PlayerPrefs.SetInt("npc1_helped", 1);
                break;
            case 2:
                answer = doubleJumpUnlocked;
                PlayerPrefs.SetInt("npc2_helped", 1);
                break;
            case 3:
                answer = hugUnlocked;
                PlayerPrefs.SetInt("npc3_helped", 1);
                break;
            case 4:
                answer = dashUnlocked;
                PlayerPrefs.SetInt("npc4_helped", 1);
                break;
            case 5:
                answer = regenUnlocked;
                PlayerPrefs.SetInt("npc5_helped", 1);
                break;
        }
        return answer;
    }

    public void UnlockAbility(int npcNumber){
        string info = "";
        switch(npcNumber){
            case 1:
                wordUnlocked = true;
                info = "Nice Word Unlocked! You can now press 'Z' to say a positive word which may banish creatures of negativity!";
                break;
            case 2:
                doubleJumpUnlocked = true;
                info = "Double Jump Unlocked! Your new friend taught you how to jump an extra time in the air!";
                break;
            case 3:
                hugUnlocked = true;
                info = "Hug Unlocked! You can now press 'X' to hug, sometimes that's what someone negative needs!";
                break;
            case 4:
                dashUnlocked = true;
                info = "Dash Unlocked! You can now press'Left Shift' to dash, staying active is good for the mind!";
                break;
            case 5:
                regenUnlocked = true;
                info = "Regen Unlocked! You've banished the negativity, allowing your body to heal on its own!";
                break;
        }
        GameManager.gm.ToggleInfoMenu(info);
    }

    private void UpdateHealthBar(){
        healthbar.fillAmount = health / maxHealth;
    }
}
