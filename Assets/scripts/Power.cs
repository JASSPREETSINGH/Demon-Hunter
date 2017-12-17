using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("player"))
        {
            GameObject.FindGameObjectWithTag("hero").SendMessage("PowerUp", 0.20f); ;
            Destroy(gameObject);
        }
    }

}