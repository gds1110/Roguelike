using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD_WeaponUI : UI_Base
{
    enum GameObjects
    {
        CurrentWeaponImg,
        SpareWeaponImg,
        AmmoText,
        RotateImg,
    }

    public WeaponBased _currentWeapon;
    public WeaponManager wm;
    public override void Init()
    {

        wm = Managers.Game.GetPlayer().GetComponent<WeaponManager>();
        if (wm)
        {
            wm._AshootWeapon -= FiredAmmo;
            wm._AshootWeapon += FiredAmmo;
            wm._AchangeWeapon -= ChangeWeaponData;
            wm._AchangeWeapon += ChangeWeaponData;

            _currentWeapon = wm._currentWeapon;
            Get<GameObject>((int)GameObjects.CurrentWeaponImg).GetComponent<Image>().sprite = wm._currentWeapon._weaponImage;
            Get<GameObject>((int)GameObjects.SpareWeaponImg).GetComponent<Image>().sprite = wm._spareWeapon._weaponImage;
            Get<GameObject>((int)GameObjects.AmmoText).GetComponent<Text>().text = $"{_currentWeapon._currentAmmo} / {_currentWeapon._ammoSize}";
        }
        else
        {
            Debug.Log("No WeaponManager");
        }
    }
    public void SetWeaponManager(WeaponManager weaponManager)
    {
        wm = weaponManager;
        if (wm)
        {
            wm._AshootWeapon -= FiredAmmo;
            wm._AshootWeapon += FiredAmmo;
            wm._AchangeWeapon -= ChangeWeaponData;
            wm._AchangeWeapon += ChangeWeaponData;

            _currentWeapon = wm._currentWeapon;
            GetImage((int)GameObjects.CurrentWeaponImg).sprite = wm._currentWeapon._weaponImage;
            GetImage((int)GameObjects.SpareWeaponImg).sprite = wm._spareWeapon._weaponImage;
            GetText((int)GameObjects.AmmoText).text = $"{_currentWeapon._currentAmmo} / {_currentWeapon._ammoSize}";
        }
        else
        {
            Debug.Log("No WeaponManager");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //   Init();

        Bind<GameObject>(typeof(GameObjects));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    void ChangeWeaponData(WeaponBased changeWeapon)
    {

        StartCoroutine(RotateImg(0.5f)); 
        if (_currentWeapon)
        {
            Get<GameObject>((int)GameObjects.SpareWeaponImg).GetComponent<Image>().sprite = _currentWeapon._weaponImage;
        }
        _currentWeapon = changeWeapon;
        Get<GameObject>((int)GameObjects.CurrentWeaponImg).GetComponent<Image>().sprite= _currentWeapon._weaponImage;
        Get<GameObject>((int)GameObjects.AmmoText).GetComponent<Text>().text = $"{_currentWeapon._currentAmmo} / {_currentWeapon._ammoSize}";
    }

    void FiredAmmo(WeaponBased currentWeapon)
    {
        Get<GameObject>((int)GameObjects.AmmoText).GetComponent<Text>().text = $"{currentWeapon._currentAmmo} / {currentWeapon._ammoSize}";
    }

    IEnumerator RotateImg(float duration)
    {
        float time = 0.0f;
        float degrees = 360f;
        float startAngle = Get<GameObject>((int)GameObjects.RotateImg).GetComponent<RectTransform>().localRotation.z;
        while (time<1.0f)
        {

            float ratio = time / duration;
            float currentAngle = Mathf.Lerp(0, degrees,ratio);
             Get<GameObject>((int)GameObjects.RotateImg).GetComponent<RectTransform>().localRotation= Quaternion.Euler(0,0,startAngle+currentAngle);

            time += Time.deltaTime / duration;
            yield return null;
        }

    }

}
