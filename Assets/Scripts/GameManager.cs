using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManager : MonoBehaviour
{
    #region Singelaton

    private static GameManager mInstance;

    public static GameManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = (GameManager)FindObjectOfType(typeof(GameManager));
            }

            if (mInstance == null)
            {
                Debug.LogError("GameManagar instance is null and not found gamemager on to the scene ! Please add Gamemanager to scene.");
            }
            return mInstance;
        }
    }

    #endregion

    [SerializeField] private Transform Enemy;
    [SerializeField] private Transform Player;
    [SerializeField] private Tilemap TileMap;

    [SerializeField] private List<Vector3> tilePositions;
    [SerializeField] private int index;
    [SerializeField] private TileBase tileBase;

    private void Awake()
    {
        Cursor.visible = false;
        TestAStar();
        Enemy.transform.position = tilePositions[index];
        Vector3Int position = new Vector3Int((int)tilePositions[index].x, (int)tilePositions[index].y, (int)tilePositions[index].z);
        TileMap.SetTile(position, tileBase);
        StartCoroutine(TestMove());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            index++;
            Vector3Int position = new Vector3Int((int)tilePositions[index].x, (int)tilePositions[index].y, (int)tilePositions[index].z);
            Enemy.transform.position = tilePositions[index];
            TileMap.SetTile(position, tileBase);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            index--;
            Vector3Int position = new Vector3Int((int)tilePositions[index].x, (int)tilePositions[index].y, (int)tilePositions[index].z);
            Enemy.transform.position = tilePositions[index];
            TileMap.SetTile(position, tileBase);
        }
    }

    private void TestAStar()
    {
        tilePositions = new List<Vector3>();
        BoundsInt bounds = TileMap.cellBounds;
        TileBase[] allTiles = TileMap.GetTilesBlock(bounds);

        foreach (var pos in TileMap.cellBounds.allPositionsWithin)
        {
            Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 place = TileMap.WorldToCell(localPlace) + new Vector3(0.5f, 0.5f, 0.0f);
            if (TileMap.HasTile(localPlace))
            {
                tilePositions.Add(place);
            }
        }
    }

    private IEnumerator TestMove()
    {
        foreach (Vector3 position in tilePositions)
        {
            yield return new WaitForSeconds(0.2f);
            Enemy.transform.position = position;
            //TileMap.SetTile(new Vector3Int((int)(position.x-0.5f), (int)(position.y - 0.5f), 0), tileBase);
        }
    }

}
