using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EventHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerClickHandler
{

    public  Action<PointerEventData> OnBeginDragHandler = null;
    public  Action<PointerEventData> OnClickHandler = null;
    public  Action<PointerEventData> OnDragHandler = null;



    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
        {
            OnClickHandler.Invoke(eventData);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        if(OnBeginDragHandler!= null)
        {
            OnBeginDragHandler.Invoke(eventData);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {

        Debug.Log("OnDrag");
        if (OnDragHandler != null)
        {
            OnDragHandler.Invoke(eventData);
        }

    }


}
