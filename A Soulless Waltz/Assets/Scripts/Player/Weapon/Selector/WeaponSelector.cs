using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSelector : MonoBehaviour {
    private int counter;
    private PlayerMaster playerMaster;
    private bool isActionActive;

    public Action EventBreakReload;

    private void Awake() {
        transform.GetChild(counter).gameObject.SetActive(true); 
        playerMaster = GetComponentInParent<PlayerMaster>();
    }
    private void HandleRotation(int rotation) {
        if (rotation == 1 && !isActionActive) {
            transform.GetChild(counter).gameObject.SetActive(false);
            counter++;
            if (counter > transform.childCount - 1) counter = 0;
            transform.GetChild(counter).gameObject.SetActive(true);
            isActionActive = false;
        }
        if (rotation == -1 && !isActionActive) {
            transform.GetChild(counter).gameObject.SetActive(false);
            counter--;
            if (counter < 0) counter = transform.childCount - 1;
            transform.GetChild(counter).gameObject.SetActive(true);
            isActionActive = false;
        }
    }

    private void HandleBreakReload() {
        isActionActive = true;
        EventBreakReload?.Invoke();
    }
    private void OnEnable() {
        playerMaster.EventRotation += HandleRotation;
        playerMaster.EventBreakReload += HandleBreakReload;
    }


    private void OnDisable() {
        playerMaster.EventRotation -= HandleRotation;
        playerMaster.EventBreakReload -= HandleBreakReload;
    }

}
