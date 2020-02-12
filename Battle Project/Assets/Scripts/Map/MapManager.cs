using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    // Array 2D che tiene il riferimento ai prefab
    GameObject[,] tilesOnMap;

    [Header("Map Settings:")]
    public int mapSizeX;
    public int mapSizeY;
    public GameObject tileContainer;

    [Header("Prefab of Tiles:")]
    public Tile[] prefabTiles;
    // Per tenere il riferimento delle coordinate
    public int[,] tiles;

    public enum TileType
    {
        TILE_GRASS = 0,
        TILE_WATER = 1,
        TILE_TREE = 2,
    }

    private void Start()
    {
        GenerateMapInfo();
        GenerateMapGraphics();
    }

    // Genera il riferimento alla mappa
    private void GenerateMapInfo()
    {
        tiles = new int[mapSizeX, mapSizeY];
        // Crea una mappa con tutti tiles di tipo grass
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                tiles[x, y] = (int)TileType.TILE_GRASS;
            }
        }
        // Mette il primo tile come water, necessità creazione layout
        tiles[0,0] = (int)TileType.TILE_WATER;
        tiles[2, 3] = (int)TileType.TILE_TREE;
    }

    private void GenerateMapGraphics()
    {
        tilesOnMap = new GameObject[mapSizeX, mapSizeY];
        int index;
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                index = tiles[x, y];
                GameObject newTile = Instantiate(prefabTiles[index].tilePrefab, new Vector3(x, 0, y), Quaternion.identity);
                newTile.transform.SetParent(tileContainer.transform);
                tilesOnMap[x, y] = newTile;
            }
        }
    }
}
