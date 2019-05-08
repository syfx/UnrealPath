using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : MonoBehaviour
{
    private Text txt_RemCount;             //获得的钻石数
    private Text txt_Score;                     //得分
    private Button btn_Pause;               //暂停按钮
    private Button btn_Play;                  //开始按钮
    private Button btn_ReStart;             //重新开始按钮

    public void Awake()
    {
        Init();
        EventCenter.AddListener(EventDefine.ShowGamePanel, Show);
    }

    private void Init()
    {
        //初始时设为禁用状态
        gameObject.SetActive(false);
        txt_RemCount = transform.Find("Gem/txtGemCount").GetComponent<Text>();
        txt_Score = transform.Find("txtScore").GetComponent<Text>();
        btn_Pause = transform.Find("Buttons/btnPause").GetComponent<Button>();
        btn_Pause.onClick.AddListener(OnPauseButtonClick);
        btn_Play = transform.Find("Buttons/btnPlay").GetComponent<Button>();
        //初始时禁用开始按钮
        btn_Play.gameObject.SetActive(false);
        btn_Play.onClick.AddListener(OnPlayButtonClick);
        btn_ReStart = transform.Find("Buttons/btnReStart").GetComponent<Button>();
        btn_ReStart.onClick.AddListener(OnReStartButtonClick);
    }

    /// <summary>
    /// 显示游戏面板
    /// </summary>
    private void Show()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// 点击重新开始按钮时调用
    /// </summary>
    private void OnReStartButtonClick()
    {
        
    }

    /// <summary>
    /// 点击开始按钮时调用
    /// </summary>
    private void OnPlayButtonClick()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 点击暂停按钮时调用
    /// </summary>
    private void OnPauseButtonClick()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 销毁时移除监听
    /// </summary>
    public void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowGamePanel, Show);
    }
}
