using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CrossHair : UI_Scene
{

    enum Images
    {
        Aim
    }
    public override void Init()
    {
        base.Init();

        Bind<Image>(typeof(Images));
    }
  
}
