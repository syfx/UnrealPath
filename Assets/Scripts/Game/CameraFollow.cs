using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform target;          //跟随目标
    /// <summary>
    /// 目标与相机之间的初始偏移量
    /// </summary>
    private Vector3 offset;
    /// <summary>
    /// 平滑时间
    /// </summary>
    public float smoothTime = 0.3F;
    private Vector3 Velocity = Vector3.zero;

    private void Update()
    {
        if(target == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            offset = transform.position - target.transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (target == null || !GameManager.instance.IsEnd)
            return;
        if(target.transform.position.y > transform.position.y - offset.y)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, offset.x + target.position.x, 
                ref Velocity.x, smoothTime);
            float posY = Mathf.SmoothDamp(transform.position.y, offset.y + target.position.y,
                ref Velocity.y, smoothTime);
            transform.position = new Vector3(posX, posY, transform.position.z);
        }
    }
}
