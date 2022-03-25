using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Moving
{
    private void Update()
    {
        var horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        var vertical = (int)(Input.GetAxisRaw("Vertical"));

        if (horizontal != 0)
            vertical = 0;

        if (horizontal != 0 || vertical != 0)
        {
            AttempMoveBy(vertical, horizontal);
        }
    }
}
