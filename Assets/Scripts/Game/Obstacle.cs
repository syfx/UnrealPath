using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour {

    private float life;                               //平台生存周期
    private bool isDrop;                          //是否下落
    private float dropSpeed;                   //下落速度
    private float startSpeed;                   //起始速度
    private float acceleratedSpeed;        //加速度
    private ObjectPool myObjectPool;   //当前物的对象池

    /// <summary>
    /// 是否首次被加载
    /// </summary>
    public bool IsFirstLoad { get; private set; }


    public void Awake()
    {
        IsFirstLoad = true;

        life = 5;
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
        if (IsFirstLoad)
        {
            IsFirstLoad = false;
            //设置对象池
            myObjectPool = objPool;
            //初始化生存周期
            this.life = life;
        }
    }

    public void OnEnable()
    {
        isDrop = false;
        dropSpeed = startSpeed;
        StartCoroutine("Drop");
    }
    public void OnDisable()
    {
        StopCoroutine("Drop");
    }

    IEnumerator Drop()
    {
        yield return new WaitForSeconds(life);
        isDrop = true;
    }

    /// <summary>
    /// 渲染器在相机不可见时销毁游戏物体
    /// </summary>
    public void OnBecameInvisible()
    {
        //放入对象池中
        if (myObjectPool != null)
        {
            myObjectPool.PutInObject(gameObject);
        }
        else
        {
            Destroy(this);
        }
    }
}
