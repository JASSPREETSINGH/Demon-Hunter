using UnityEngine;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    [SerializeField]
    GameObject mExplosionPrefab;
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            GameObject.FindGameObjectWithTag("hero").GetComponent<Hero>().TakeDamage(30f);
      
            Instantiate(mExplosionPrefab, transform.position, Quaternion.identity);
        }
    }
}
