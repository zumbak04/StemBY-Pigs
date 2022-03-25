using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public abstract class Moving : MonoBehaviour
{
    public Vector2 CellPosition { get; private set; }
    public float speed;

    private ParallelogramGrid grid;
    private bool isMoving = false;
    private Collider2D collider;

    public void AttempMoveBy(int x, int y)
    {
        if (isMoving)
        {
            return;
        }

        var newPosition = CellPosition;
        newPosition += new Vector2(x, y);

        if(CanMove(newPosition))
        {
            MoveTo(newPosition);
        }
    }
    public void MoveTo(Vector2 newPosition)
    {
        isMoving = true;
        CellPosition = newPosition;

        transform.DOMove(grid.CellToWorld(newPosition), speed).OnComplete(StopMoving);
    }

    private void Awake()
    {
        grid = ParallelogramGrid.instance;
        CellPosition = grid.GridStart;
        collider = GetComponent<Collider2D>();

    }
    private void StopMoving()
    {
        isMoving = false;
    }
    private bool CanMove(Vector2 target)
    {
        var worldTarget = grid.CellToWorld(target);
        var path = worldTarget - (Vector2)transform.position;

        RaycastHit2D[] results = new RaycastHit2D[1];
        var hits = collider.Cast(path.normalized, results, path.magnitude);

        return hits == 0;
    }
}
