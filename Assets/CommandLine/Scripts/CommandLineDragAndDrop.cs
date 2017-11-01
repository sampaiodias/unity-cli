using UnityEngine;
using UnityEngine.EventSystems;

public class CommandLineDragAndDrop : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public Transform target;
    public CommandLineCore cliCore;
    Vector3 dist;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (cliCore.draggableWindow)
        {
            dist = target.position - Input.mousePosition;
        }        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (cliCore.draggableWindow)
        {
            target.position = Input.mousePosition + dist;
        }
    }
}
