using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
   
    private bool isJump = false;                                                   // 是否正在跳跃
    private Vector2 nextLeftPlatformPos;                                    // 下一个左边平台的位置
    private Vector2 nextRightPlatformPos;                                 // 下一个右边平台的位置
    private float lastPlatformPlayerPosY;                                     //上一个平台上时主角的y轴坐标
    private bool isMoveLeft;                                                        //是否向左边移动       
    private SpriteRenderer spriteRenderer;                                 //渲染器
    private BoxCollider2D boxCollider;                                        //碰撞器

    /// <summary>
    /// 初始化玩家
    /// </summary>
    public void Init(Sprite sprite, Vector3 initPos)
    {
        //设置初始位置
        transform.position = initPos;
        lastPlatformPlayerPosY = -32767;
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
        if(boxCollider == null)
        {
            boxCollider = GetComponent<BoxCollider2D>();
        }
        gameObject.SetActive(true);
        spriteRenderer.sortingLayerName = "Player";
        boxCollider.isTrigger = false;
        //初始化皮肤
        spriteRenderer.sprite = sprite;
    }
    /// <summary>
    /// 判断是否点击到了UI上
    /// </summary>
    /// <returns></returns>
    private bool IsClickOnUI(Vector2 mousePos)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = mousePos;
        List<RaycastResult> resultList = new List<RaycastResult>();
        //通过射线检测点击到的UI
        EventSystem.current.RaycastAll(eventData, resultList);
        return resultList.Count > 0;
    }

	void FixedUpdate () {
        ///----------------------------------------
        ///发布到安卓平台上时，用nput.GetMouseButtonDown(0)
        ///做判断时不知道为什么有时候检测不到点击事件，因此这里
        ///使用预编译指令来决定使用生么方式做点击判断
        ///-----------------------------------------
#if UNITY_ANDROID && !UNITY_EDITOR
        if (Input.touchCount > 0 && !isJump)
#endif
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0) && !isJump)
#endif
        {
            Vector3 mousePos = Input.mousePosition;
            if (IsClickOnUI(mousePos)) { return; }

            //点击屏幕左侧
            if(mousePos.x <= Screen.width / 2)
            {
                isJump = true;
                //人物朝左
                transform.localScale = new Vector3(-1, 1, 1);
                isMoveLeft = true;
            }
            //点击屏幕右侧
            else if (mousePos.x > Screen.width / 2)
            {
                isJump = true;
                //人物朝右
                transform.localScale = new Vector3(1, 1, 1);
                isMoveLeft = false;
            }
            Jump();
            lastPlatformPlayerPosY = transform.position.y;
        }

        //判断是否坠落
        if(transform.position.y < lastPlatformPlayerPosY)
        {
            PlayerDrop();
        }
    }
    //跳跃
    private void Jump()
    {
        if (isMoveLeft)
        {
            transform.DOMoveX(nextLeftPlatformPos.x, 0.15f);
            transform.DOMoveY(nextLeftPlatformPos.y + 0.6f, 0.1f);
        }
        else
        {
            transform.DOMoveX(nextRightPlatformPos.x, 0.15f);
            transform.DOMoveY(nextRightPlatformPos.y + 0.6f, 0.1f);
        }
        //广播事件码以生成新的平台
        EventCenter.Broadcast(EventDefine.CreatPlatform);
    }
    /// <summary>
    /// 玩家坠落
    /// </summary>
    private void PlayerDrop()
    {
        //将玩家渲染到障碍物后面
        spriteRenderer.sortingLayerName = "DeadPlayer";
        //设为触发器
        boxCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //跳到到平台
        if (collision.tag == "Platform")
        {
            isJump = false;
            nextLeftPlatformPos = collision.transform.position + new Vector3(-PlatformManger.nextPosX, PlatformManger.nextPosY, 0);
            nextRightPlatformPos = nextLeftPlatformPos + new Vector2(2 * PlatformManger.nextPosX, 0);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //跳到障碍物或者突刺上
        if (collision.collider.tag == "Obstacle")
        {
            DestroyPlayer(false, true);
        }
    }
    /// <summary>
    /// 销毁玩家
    /// </summary>
    /// <param name="isTrueDestroy">是否真正销毁</param>
    /// <param name="playEffect">是否播放死亡动画</param>
    public void DestroyPlayer(bool isTrueDestroy, bool isPlayEffect)
    {
        if (isTrueDestroy)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
        if (isPlayEffect)
        {
            EventCenter.Broadcast(EventDefine.PlayerDeath);
        }
        //玩家死亡一秒后结束当前游戏
        GameManager.instance.GameOver(1);
    }
    /// <summary>
    /// 玩家跌落死亡时，当玩家消失在相机内时销毁玩家
    /// </summary>
    private void OnBecameInvisible()
    {
        //销毁玩家
        DestroyPlayer(false, true);
    }
}
