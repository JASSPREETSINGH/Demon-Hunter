using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class jammingeeer : MonoBehaviour {
    [SerializeField]
    GameObject mExplosionPrefab;
    //life bar
    public Image lifebar;
    public float life = 1;

    // Wall kicking

    Vector2 mFacingDirection;

    void Start()
    {
        mFacingDirection = Vector2.left;
    }

    //on hitting by the bullet 
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("playerBullet"))
        {
            if (col.gameObject.layer == LayerMask.NameToLayer("playerBullet"))
            {
                life = life - (float)(0.10);
                Destroy(col.gameObject);
            }

        }
        // TODO: Check if I am being hit by a bullet
        //       If that's the case, do the following:
        //          - Destroy the bullet
        //          - Destroy myself
        //          - Instantiate an explosion (use the prefab "mExplosionPrefab")
    }
    public Vector2 GetFacingDirection()
    {
        return mFacingDirection;
    }
    void Update()
    {

        //life and destroy
        lifebar.fillAmount = life;
        if (life <= 0)
        {
            Destroy(gameObject);
            Instantiate(mExplosionPrefab, transform.position, Quaternion.identity);
            SceneManager.LoadScene("endGameWin");
        }
     
    }

}
 