using UnityEngine;
using System.Collections;

public class platform : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "hero")
        {
            collider.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D collider)
    {
        if (collider.gameObject.tag == "hero")
        {
            collider.transform.SetParent(null);
        }
    }
}
