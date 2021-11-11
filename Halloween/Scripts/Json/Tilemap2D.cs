using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;

public class Tilemap2D : MonoBehaviour
{
    [Header("Tile")]
    [SerializeField]
    private GameObject tilePrefab;
    public void GenerateTilemap(string mapData)
    {
        int width = 20;
        int height = 30;
        int index = 0;
        string strReturnData = mapData;
        JObject root = JObject.Parse(strReturnData);
        JToken arr_data = root["Tiles"];
        JArray Tile_array = (JArray)arr_data;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                for(index = 0; index < arr_data.Count(); index++)
                {
                    if(Convert.ToInt32(Tile_array[index]["X"]) == x && Convert.ToInt32(Tile_array[index]["Y"]) == y)
                    {
                        Vector3 position = new Vector3(-(width * 0.5f + 0.5f) + (x * 0.5f), (height * 0.5f - 0.5f) - (y * 0.5f), 4);

                        SpawnTile((TileName)Enum.Parse(typeof(TileName),Tile_array[index]["TileNum"].ToString()), position);
                    }
                }
            }
        }
    }

    private void SpawnTile(TileName tilename, Vector3 position)
    {
        GameObject clone = Instantiate(tilePrefab, position, Quaternion.identity);

        clone.name = "Tile"; // Tile 오브젝트의 이름을 "Tile"로 설정
        clone.transform.SetParent(transform); // Tilemap2D 오브젝트를 Tile 오브젝트의 부모로 설정

        Tile tile = clone.GetComponent<Tile>();
        tile.Setup(tilename);
    }
}

// 각자 형태로 배치된 타일들을 왼쪽 상단부터 순차적으로 번호를 부여
// 0, 1, 2, 3, 4, 5, 6, 7, 8,...
// 6, 7.....
/*
int index = y * width + x;

if (mapData.mapData[index] == (int)TileName.Empty)
{
    continue;
}

// 생성되는 타일맵의 중앙이 (0, 0, 0)인 위치
Vector3 position = new Vector3(-(width * 0.5f - 0.5f) + x, (height * 0.5f - 0.5f) - y);

// 현재 index의 맵 정보가 TileName.Empty(0)보다 크고, TileName.LastIndex(17)보다 작으면
if (mapData.mapData[index] > (int)TileName.Empty && mapData.mapData[index] < (int)TileName.LastIndex)
{
    // 타일 생성
    SpawnTile((TileName)mapData.mapData[index], position);
}
*/
