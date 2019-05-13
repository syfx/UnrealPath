using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 人物皮肤选择面板
/// </summary>
public class ScriptSelectPanel : MonoBehaviour {

    private AssetManager assetManager;
    private float screenWidth;                          //当前设备的屏幕宽度
    private RectTransform rectTransform;       //矩形变换
    private GameObject childPrefab;               //用来显示皮肤
    private List<Sprite> sprites;                       //皮肤集合
    private int index = 0;                                  //当前展示的皮肤的索引值
    private int lastIndex = 0;                            //上一帧展示的皮肤的索引值
    private int itemCount = 0;                         //当前用来显示皮肤的格子数
    /// <summary>
    /// 选择的皮肤
    /// </summary>
    public Sprite SelectiveSprite { get; set; }
    
    /// <summary>
    /// 初始化皮肤选择界面
    /// </summary>
    /// <param name="sprites">皮肤集合</param>
    public void Init(List<Sprite> sprites)
    {
        this.sprites = sprites;
        //EventCenter.AddListener(EventDefine.SelectScript, SelectSprite);
    }

    //加载当前的人物皮肤
    IEnumerator Start () {
        while (sprites == null)
        {
            Debug.Log("皮肤集为空");
            yield return new WaitForEndOfFrame();
        }
        rectTransform = GetComponent<RectTransform>();
        //得到资源管理器
        assetManager = AssetManager.GetAssetManager();
        //得到当前屏幕宽度
        screenWidth = Screen.width;
        rectTransform.sizeDelta = new Vector2(screenWidth - 160, 280);
        //获取预制体
        childPrefab = assetManager.childPrefab;

        //加载所有皮肤
        foreach (Sprite sprite in sprites)
        {
            rectTransform.sizeDelta += new Vector2(160, 0);
            GameObject go = Instantiate(childPrefab, transform);
            //设置皮肤图片
            go.transform.GetChild(0).GetComponent<Image>().sprite = sprite;
            //设置记录格子数量的变量
            itemCount++;
            yield return new WaitForEndOfFrame();
        }
        //皮肤显示
        SetItemSizeAndPos();
    }

    private void Update()
    {
        //当前显示在中间的格子的序号
        int nowIndex = (int)Mathf.Round(rectTransform.localPosition.x / -160f);
        if(nowIndex >= 0 && nowIndex < itemCount)
        {
            index = nowIndex;
        }
        if (Input.GetMouseButtonUp(0))
        {
            SetItemSizeAndPos();
            lastIndex = index;
        }
    }
    /// <summary>
    /// 设置格子大小和居中显示
    /// </summary>
	public void SetItemSizeAndPos()
    {
        transform.DOLocalMoveX(-160 * index, 0.15f);
        transform.GetChild(index).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(160, 160);
        if (lastIndex != index)
        {
            transform.GetChild(lastIndex).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        }
    }

    /// <summary>
    /// 选择皮肤
    /// </summary>
    public void SelectSprite()
    {
        SelectiveSprite = sprites[index];
    }

}
