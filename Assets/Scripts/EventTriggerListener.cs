using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventTriggerListener : MonoBehaviour,
    IEventSystemHandler,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IPointerClickHandler,
    IUpdateSelectedHandler,
    ISelectHandler,
    IDragHandler,
    IEndDragHandler,
    IDropHandler,
    IBeginDragHandler
{

    public delegate void VoidDelegate();

    private VoidDelegate m_onPointUp;
    public VoidDelegate onPointUp
    {
        get { return m_onPointUp; }
        set { m_onPointUp = value; }
    }

    private VoidDelegate m_onBeginDrag;
    public VoidDelegate onBeginDrag
    {
        get { return m_onBeginDrag; }
        set { m_onBeginDrag = value; }
    }

    private VoidDelegate m_onDrag;
    public VoidDelegate onDrag
    {
        get { return m_onDrag; }
        set { m_onDrag = value; }
    }

    private VoidDelegate m_onDrop;
    public VoidDelegate onDrop
    {
        get { return m_onDrop; }
        set { m_onDrop = value; }
    }

    private VoidDelegate m_onEndDrag;
    public VoidDelegate onEndDrag
    {
        get { return m_onEndDrag; }
        set { m_onEndDrag = value; }
    }

    private VoidDelegate m_onPointerClick;
    public VoidDelegate onPointerClick
    {
        get { return m_onPointerClick; }
        set { m_onPointerClick = value; }
    }

    private VoidDelegate m_onPointerDown;
    public VoidDelegate onPointerDown
    {
        get { return m_onPointerDown; }
        set { m_onPointerDown = value; }
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDrag != null)
        {
            onBeginDrag();
        }
        else
        {
            if (gameObject.transform.parent != null)
            {
                var go = ExecuteEvents.GetEventHandler<IBeginDragHandler>(gameObject.transform.parent.gameObject);
                if (go != null)
                {
                    ExecuteEvents.Execute(go, eventData, ExecuteEvents.beginDragHandler);
                }
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        if(onDrag != null)
        {
            onDrag();
        }
        else
        {
            if(gameObject.transform.parent != null)
            {
                var go = ExecuteEvents.GetEventHandler<IDragHandler>(gameObject.transform.parent.gameObject);
                if(go != null)
                {
                    ExecuteEvents.Execute(go, eventData, ExecuteEvents.dragHandler);
                }
            }
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (onDrop != null)
        {
            onDrop();
        }
        else
        {
            if (gameObject.transform.parent != null)
            {
                var go = ExecuteEvents.GetEventHandler<IDropHandler>(gameObject.transform.parent.gameObject);
                if (go != null)
                {
                    ExecuteEvents.Execute(go, eventData, ExecuteEvents.dropHandler);
                }
            }
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (onEndDrag != null)
        {
            onEndDrag();
        }
        else
        {
            if (gameObject.transform.parent != null)
            {
                var go = ExecuteEvents.GetEventHandler<IEndDragHandler>(gameObject.transform.parent.gameObject);
                if (go != null)
                {
                    ExecuteEvents.Execute(go, eventData, ExecuteEvents.endDragHandler);
                }
            }
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if(onPointerClick != null)
            onPointerClick.Invoke();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (onPointerDown != null)
            onPointerDown.Invoke();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
    public void OnPointerExit(PointerEventData eventData)
    {
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(onPointUp != null)
            onPointUp.Invoke();
    }
    public void OnSelect(BaseEventData eventData)
    {

    }
    public void OnUpdateSelected(BaseEventData eventData)
    {

    }

    private void OnDestroy()
    {
        onPointUp = null;
    }
}
