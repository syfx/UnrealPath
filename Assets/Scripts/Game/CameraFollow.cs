using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Vector3 offset;                                 // 目标与相机之间的初始偏移量
    private Vector3 Velocity = Vector3.zero;
    /// <summary>
    /// 相机跟随目标
    /// </summary>
    public Transform Target { get; set; }
    /// <summary>
    /// 平滑时间
    /// </summary>
    public float smoothTime = 0.3F;
    
    /// <summary>
    /// 根据初始偏移量重置相机位置
    /// </summary>
    public  void Reset()
    {
        transform.position = Target.position + offset;
    }

    private void Update()
    {
        if(Target == null && GameObject.FindGameObjectWithTag("Player") != null)
        {
            Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            offset = transform.position - Target.transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (Target == null)
            return;
        if(Target.transform.position.y > transform.position.y - offset.y)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, offset.x + Target.position.x, 
                ref Velocity.x, smoothTime);
            float posY = Mathf.SmoothDamp(transform.position.y, offset.y + Target.position.y,
                ref Velocity.y, smoothTime);
            transform.position = new Vector3(posX, posY, transform.position.z);
        }
    }
}
