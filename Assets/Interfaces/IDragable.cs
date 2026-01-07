using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragable 
{
    public void OnDragStart();
    public void OnDragging(Vector2 position);
    public void OnDragEnd();
}
