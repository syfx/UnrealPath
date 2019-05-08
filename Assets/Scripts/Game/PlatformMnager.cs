using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMnager : MonoBehaviour {

    private Vector2 platformStartPos;                           //平台起始生成的位置
    //下一平台生成位置与当前平台位置的差值
    private const float nextPosX = 0.544f; 
    private const float nextPosY = 0.642f;
    
    private int platformCount = 5;                                //当前要生成的平台的数量
    private bool isLeftCreat = true;                                //是否向左边生成

    public void Start()
    {
        platformStartPos = new Vector2(0, -2);
        while(platformCount > 0)
        {
            EnsurePath();
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            EnsurePath();
    }

    /// <summary>
    /// 确定生成的路径
    /// </summary>
	private void EnsurePath()
    {
        if(platformCount > 0)
        {
            CreatOnePlatform();
            --platformCount;
        }
        else
        {
            //生成完一组台阶之后，随机获取下一组台阶数量
            platformCount = Random.Range(1, 6);
            //改变生成方向
            isLeftCreat = !isLeftCreat;
            CreatOnePlatform();
        }
    }

    /// <summary>
    /// 生成一个台阶
    /// </summary>
    private void CreatOnePlatform()
    {
        GameObject go = PoolManager.PlatformPool.TakeOutObject();
        go.transform.parent = transform;
        go.transform.position = platformStartPos;

        if (isLeftCreat)
            platformStartPos += new Vector2(-nextPosX, nextPosY);
        else
            platformStartPos += new Vector2(nextPosX, nextPosY);
    }
}
