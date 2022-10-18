using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHeadStunParticle : MonoBehaviour
{
    [SerializeField] Transform followPoint;
    public bool followActive;
   public void StartFollowing()
    {
        followActive = true;
        StartCoroutine(Following());
    }
    IEnumerator Following()
    {
        while (followActive)
        {
            transform.position = followPoint.position + new Vector3(0, 10, 0);
            yield return null;
        }
    }
}
