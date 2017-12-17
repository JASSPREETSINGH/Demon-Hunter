using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Hero : MonoBehaviour
{
    //variables
    public float mMoveSpeed;
    public float mJumpForce;
    public LayerMask mWhatIsGround;
    float kGroundCheckRadius = 0.1f;

    // Animator booleans
    bool mJumping;
    bool mFalling;
    bool mMoving;
    bool mGrounded;


    // Damage effects
    float kDamagePushForce = 1.5f;

    // Invincibility timer
    float kInvincibilityDuration = 1.0f;
    float mInvincibleTimer;
    bool mInvincible;

    // Wall kicking

    Vector2 mFacingDirection;
    public GameObject mExplosionPrefab;

    // References to other components and game objects
    Animator mAnimator;
    Rigidbody2D mRigidBody2D;
    List<GroundCheck> mGroundCheckList;

    //life bar
    public Image lifebar;
    public float life = 1;

    void Start()
    {
        // Get references to other components and game objects
        mAnimator = GetComponent<Animator>();
        mRigidBody2D = GetComponent<Rigidbody2D>();
        mFacingDirection = Vector2.right;

        // Obtain ground check components and store in list
        mGroundCheckList = new List<GroundCheck>();
        GroundCheck[] groundChecksArray = transform.GetComponentsInChildren<GroundCheck>();
        foreach (GroundCheck g in groundChecksArray)
        {
            mGroundCheckList.Add(g);
        }

    }

    void Update()
    {

        //start jump 
        if (mGrounded && Input.GetButtonDown("jump"))
        {
            mRigidBody2D.AddForce(Vector2.up * mJumpForce, ForceMode2D.Impulse);
        }
        mJumping = mRigidBody2D.velocity.y > 0.0f;

        //start falling
        mFalling = mRigidBody2D.velocity.y < 0.0f;

        //start runnning or moving
        mMoving = false;
        if (Input.GetButton("boost"))
        {
            mMoveSpeed = mMoveSpeed + (float)0.05;
        }
        else
        {
            mMoveSpeed = 2;
        }

        if (Input.GetButton("left"))
        {
            transform.Translate(Vector2.left * mMoveSpeed * Time.deltaTime, Space.World);
            FaceDirection(Vector2.left);
            mMoving = true;
        }
        if (Input.GetButton("right"))
        {
            transform.Translate(Vector2.right * mMoveSpeed * Time.deltaTime, Space.World);
            FaceDirection(Vector2.right);
            mMoving = true;
        }

        //check grounded
        bool grounded = CheckGrounded();
        mGrounded = grounded;

        //life and destroy
        lifebar.fillAmount = life;
        if (life <= 0)
        {
            Destroy(gameObject);
            Instantiate(mExplosionPrefab, transform.position, Quaternion.identity);
            SceneManager.LoadScene("gamelost");
        }
        if (mInvincible)
        {
            mInvincibleTimer += Time.deltaTime;
            if (mInvincibleTimer >= kInvincibilityDuration)
            {
                mInvincible = false;
                mInvincibleTimer = 0.0f;
            }
        }

        UpdateAnimator();
    }

    public Vector2 GetFacingDirection()
    {
        return mFacingDirection;
    }

    private void FaceDirection(Vector2 direction)
    {
        mFacingDirection = direction;
        if (direction == Vector2.right)
        {
            Vector3 newScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
        else
        {
            Vector3 newScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            transform.localScale = newScale;
        }
    }

    private bool CheckGrounded()
    {
        foreach (GroundCheck g in mGroundCheckList)
        {
            if (g.CheckGrounded(kGroundCheckRadius, mWhatIsGround, gameObject))
            {
                return true;
            }
        }
        return false;
    }

    private void UpdateAnimator()
    {
        mAnimator.SetBool("isJumping", mJumping);
        mAnimator.SetBool("isFalling", mFalling);
        mAnimator.SetBool("isMoving", mMoving);
        mAnimator.SetBool("isGrounded", mGrounded);
        mAnimator.SetBool("isHurt", mInvincible);
    }
    //on hitting by the bullet 
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("enemyBullet"))
        {
            TakeDamage(0.10f);
            PushBack();
            Destroy(col.gameObject);
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("enemy"))
        {

            TakeDamage(0.10f);
            PushBack();
           
        }
    }
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("enemy"))
        {
            TakeDamage(0.001f);
            PushBack();
            mInvincible = true;

        }
    }

    public void TakeDamage(float damage)
    {
        life = life - damage;
    }

    public void PowerUp(float amount)
    {
        life = life + amount;
    }


    public void PushBack()
    {
        if (!mInvincible)
        {
            Vector2 forceDirection = new Vector2(-mFacingDirection.x, 0.3f) * kDamagePushForce;
            mRigidBody2D.velocity = Vector2.zero;
            mRigidBody2D.AddForce(forceDirection, ForceMode2D.Impulse);
            mInvincible = true;
       
        }
    }


}
