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
    }
    public void InitWeaponUI(WeaponManager weapon)
    {

        HUD_WeaponUI = GetComponentInChildren<HUD_WeaponUI>();
        if (HUD_WeaponUI != null)
        {
            HUD_WeaponUI.SetWeaponManager(weapon);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
