using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Diamond : MonoBehaviour {
   
    private Transform destination;               //碰到钻石后，钻石飞到此处
    private Vector2 startPos;                        //钻石初始位置
    private bool isFree = true;                      //未被玩家迟到
    private Vector3 Velocity;

    private void Awake()
    {
        destination = GameObject.Find("Main Camera/GemGather ").GetComponent<Transform>();
        Init();
    }
    //初始化
    public void Init()
    {
        startPos = transform.position;
        isFree = true;
    }
    void FixedUpdate() {
        //实现动画效果
        if (isFree)
        {
            if (transform.position.y <= startPos.y)
            {
                transform.DOMoveY(startPos.y + 0.3f, 0.6f);
            }
            if (transform.position.y >= startPos.y + 0.25f)
            {
                transform.DOMoveY(startPos.y - 0.05f, 0.6f);
            }
        }
        else
        {
            float posX = Mathf.SmoothDamp(transform.position.x, destination.position.x,
               ref Velocity.x, 0.2f);
            float posY = Mathf.SmoothDamp(transform.position.y, destination.position.y,
                ref Velocity.y, 0.2f);
            transform.position = new Vector3(posX, posY, transform.position.z);
        }
    }
    /// <summary>
    /// 销毁自己
    /// </summary>
    /// <param name="time">多长时间后销毁</param>
    public void DestroySelf(float time)
    {
        StartCoroutine("DestroyDiamond", time);
    }

    IEnumerator DestroyDiamond(float time)
    {
        yield return new WaitForSeconds(time);
        PoolManager.DiamondPool.PutInObject(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //被玩家碰到
        if(collision.tag == "Player" && destination != null)
        {
            AudioManager.instance.PlayGetGemMusic();
            isFree = false;
        }
        //碰到底部时销毁
        if (collision.tag == "Bottom")
        {
            StartCoroutine("DestroyDiamond", 0.5f);
        }
        //钻石数增加
        if(collision.tag == "GemGather")
        {
            //TODO
            StartCoroutine("DestroyDiamond", 0);
            GameManager.instance.ChangeGemCount(1);
        }
    }
    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
