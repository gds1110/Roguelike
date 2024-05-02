using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HUD : UI_Base
{
    

    public HUD_WeaponUI HUD_WeaponUI = null;
    public HUD_Exp HUD_ExpUI = null;   
    public HUD_Score HUD_ScoreUI = null;
    public HUD_Timer HUD_TimerUI = null;
    public override void Init()
    {
        if (HUD_WeaponUI != null)
            HUD_WeaponUI.Init();
    }
 
    // Start is called before the first frame update
    void Start()
    {
        HUD_ExpUI = GetComponentInChildren<HUD_Exp>();
        HUD_ScoreUI = GetComponentInChildren<HUD_Score>();
        HUD_TimerUI = GetComponentInChildren<HUD_Timer>();
        HUD_TimerUI.Init();
        HUD_ExpUI.Init();
        HUD_ScoreUI.Init();
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
