using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager 
{
    int _order = 10;

    Stack<UI_Popup> _popupStack = new Stack< UI_Popup>();
    UI_Scene _SceneUI = null;
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if(root==null)
                root = new GameObject { name = "@UI_Root" };
            return root;

        }
    }
    public void SetCanvas(GameObject go,bool sort=true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if(sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }


    }

    public T MakeSubItem<T>(Transform parent = null, string name =null) where T : UI_Base
    {
        if(string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/SubItem/{name}");

        if(parent!= null)
        {
            go.transform.SetParent(parent);
        }

        return Util.GetOrAddComponent<T>(go);       

    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }
        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}");

        T SceneUI = Util.GetOrAddComponent<T>(go);
        _SceneUI = SceneUI;

        go.transform.SetParent(Root.transform);


        return SceneUI;
    }

    public T ShowPopupUI<T>(string name =null) where T : UI_Popup
    {
        if (string.IsNullOrEmpty(name))
        {
            name = typeof(T).Name;
        }
        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{name}");

        T Popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push( Popup );

       
        go.transform.SetParent(Root.transform);

        return Popup;
    }
    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
        {
            return;
        }

        if(_popupStack.Peek() != popup) 
        {
            Debug.Log("Close Popup Faild");
            return;
        }
        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if(_popupStack.Count == 0 )
        {
            return;
        }

        UI_Popup popup = _popupStack.Pop();
        Managers.Resource.Destroy(popup.gameObject);
        popup = null;

        _order--;
    }

    public void Clear()
    {
        ClosePopupUI();
        _SceneUI = null;
    }
}
