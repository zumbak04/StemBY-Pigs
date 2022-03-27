using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Не реализую интерфейс GridLayout так как хочу его упростить
public class ParallelogramGrid : MonoBehaviour
{
    #region Public Fields
    public static ParallelogramGrid instance = null;
    public float gridXSize = 10;
    public float gridYSize = 10;
    public float cellTopAndBottomSide = 2;
    public float cellSides = 3;
    public float cellAngle = 15;
    #endregion

    #region Properties
    private Quaternion Rotation
    {
        get => Quaternion.AngleAxis(cellAngle, Vector3.forward);
    }
    private Quaternion ReverseRotation
    {
        get => Quaternion.AngleAxis(cellAngle, Vector3.back);
    }
    public Vector2 GridStart
    {
        get => gameObject.transform.position;
    }
    #endregion

    #region Public Methods
    public Vector2 CellToWorld(Vector2 cellPosition)
    {
        Vector2 worldPositionX = Rotation * Vector2.up * cellSides * cellPosition.x;
        Vector2 worldPositionY = Vector2.right * cellTopAndBottomSide * cellPosition.y;

        return worldPositionX + worldPositionY + GridStart;
    }
    #endregion

    #region Private Methods
    private void Awake()
    {
        instance = this;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;

        for (int i = 0; i <= gridXSize; i++)
        {
            Gizmos.DrawRay(GridStart + (Vector2)(Rotation * Vector2.up * cellSides * i), Vector2.right * gridYSize * cellTopAndBottomSide);
        }
        for (int i = 0; i <= gridYSize; i++)
        {
            Gizmos.DrawRay(GridStart + Vector2.right * cellTopAndBottomSide * i, Rotation * Vector2.up * gridXSize * cellSides);
        }

        Gizmos.DrawSphere(CellToWorld(new Vector3(2,3,0)), 0.1f);
    }
    #endregion
}
