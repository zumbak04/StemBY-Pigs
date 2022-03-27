using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Vector2 stoneOffset;
    public int stoneGap;
    public Vector2 stonesRowsByColumns;
    public Vector2 playerSpawn;
    public int enemiesNumber;

    private ParallelogramGrid Grid => ParallelogramGrid.instance;

    void Start()
    {
        SpawnStones();
        SpawnPlayer();
        SpawnEnemies();
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
                spawnLocation = Grid.CellToWorld(spawnLocation);
                Instantiate(GameAssets.instance.stone, spawnLocation, Quaternion.identity);
            }
        }
    }
    private void SpawnPlayer()
    {
        var player = Instantiate(GameAssets.instance.player, Grid.CellToWorld(playerSpawn), Quaternion.identity);
        player.MoveTo(playerSpawn);
    }
    private void SpawnEnemies()
    {
        List<Vector2> possibleSpawns = new List<Vector2>();

        for(int i = 0; i < Grid.gridXSize; i++)
        {
            for (int j = 0; j < Grid.gridYSize; j++)
            {
                Vector2 cell = new Vector2(i, j + 0.5f);
                if (IsCellFree(cell))
                {
                    possibleSpawns.Add(cell);
                }
            }
        }
        for(int i = 0; i < enemiesNumber; i++)
        {
            var spawnIndex = Random.Range(0, possibleSpawns.Count);
            var spawnPoint = possibleSpawns[spawnIndex];
            possibleSpawns.RemoveAt(spawnIndex);
            SpawnDog(spawnPoint);
        }
    }
    private bool IsCellFree(Vector2 cell)
    {
        var hit = Physics2D.OverlapCircle(Grid.CellToWorld(cell), 0.1f);

        return hit == null;
    }
    private void SpawnDog(Vector2 position)
    {
        var dog = Instantiate(GameAssets.instance.dog, Grid.CellToWorld(position), Quaternion.identity);
        dog.MoveTo(position);
    }
}
