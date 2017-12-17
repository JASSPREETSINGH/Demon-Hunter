using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunshootEnemyA_6 : MonoBehaviour
{
    public float reloadTime = 2.55f;
    float timer = 0f;
    public GameObject mBulletPrefab;
    jammingeeer mJamRef;
    AudioSource mBusterSound;
    public float mExpirationTime;
    float mTimer;
    // Use this for initialization
    void Start()
    {
        mJamRef = GetComponentInParent<jammingeeer>();
        //          - MegaMan component in the "Mega Man" game object (store in "mMegaManRef")
        mBusterSound = GetComponent<AudioSource>();
        //          - AudioSource component in "BusterGun" game object (store in "mBusterSound")
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= reloadTime)
        {
             GameObject bulletObject = Instantiate(mBulletPrefab, transform.position, Quaternion.identity);       //Call instantiate
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bullet.SetDirection(mJamRef.GetFacingDirection());
            mBusterSound.Play();
            timer = 0f;
        }

    }


}