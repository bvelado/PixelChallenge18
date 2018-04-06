using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour {

    private const float TILE_SIZE = 6.70f;
    private const int MAP_WIDTH = 9;
    private const int MAP_HEIGHT = 9;
    private const int CHUNK_WIDTH = 3;
    private const int CHUNK_HEIGHT = 3;

    [Header("Player Data")]
    [SerializeField] private PlayerData[] _playerDatas;

    [Header("Prefabs")]
    [SerializeField] private GameObject _bucketTilePrefab;
    [SerializeField] private GameObject _gardenTilePrefab;
    [SerializeField] private GameObject _holeTilePrefab;

    [SerializeField] private Transform _mapContainer;

    private void Awake()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        _mapContainer.position = - new Vector3(MAP_WIDTH * CHUNK_WIDTH, 0f, MAP_HEIGHT * CHUNK_HEIGHT);

        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                GenerateChunk(x, y);
            }
        }
    }

    private void GenerateChunk(int mapX, int mapY)
    {
        var chunkContainer = new GameObject(string.Format("Chunk [{0};{1}]", mapX, mapY)).transform;
        chunkContainer.SetParent(_mapContainer, false);
        chunkContainer.localPosition = new Vector3(mapX * 3 * TILE_SIZE, 0f, mapY * 3 * TILE_SIZE);

        var pool = GenerateSpawnPool(mapX == 1 && mapY == 1, chunkContainer);

        for (int chunkX = 0; chunkX < 3; chunkX++)
        {
            for (int chunkY = 0; chunkY < 3; chunkY++)
            {
                var tile = pool[Random.Range(0, pool.Count)];
                pool.Remove(tile);

                tile.transform.localPosition = new Vector3(chunkX * TILE_SIZE, 0f, chunkY * TILE_SIZE);
            }
        }
    }

    private List<GameObject> GenerateSpawnPool(bool isBucketChunk, Transform chunkContainer)
    {
        var pool = new List<GameObject>();
        for (int i = 0; i < 8; i++)
        {
            var gardenTile = Instantiate(_gardenTilePrefab, chunkContainer);
            var vegetable = gardenTile.GetComponentInChildren<Vegetable>();
            vegetable.Initialize(_playerDatas[i/2]);
            pool.Add(gardenTile);
        }
        var lastTile = Instantiate(isBucketChunk ? _bucketTilePrefab : _holeTilePrefab, chunkContainer);
        pool.Add(lastTile);

        return pool;
    }
}
