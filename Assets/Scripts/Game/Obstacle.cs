using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    private float lifeTime;                         //平台生存周期
    private bool isDrop;                          //是否下落
    private float dropSpeed;                   //下落速度
    private float startSpeed;                   //起始速度
    private float acceleratedSpeed;        //加速度
    private ObjectPool myObjectPool;   //当前物的对象池

    public void Awake()
    {
        lifeTime = 30;
        isDrop = false;
        startSpeed = 1;
        acceleratedSpeed = 0.2f;
    }
    public void Update()
    {
        if (isDrop)
        {
            transform.position += Vector3.down * dropSpeed * Time.deltaTime;
            dropSpeed += acceleratedSpeed;
        }
    }

    /// <summary>
    /// 初始化时在外部调用来设置皮肤和生命周期
    /// </summary>
    /// <param name="sprite"></param>
    /// <param name="life"></param>
    public void Init(ObjectPool objPool, float life)
    {
        //设置对象池
        myObjectPool = objPool;
        //初始化生存周期
        lifeTime = life;

        isDrop = false;
        dropSpeed = startSpeed;
        StartCoroutine("Drop");
    }
    /// <summary>
    /// 销毁障碍物
    /// </summary>
    /// <param name="time">多少秒后销毁</param>
    public void DestroySelf(float time)
    {
        StartCoroutine("DestroyObstacle", time);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator Drop()
    {
        yield return new WaitForSeconds(lifeTime);
        isDrop = true;
        StartCoroutine("DestroyObstacle", 0.8f);
    }

    IEnumerator DestroyObstacle(float time)
    {
        yield return new WaitForSeconds(time);
        if (myObjectPool != null)
        {
            myObjectPool.PutInObject(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //碰到底部时销毁
        if (collision.tag == "Bottom")
        {
            isDrop = true;
            StartCoroutine("DestroyObstacle", 0.2f);
        }
    }
}
