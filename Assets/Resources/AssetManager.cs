using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AssetManager")]
public class AssetManager : ScriptableObject {

    public static AssetManager GetAssetManager()
    {
        return Resources.Load<AssetManager>("MyAssetManager");
    }
    /// <summary>
    /// 游戏背景皮肤集合
    /// </summary>
    [Serializable]
    public class BgSpriteSet
    {
        public List<BgSprite> spriteType = new List<BgSprite>();
        public List<Sprite> sprite = new List<Sprite>();

        public Sprite this[BgSprite type]
        {
            get
            {
                int index = spriteType.IndexOf(type);
                if (index >= 0 && index < sprite.Count)
                {
                    return sprite[index];
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    spriteType.Add(type);
                    sprite.Add(value);
                }
            }
        }
    }
    /// <summary>
    /// 平台背景皮肤集合
    /// </summary>
    [Serializable]
    public class PlatformSpriteSet
    {
        public List<PlatformSprite> spriteType = new List<PlatformSprite>();
        public List<Sprite> sprite = new List<Sprite>();

        public Sprite this[PlatformSprite type]
        {
            get
            {
                int index = spriteType.IndexOf(type);
                if (index >= 0 && index < sprite.Count)
                {
                    return sprite[index];
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    spriteType.Add(type);
                    sprite.Add(value);
                }
            }
        }
    }
    /// <summary>
    /// 主角皮肤集合
    /// </summary>
    [Serializable]
    public class PlayerSpriteSet
    {
        public List<PlayerSprite> spriteType = new List<PlayerSprite>();
        public List<Sprite> sprite = new List<Sprite>();

        public Sprite this[PlayerSprite type]
        {
            get
            {
                int index = spriteType.IndexOf(type);
                if(index >= 0 && index < sprite.Count)
                {
                    return sprite[index];
                }
                return null;
            }
            set
            {
                if(value != null)
                {
                    spriteType.Add(type);
                    sprite.Add(value);
                }
            }
        }
        //public PlayerSprite spriteType;
        //public Sprite sprite;
    }

    [Tooltip("背景皮肤列表")]
    public BgSpriteSet bgSpriteSet = new BgSpriteSet();
    [Tooltip("平台皮肤列表")]
    public PlatformSpriteSet platformSpriteSet = new PlatformSpriteSet();
    [Tooltip("玩家皮肤列表")]
    public PlayerSpriteSet playerSpriteSet = new PlayerSpriteSet();

    [Tooltip("玩家预制")]
    public GameObject playerPrefab;
}
