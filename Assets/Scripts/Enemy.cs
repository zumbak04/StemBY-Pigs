using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Moving
{
    private void Start()
    {
        StartCoroutine(RandomMove());
        // Что бы враги ходили не одновременно
        moveTime += Random.Range(0, moveTime);
    }
    private IEnumerator RandomMove()
    {
        while (true)
        {
            List<Vector2> possibleMoves = new List<Vector2>();

            AddPossibleMove(1, 0, ref possibleMoves);
            AddPossibleMove(-1, 0, ref possibleMoves);
            AddPossibleMove(0, 1, ref possibleMoves);
            AddPossibleMove(0, -1, ref possibleMoves);

            if (possibleMoves.Count == 0)
            {
                yield return new WaitForSeconds(moveTime);
                continue;
            }

            int randomMoveIndex = Random.Range(0, possibleMoves.Count);
            var randomMove = possibleMoves[randomMoveIndex];

            TryMoveBy((int)randomMove.x, (int)randomMove.y);

            yield return new WaitForSeconds(moveTime);
        }
    }
    private void AddPossibleMove(int x, int y, ref List<Vector2> moves)
    {
        if (CanMoveBy(x, y))
        {
            moves.Add(new Vector2(x, y));
        }
    }
}
