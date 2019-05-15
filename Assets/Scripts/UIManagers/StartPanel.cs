using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class StartPanel : MonoBehaviour
{
    private Button btn_Start;                   //开始游戏按钮
    private Button btn_Figure;                 //选择人物按钮
    private Button btn_Rank;                   //查看排行榜按钮
    private Button btn_Sound;                //打开/关闭声音按钮
    private Button btnExit;                      //退出游戏
    private Button btnPlatform;              //平台皮肤

    public void Awake()
    {
        EventCenter.AddListener(EventDefine.OpenStartPanel, OpenStartPanel);
        EventCenter.AddListener(EventDefine.CloseStartPanel, CloseStartPanel);
        Init();
    }

    private void Init()
    {
        btn_Start = transform.Find("Buttons/Start").GetComponent<Button>();
        btn_Start.onClick.AddListener(OnStartButtonClick);
        btn_Figure = transform.Find("Buttons/ButtonGroup/Figure").GetComponent<Button>();
        btn_Figure.onClick.AddListener(OnFigureButtonClick);
        btn_Rank = transform.Find("Buttons/ButtonGroup/Rank").GetComponent<Button>();
        btn_Rank.onClick.AddListener(OnRankButtonClick);
        btn_Sound = transform.Find("Buttons/ButtonGroup/Sound").GetComponent<Button>();
        btn_Sound.onClick.AddListener(OnSoundButtonClick);
        btnExit = transform.Find("ExitGame").GetComponent<Button>();
        btnExit.onClick.AddListener(OnExitButtonClick);
        btnPlatform = transform.Find("Buttons/ButtonGroup/Platform").GetComponent<Button>();
        btnPlatform.onClick.AddListener(OnPlatformButtonClick);
    }

    /// <summary>
    /// 点击退出游戏按钮
    /// </summary>
    private void OnExitButtonClick()
    {
        //退出游戏
        Application.Quit();
    }
    /// <summary>
    /// 当点击开始按钮时调用
    /// </summary>
    private void OnStartButtonClick()
    {
        //TODO
        //隐藏自己
        CloseStartPanel();
        //开始游戏
        GameManager.instance.StartGame();
    }
    /// <summary>
    /// 当点击人物按钮时调用
    /// </summary>
    private void OnFigureButtonClick()
    {
        //TODO
        EventCenter.Broadcast(EventDefine.SelectFigure);
    }
    /// <summary>
    /// 点击选择平台按钮
    /// </summary>
    private void OnPlatformButtonClick()
    {
        EventCenter.Broadcast(EventDefine.SelectPlatform);
    }

    /// <summary>
    /// 当点击排行按钮时调用
    /// </summary>
    private void OnRankButtonClick()
    {
        //TODO

    }
    /// <summary>
    /// 当点击声音按钮时调用
    /// </summary>
    private void OnSoundButtonClick()
    {
        //TODO

    }
    /// <summary>
    /// 打开开始游戏面板
    /// </summary>
    private void OpenStartPanel()
    {
        gameObject.SetActive(true);
    }
    /// <summary>
    /// 关闭开始游戏面板
    /// </summary>
    private void CloseStartPanel()
    {
        gameObject.SetActive(false);
    }
}
