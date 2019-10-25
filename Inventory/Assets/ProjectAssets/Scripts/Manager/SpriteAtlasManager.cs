using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteAtlasManager : MonoBehaviour
{
    private static SpriteAtlasManager _instance;

    public static SpriteAtlasManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<SpriteAtlasManager>();
            }

            return _instance;
        }
    }

    public SpriteAtlas itemIconSpriteAtlas;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        LoadAtlasesFromResources();
    }

    public void LoadAtlasesFromResources()
    {
        itemIconSpriteAtlas = Resources.Load<SpriteAtlas>("Atlases/ItemIcon_Atlas_128");
    }

    public Sprite GetSprite (string spriteName)
    {
        Sprite icon = itemIconSpriteAtlas.GetSprite(spriteName);

        return icon;
    }
}
