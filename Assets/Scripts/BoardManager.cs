using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Vector2 stoneOffset;
    public int stoneGap;
    public Vector2 stonesRowsByColumns;

    public Vector2 playerSpawn;

    private ParallelogramGrid grid = ParallelogramGrid.instance;

    void Start()
    {
        grid = ParallelogramGrid.instance;

        SpawnStones();
        SpawnPlayer();
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
                spawnLocation = grid.CellToWorld(spawnLocation);
                Instantiate(GameAssets.instance.stone, spawnLocation, Quaternion.identity);
            }
        }
    }
    private void SpawnPlayer()
    {
        var player = Instantiate(GameAssets.instance.player, grid.CellToWorld(playerSpawn), Quaternion.identity);
        player.MoveTo(playerSpawn);
    }
}
