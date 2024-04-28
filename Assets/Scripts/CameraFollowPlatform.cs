using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlatform : CameraFollow
{
    protected override void Follow()
    {
        Vector3 newPos = new Vector3(target.position.x + 5f, target.position.y + 1f, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, _followSpeed * Time.deltaTime);
    }
}
