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
    private bool isReloading;

    private void Awake() {
        playerMaster = GetComponentInParent<PlayerMaster>();
        weaponSelector = GetComponentInParent<WeaponSelector>();
    }

    private void HandleFireInputAuto() => EventFireWeaponAuto?.Invoke();
    private void HandleFireInputSemi() => EventFireWeaponSemi?.Invoke();
    private void HandleAimInputActive() => EventAimWeaponActive?.Invoke();
    private void HandleAimInputCancel() => EventAimWeaponCancel?.Invoke();
    private void HandleBreakReload() {
        float reloadSpeed = GetComponentInChildren<TestFire>().reloadSpeed;
        if (!isReloading) StartCoroutine(ReloadRoutine(reloadSpeed));
    }
    private IEnumerator ReloadRoutine(float reloadSpeed) {
        isReloading = true;
        for (float counter = 0; counter < reloadSpeed; counter += Time.deltaTime) yield return null;

        EventBreakReload?.Invoke();
        weaponSelector.isActionActive = false;
        isReloading = false;
    }

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