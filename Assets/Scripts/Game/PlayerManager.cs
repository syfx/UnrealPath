using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private AssetManager assetManager;                              //游戏资源管理器
    private Vector3 initPos = new Vector3(0, -1.495f, 0);      //人物称初始位置         
    private DeathEffect playerDeathEffect;                           //主角死亡粒子特效
    private Sprite playerSprite;                                              //主角皮肤
    /// <summary>
    /// 主角控制器
    /// </summary>
    public PlayerController MyPlayerController { get; set; }
    /// <summary>
    /// 当前主角皮肤类型
    /// </summary>
    public PlayerSprite PlayerSpriteType { get; set; }

    private void Awake()
    {
        //添加主角死亡处理监听者
        EventCenter.AddListener(EventDefine.DestroyPlayer, OnPlayerDestroy);
        //得到资源管理器
        assetManager = AssetManager.GetAssetManager();
    }

    /// <summary>
    /// 初始化主角
    /// </summary>
    public void Init()
    {
        if (MyPlayerController == null)
        {
            //得到主角身上的控制器
            MyPlayerController = Instantiate(assetManager.playerPrefab).GetComponent<PlayerController>() ;
        }
        //设置主角皮肤类型
        PlayerSpriteType = GameManager.instance.GameData.NowPlayerSpriteType;
        //设置主角皮肤
        playerSprite = assetManager.playerSpriteSet[PlayerSpriteType];
        //初始化主角
        MyPlayerController.Init(playerSprite, initPos);
    }

    /// <summary>
    /// 主角死亡监听函数
    /// </summary>
    public void OnPlayerDestroy()
    {
        //销毁玩家
        if(playerDeathEffect == null)
        {
            //拿到玩家死亡粒子特效
            GameObject effect = Instantiate(assetManager.deathEffectPrefab);
            playerDeathEffect = effect.GetComponent<DeathEffect>();
        }
        //设置粒子颜色为当前皮肤的主色调颜色
        playerDeathEffect.PlayWithOneColor(assetManager.playerSpriteSet.GetSpriteColor(PlayerSpriteType), MyPlayerController.transform.position, 1);
    }
}
