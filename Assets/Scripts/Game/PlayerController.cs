using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PlayerController : MonoBehaviour {

    /// <summary>
    /// 是否正在跳跃
    /// </summary>
    private bool isJump = false;
    /// <summary>
    /// 下一个左边平台的位置
    /// </summary>
    private Vector2 nextLeftPlatformPos;
    /// <summary>
    /// 下一个右边平台的位置
    /// </summary>
    private Vector2 nextRightPlatformPos;
    private bool isMoveLeft;

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
        //点击屏幕
        if (Input.GetMouseButtonDown(0) && !isJump)
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
        }
    }

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

        //生成新的平台
        EventCenter.Broadcast(EventDefine.CreatPlatform);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //跳到到平台
        if (collision.transform.tag == "Platform")
        {
            isJump = false;
            nextLeftPlatformPos = collision.transform.position + new Vector3(-PlatformMnager.nextPosX, PlatformMnager.nextPosY, 0);
            nextRightPlatformPos = nextLeftPlatformPos + new Vector2(2 * PlatformMnager.nextPosX, 0);
        }
        //跳到障碍物上
        if (collision.transform.tag == "Obstacle")
        {
            print("dead");
        }
    }
}
