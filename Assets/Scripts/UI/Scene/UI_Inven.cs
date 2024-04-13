using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inven :UI_Scene
{
    enum GameObjects
    {
        GridPanel
    }

    private void Start()
    {
        
    }

    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObject));

        GameObject gridPanel = Get<GameObject>((int)GameObjects.GridPanel);
        foreach(Transform child in gridPanel.transform)
        {
            Managers.Resource.Destroy(child.gameObject);
        }

        //for(int i=0;i<8;i++)
        //{
        //    GameObject item = Managers.UI.MakeSubItem<UI_Inven_Item>(gridPanel.trasform).gameObject;


        //    Util.GetOrAddComponent<UI_Inven_Item>(item);
        // UI_Inven_Item invenItem = item.GetOrAddComponent<UI_Inven_Item>(item);
        //}
    }

}
