using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AssetManager")]
public class AssetManager : ScriptableObject {

    public static AssetManager GetAssetManager()
    {
        return Resources.Load<AssetManager>("MyAssetManager");
    }

    [Tooltip("背景皮肤列表")]
    public List<Sprite> bgRender = new List<Sprite>();
    [Tooltip("平台皮肤列表")]
    public List<Sprite> platformRender = new List<Sprite>();
}
