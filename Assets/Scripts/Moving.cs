using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public abstract class Moving : MonoBehaviour
{
    public Vector2 CellPosition { get; private set; }
    public float moveTime;

    private bool isMoving = false;
    private Collider2D coldr;
    private Animator animator;
    private Vector2 origColOffset;

    private ParallelogramGrid Grid => ParallelogramGrid.instance;

    public UnityEvent moveComplete;

    public bool TryMoveBy(int x, int y)
    {
        if (isMoving)
        {
            return false;
        }
        if(!CanMoveBy(x, y))
        {
            return false;
        }

        animator.SetInteger("Vertical", x);
        animator.SetInteger("Horizontal", y);

        var newPosition = CellPosition;
        newPosition += new Vector2(x, y);
        MoveTo(newPosition);

        return true;
    }
    public void MoveTo(Vector2 newPosition)
    {
        isMoving = true;
        CellPosition = newPosition;
        var newWorldPosition = Grid.CellToWorld(newPosition);
        var path = newWorldPosition - (Vector2)transform.position;
        coldr.offset += path;
        transform.DOMove(newWorldPosition, moveTime).OnComplete(() => moveComplete.Invoke());
    }

    protected void Awake()
    {
        CellPosition = Grid.GridStart;
        coldr = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
        origColOffset = coldr.offset;

        moveComplete.AddListener(StopMoving);
    }
    protected void StopMoving()
    {
        isMoving = false;
        coldr.offset = origColOffset;
    }
    protected bool CanMoveBy(int x, int y)
    {
        var target = CellPosition;
        target += new Vector2(x, y);

        var worldTarget = Grid.CellToWorld(target);
        var path = worldTarget - (Vector2)transform.position;

        RaycastHit2D[] results = new RaycastHit2D[1];
        var hits = coldr.Cast(path.normalized, results, path.magnitude);

        return hits == 0;
    }
}
