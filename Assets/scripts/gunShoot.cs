using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunShoot : MonoBehaviour {
    Animator mAnimator;
    bool mShooting;
    public float reloadTime = 2.10f;
    float timer = 0f;
 

    float kShootDuration = 0.25f;
    float mTime;

    public GameObject mBulletPrefab;
    Hero mHeroRef;
    AudioSource mBusterSound;
    // Use this for initialization
    void Start () {
        mAnimator = GetComponentInParent<Animator>();
        // TODO: Get a reference to the following items and store them 
        mHeroRef = GetComponentInParent<Hero>();
        //          - MegaMan component in the "Mega Man" game object (store in "mMegaManRef")
        mBusterSound = GetComponent<AudioSource>();
        //          - AudioSource component in "BusterGun" game object (store in "mBusterSound")
    }

    // Update is called once per frame
    void Update () {
        timer += Time.deltaTime;
        if (timer >= reloadTime)
        {
            if (Input.GetButtonDown("fire"))
            {
                Vector2 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 myPos = new Vector2(transform.position.x, transform.position.y);
                Vector2 direction = target - myPos;
                direction.Normalize();

                GameObject bulletObject = Instantiate(mBulletPrefab, transform.position, Quaternion.identity);       //Call instantiate
                Bullet bullet = bulletObject.GetComponent<Bullet>();

                bullet.SetDirection(direction);
                mBusterSound.Play();

                // Set animation params
                mShooting = true;
              
                timer = 0f;
            }
        }
        if (mShooting)
        {
            mTime += Time.deltaTime;
            if (mTime > kShootDuration)
            {
                mShooting = false;
            }
        }
        UpdateAnimator();
    }

    private void UpdateAnimator()
    {
        mAnimator.SetBool("isFiring", mShooting);
    }
}
