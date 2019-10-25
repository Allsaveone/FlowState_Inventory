using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class SpriteAtlasTest : MonoBehaviour
{
    public SpriteAtlas atlas;
    public Image image;

    void Start()
    {
        Sprite icon = atlas.GetSprite("World_Gate");
        image.sprite = icon;
    }

}
