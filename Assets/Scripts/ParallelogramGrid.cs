using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Не реализую интерфейс GridLayout так как хочу его упростить
public class ParallelogramGrid : MonoBehaviour
{
    #region Public Fields
    public float gridSize = 10;
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
    private Vector3 GridStart
    {
        get => gameObject.transform.position;
    }
    #endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;

        for (int i = 0; i <= gridSize; i++)
        {
            Gizmos.DrawRay(GridStart + Rotation * Vector3.up * cellSides * i, Vector3.right * gridSize * cellTopAndBottomSide);
            Gizmos.DrawRay(GridStart + Vector3.right * cellTopAndBottomSide * i, Rotation * Vector3.up * gridSize * cellSides);
        }

        Gizmos.DrawSphere(CellToWorld(new Vector3(2,3,0)), 0.1f);
    }

    public Vector3 CellToWorld(Vector3 cellPosition)
    {
        Vector3 worldPosition = Rotation * Vector3.up * cellSides * cellPosition.x + GridStart + Vector3.right * cellTopAndBottomSide * cellPosition.y;

        return worldPosition;
    }
}
