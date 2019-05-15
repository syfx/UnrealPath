using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

    private AssetManager assetManager;                              //游戏资源管理器
    private Vector3 initPos = new Vector3(0, -1.495f, 0);      //人物称初始位置         
    private DeathEffect playerDeathEffect;                           //主角死亡粒子特效
    private Sprite playerSprite;                                              //主角皮肤
    private PlayerSprite playerSpriteType;                            //主角皮肤类型
    /// <summary>
    /// 主角控制器
    /// </summary>
    public PlayerController MyPlayerController { get; set; }
   
    private void Awake()
    {
        //得到资源管理器
        assetManager = AssetManager.GetAssetManager();
    }
    private void OnEnable()
    {
        //添加主角死亡处理监听者
        EventCenter.AddListener(EventDefine.PlayerDeath, OnPlayerDestroy);
    }
    /// <summary>
    /// 初始化主角
    /// </summary>
    public void Init()
    {
        if(gameObject.activeSelf == false)
        {
            gameObject.SetActive(true);
        }
        if (MyPlayerController == null)
        {
            //得到主角身上的控制器
            MyPlayerController = Instantiate(assetManager.playerPrefab, transform).GetComponent<PlayerController>() ;
        }
        //设置主角皮肤
        playerSprite = GameManager.instance.PlayerSkin;
        playerSpriteType = assetManager.playerSpriteSet[playerSprite];
        //初始化主角
        MyPlayerController.Init(playerSprite, initPos);
    }
    /// <summary>
    /// 主动杀死主角
    /// </summary>
    public void DeadPlayer()
    {
        //销毁主角
        MyPlayerController.gameObject.SetActive(false);
        //禁用特效
        if (playerDeathEffect != null)
        {
            playerDeathEffect.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 主角死亡监听函数
    /// </summary>
    public void OnPlayerDestroy()
    {
        //获取玩家死亡特效
        if(playerDeathEffect == null)
        {
            //拿到玩家死亡粒子特效
            GameObject effect = Instantiate(assetManager.deathEffectPrefab, transform);
            playerDeathEffect = effect.GetComponent<DeathEffect>();
        }
        //设置粒子颜色为当前皮肤的主色调颜色
        playerDeathEffect.PlayWithOneColor(assetManager.playerSpriteSet.GetSpriteColor(playerSpriteType), MyPlayerController.transform.position, 1);
    }
    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.PlayerDeath, OnPlayerDestroy);
    }
}
