using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFiring : MonoBehaviour {
    private PlayerMaster playerMaster;
    private WeaponSelector weaponSelector;

    public Action EventFireWeaponAuto;
    public Action EventFireWeaponSemi;
    public Action EventAimWeaponActive;
    public Action EventAimWeaponCancel;
    // public Action EventReload;

    public Action EventBreakReload;

    private void Awake() {
        playerMaster = GetComponentInParent<PlayerMaster>();
        weaponSelector = GetComponentInParent<WeaponSelector>();
    }

    private void HandleFireInputAuto() => EventFireWeaponAuto?.Invoke();
    private void HandleFireInputSemi() => EventFireWeaponSemi?.Invoke();
    private void HandleAimInputActive() => EventAimWeaponActive?.Invoke();
    private void HandleAimInputCancel() => EventAimWeaponCancel?.Invoke();
    private void HandleBreakReload() =>  Debug.Log("ABout to reload"); // EventBreakReload?.Invoke();

    private void OnEnable() {
        playerMaster.EventFireAuto += HandleFireInputAuto;
        playerMaster.EventFireSemi += HandleFireInputSemi;
        playerMaster.EventAimActive += HandleAimInputActive;
        playerMaster.EventAimCancel += HandleAimInputCancel;

        weaponSelector.EventBreakReload += HandleBreakReload;
    }


    private void OnDisable() {
        playerMaster.EventFireAuto -= HandleFireInputAuto;
        playerMaster.EventFireSemi -= HandleFireInputSemi;
        playerMaster.EventAimActive -= HandleAimInputActive;
        playerMaster.EventAimCancel -= HandleAimInputCancel;

        weaponSelector.EventBreakReload -= HandleBreakReload;
    }
}