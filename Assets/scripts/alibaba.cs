using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alibaba : MonoBehaviour {
    [SerializeField]
     public GameObject mExplosionPrefab;
  
    // Damage effects
    float kDamagePushForce = 1.5f;
    
  
   
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("playerBullet"))
            {
               
                Destroy(gameObject);
            }
    }
  




}
