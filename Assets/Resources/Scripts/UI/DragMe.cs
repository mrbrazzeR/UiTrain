using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragMe : MonoBehaviour, IBeginDragHandler, IEndDragHandler,IDragHandler
{
    public Vector3 startPosition;
    void Start()
    {
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        UpdatePosition();
    }
    public void OnDrag(PointerEventData eventData)
    {
        UpdatePosition();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        
    }

    void UpdatePosition()
    {
        var mousePosition = Input.mousePosition;
        transform.position = mousePosition;
    }
}
