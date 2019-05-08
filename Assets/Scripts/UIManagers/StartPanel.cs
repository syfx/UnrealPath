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

    public void Awake()
    {
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
    }

    /// <summary>
    /// 当点击开始按钮时调用
    /// </summary>
    private void OnStartButtonClick()
    {
        //TODO
        //打开游戏面板
        EventCenter.Broadcast(EventDefine.ShowGamePanel);
        //隐藏自己
        gameObject.SetActive(false);
    }

    /// <summary>
    /// 当点击人物按钮时调用
    /// </summary>
    private void OnFigureButtonClick()
    {
        //TODO

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
}
