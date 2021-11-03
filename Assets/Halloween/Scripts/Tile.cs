using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileName {Halloween_Tile1_1, Halloween_Tile1_2, Halloween_Tile1_3, Halloween_Tile1_4, Halloween_Tile1_5, Halloween_Tile1_6,
    Halloween_Incline_Tile1_1, Halloween_Incline_Tile1_2, Halloween_Incline_Tile1_3, Halloween_Incline_Tile1_4, Halloween_Incline_Tile1_5,
    Halloween_Incline_Tile2_1, Halloween_Incline_Tile2_2, Halloween_Incline_Tile2_3, Halloween_Incline_Tile2_4, Halloween_Incline_Tile2_5
} 
// LastIndex = 개수 파악용, Empty = 빈공간

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Sprite[] images; // 타일에 적용될 수 있는 이미지 배열
    private SpriteRenderer spriteRenderer; // 타일 이미지 변경을 위한 SpriteRenderer
    private TileName tileName; // 현재 타일의 이름

    public void Setup(TileName tileName)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        TileName = tileName;
    }

    public TileName TileName
    {
        get => tileName;
        set
        {
            tileName = value;
            spriteRenderer.sprite = images[(int)tileName];
        }
    }

}
