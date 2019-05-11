using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    private float lifeTime;                        //平台生存周期
    private bool isDrop;                          //是否下落
    private float dropSpeed;                   //下落速度
    private float startSpeed;                   //起始速度
    private float acceleratedSpeed;        //加速度
   
    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
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
    public void Init(Sprite sprite, float life)
    {
        //切换皮肤
        spriteRenderer.sprite = sprite;
        //初始化生存周期
        this.lifeTime = life;
        isDrop = false;
        dropSpeed = startSpeed;
        StartCoroutine("Drop");
    }
    /// <summary>
    /// 销毁当前平台
    /// </summary>
    /// <param name="time">多少秒后销毁</param>
    public void Destroy(float time)
    {
        StartCoroutine("DestroyPlatForm", time);
    }

    private void OnDisable()
    {
        StopCoroutine("Drop");
        StopCoroutine("DestroyPlatForm");
    }

    IEnumerator Drop()
    {
        yield return new WaitForSeconds(lifeTime);
        isDrop = true;
        StartCoroutine("DestroyPlatForm", 0.8f);
    }

    IEnumerator DestroyPlatForm(float time)
    {
        yield return new WaitForSeconds(time);
        PoolManager.PlatformPool.PutInObject(gameObject);
    }
}
