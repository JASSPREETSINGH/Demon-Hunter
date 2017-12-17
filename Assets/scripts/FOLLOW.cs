using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOLLOW : MonoBehaviour {

    [SerializeField]
    Transform mTarget;
    [SerializeField]
    float mFollowSpeed;
    [SerializeField]
    float mFollowRange;

    float mArriveThreshold = 0.05f;

    void Update()
    {
        if (mTarget != null)
        {
            if (Vector3.Distance(mTarget.position, transform.position) < mFollowRange)
            {
                Vector3 localPosition = mTarget.transform.position - transform.position;

                localPosition = localPosition.normalized; // The normalized direction in LOCAL space
                transform.Translate(localPosition.x * Time.deltaTime * mFollowSpeed, localPosition.y * Time.deltaTime * mFollowSpeed, localPosition.z * Time.deltaTime * mFollowSpeed);   // TODO: Make the enemy follow the target "mTarget"
            }                            //       only if the target is close enough (distance smaller than "mFollowRange")
                                         //      Get distance by simple substraction.
        }
    }

    public void SetTarget(Transform target)
    {
        mTarget = target;
    }
}
