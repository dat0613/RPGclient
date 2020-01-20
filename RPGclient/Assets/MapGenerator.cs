using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    enum Tile
    {
        Grass,
        Water,
        Trap,
        Bridge,
        BlueButton,
        RedButton,
        WayPoint
    };

    public float multiple = 1.0f;
    const int xSize = 20;
    const int ySize = 20;
    GameObject tileMapParent = null;

    public List<GameObject> tilePrefabList;

    int[,] map = new int[ySize, xSize]
    {
        { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
        { 0, 4, 0, 1, 1, 1, 1, 0, 5, 0, 0, 4, 0, 1, 1, 1, 1, 0, 5, 0 },
        { 0, 0, 0, 1, 1, 1, 1, 0, 2, 0, 0, 0, 0, 1, 1, 1, 1, 0, 2, 0 },
        { 0, 2, 0, 1, 1, 1, 1, 0, 0, 0, 0, 2, 0, 1, 1, 1, 1, 0, 0, 0 },
        { 0, 0, 0, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 0, 0, 0 },
        { 0, 0, 0, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 0, 0, 0 },
        { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
        { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
        { 0, 2, 0, 1, 1, 1, 1, 0, 2, 0, 0, 2, 0, 1, 1, 1, 1, 0, 2, 0 },
        { 0, 0, 0, 1, 1, 1, 1, 0, 0, 6, 0, 0, 0, 1, 1, 1, 1, 0, 0, 6 },
        { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
        { 0, 4, 0, 1, 1, 1, 1, 0, 5, 0, 0, 4, 0, 1, 1, 1, 1, 0, 5, 0 },
        { 0, 0, 0, 1, 1, 1, 1, 0, 2, 0, 0, 0, 0, 1, 1, 1, 1, 0, 2, 0 },
        { 0, 2, 0, 1, 1, 1, 1, 0, 0, 0, 0, 2, 0, 1, 1, 1, 1, 0, 0, 0 },
        { 0, 0, 0, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 0, 0, 0 },
        { 0, 0, 0, 3, 3, 3, 3, 0, 0, 0, 0, 0, 0, 3, 3, 3, 3, 0, 0, 0 },
        { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
        { 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0 },
        { 0, 2, 0, 1, 1, 1, 1, 0, 2, 0, 0, 2, 0, 1, 1, 1, 1, 0, 2, 0 },
        { 0, 0, 0, 1, 1, 1, 1, 0, 0, 6, 0, 0, 0, 1, 1, 1, 1, 0, 0, 6 }
    };

    GameObject GetTilePrefab(Tile tile)
    {
        int tileNumber = (int)tile;
        if(0 > tileNumber || tileNumber >= tilePrefabList.Count)
            return null;

        return tilePrefabList[tileNumber];
    }

    void GenerateTile(int x, int y)
    {
        Tile tile = (Tile)map[y, x];

        GenerateTile(x, y, tile);

        switch (tile)
        {
            case Tile.Trap:// 함정, 버튼 등의 아래에는 풀이 깔려있음
            case Tile.BlueButton:
            case Tile.RedButton:
            case Tile.WayPoint:
                GenerateTile(x, y, Tile.Grass);
                break;

            case Tile.Bridge:// 다리 아래에는 물이 있음
                GenerateTile(x, y, Tile.Water);
                break;

            default:

                break;
        }
    }

    void GenerateTile(int x, int y, Tile tile)
    {
        var prefab = GetTilePrefab(tile);
        if(prefab == null)
            return;

        var tileObject = Instantiate(prefab, new Vector3((float)x - (float)xSize * 0.5f, 0.0f, (float)y - (float)ySize * 0.5f) * multiple, Quaternion.identity);
        tileObject.transform.localScale = new Vector3(multiple, multiple, multiple);
        tileObject.gameObject.isStatic = true;

        if(tileMapParent != null)
            tileObject.transform.SetParent(tileMapParent.transform);

        if(tile != Tile.WayPoint)
            return;

        for(int i = 0; i < tileObject.transform.childCount; i++)
        {
            var childObject = tileObject.transform.GetChild(i);
            childObject.transform.localScale = new Vector3(multiple, multiple, multiple);
        }

    }

    void Awake()
    {
        tileMapParent = tileMapParent ?? new GameObject("TileMapParent");
        tileMapParent.gameObject.isStatic = true;
        
        for(int y = 0; y < ySize; y++)
        {
            for(int x = 0; x < xSize; x++)
            {
                GenerateTile(x, y);
            }
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
