using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Vector2 stoneOffset;
    public int stoneGap;
    public Vector2 stonesRowsByColumns;

    public Vector2 playerSpawn;

    void Start()
    {
        SpawnStones();
    }
    private void SpawnStones()
    {
        Vector3 spawnLocation = Vector3.zero;
        for (int i = 0; i < stonesRowsByColumns.x; i++)
        {
            for (int j = 0; j < stonesRowsByColumns.y; j++)
            {
                spawnLocation.x = stoneOffset.x + i * stoneGap;
                spawnLocation.y = stoneOffset.y + j * stoneGap;
                spawnLocation = ParallelogramGrid.instance.CellToWorld(spawnLocation);
                Instantiate(GameAssets.instance.stone, spawnLocation, Quaternion.identity);
            }
        }
    }
    private void SpawnPlayer()
    {
        Vector3 spawnLocation = Vector3.zero;
    }
}
