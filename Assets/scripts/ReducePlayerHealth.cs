using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReducePlayerHealth : MonoBehaviour {
    public Image lifebar;
    public float life = 1;
    public GameObject mrefplayer;
    [SerializeField]
    GameObject mExplosionPrefab;

    // Use this for initialization
    void Start () {
        

    }

    //on hitting by the bullet 
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("enemyBullet"))
        {
          life=life-(float)(0.10);
        }
    }
    
    // Update is called once per frame
    void Update () {
        lifebar.fillAmount = life;
        if(life<=0)
        {
            Destroy(mrefplayer);
            Instantiate(mExplosionPrefab, transform.position, Quaternion.identity);
        }
	}

}
