using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HUD : UI_Base
{

    public HUD_WeaponUI HUD_WeaponUI = null;

    public override void Init()
    {
        if (HUD_WeaponUI != null)
            HUD_WeaponUI.Init();
    }

    // Start is called before the first frame update
    void Start()
    {
        HUD_WeaponUI = GetComponentInChildren<HUD_WeaponUI>();
        Managers.Game._ui_HUD = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
