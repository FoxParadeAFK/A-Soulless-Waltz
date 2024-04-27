using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestFire : MonoBehaviour
{
    [SerializeField] private float accuracy;
    [SerializeField] private float reloadSpeed;
    [SerializeField] private int magazineSize;
    private int currentMagazineSize;
    private AimerMaster aimerMaster;
    private WeaponFiring weaponFiring;
    private Vector3 leftAngleVector;
    private Vector3 rightAngleVector;
    private bool canFire = false;
    private float fireStartTime;
    private float magazineStartTime;
    [SerializeField] private GameObject projectile;
    [SerializeField] private float cooldown;
    [SerializeField] private bool selectFireAuto;
    [SerializeField] private Color GizmosColor;
    private float aimingIncrease = 1;
    private bool isReloading;

    public Action EventReloadFinished;

    private void Awake() {
        aimerMaster = GameObject.Find("Aimer").GetComponent<AimerMaster>();
        weaponFiring = GetComponentInParent<WeaponFiring>();
        currentMagazineSize = magazineSize;
    }
    private void Update() {
        Debug.Log(aimingIncrease);
        // * Move to the weapon dependent 
        float leftAngleBound = Mathf.Atan2(aimerMaster.MouseVector.y, aimerMaster.MouseVector.x) + (-(accuracy * aimingIncrease) * Mathf.Deg2Rad);
        float rightAngleBound = Mathf.Atan2(aimerMaster.MouseVector.y, aimerMaster.MouseVector.x) + (accuracy * aimingIncrease * Mathf.Deg2Rad);
        leftAngleVector = new Vector2(Mathf.Cos(leftAngleBound), Mathf.Sin(leftAngleBound));
        rightAngleVector = new Vector2(Mathf.Cos(rightAngleBound), Mathf.Sin(rightAngleBound));
        Vector3 movementVector = aimerMaster.MovementVectorCalculation(accuracy * aimingIncrease);

        if (canFire && !isReloading && currentMagazineSize > 0) {
            canFire = false;
            currentMagazineSize--;
            fireStartTime = Time.time;

            GameObject projectileSpawned = Instantiate(projectile, transform.position, Quaternion.LookRotation(transform.forward, movementVector));
            Rigidbody2D projectileRB = projectileSpawned.GetComponent<Rigidbody2D>();
            projectileRB.velocity = movementVector * 10f;
        }
    }

    private void HandleFireInput() {
        if (Time.time >= fireStartTime + cooldown && !canFire && !isReloading) {
            canFire = true;
        }
    }

    private void HandleAimingActive() {
        aimingIncrease = 0.5f;
    }
    private void HandleAimingCancel() {
        aimingIncrease = 1;
    }
    private void HandleReloadInput() {
        if (!isReloading) StartCoroutine(ReloadRoutine());
    }
    private IEnumerator ReloadRoutine() {
        isReloading = true;
        float counter = 0;
        while (counter < 3) {
            counter += Time.deltaTime;
            yield return null;
        }
        currentMagazineSize = magazineSize;
        isReloading = false;
        EventReloadFinished?.Invoke();
    }

    private void OnEnable() {
        if (selectFireAuto) weaponFiring.EventFireWeaponAuto += HandleFireInput; else weaponFiring.EventFireWeaponSemi += HandleFireInput;
        weaponFiring.EventAimWeaponActive += HandleAimingActive;
        weaponFiring.EventAimWeaponCancel += HandleAimingCancel;
        // weaponFiring.EventReload += HandleReloadInput;

        weaponFiring.EventBreakReload += HandleReloadInput;

    }
    private void OnDisable() {
        if (selectFireAuto) weaponFiring.EventFireWeaponAuto -= HandleFireInput; else weaponFiring.EventFireWeaponSemi -= HandleFireInput;
        weaponFiring.EventAimWeaponActive -= HandleAimingActive;
        weaponFiring.EventAimWeaponCancel -= HandleAimingCancel;
        // weaponFiring.EventReload -= HandleReloadInput;

        weaponFiring.EventBreakReload -= HandleReloadInput;
    }

    private void OnDrawGizmos() {
        Gizmos.color = GizmosColor;
        Gizmos.DrawLine(transform.position, transform.position + leftAngleVector * 1.5f * Mathf.Sqrt(Mathf.Pow(aimerMaster.MouseVector.x, 2) + Mathf.Pow(aimerMaster.MouseVector.y, 2)));
        Gizmos.DrawLine(transform.position, transform.position + rightAngleVector * 1.5f * Mathf.Sqrt(Mathf.Pow(aimerMaster.MouseVector.x, 2) + Mathf.Pow(aimerMaster.MouseVector.y, 2)));
        Gizmos.DrawWireSphere(aimerMaster.gameObject.transform.position, Mathf.Abs(Mathf.Sin((accuracy * aimingIncrease) * Mathf.Deg2Rad)) * Mathf.Sqrt(Mathf.Pow(aimerMaster.MouseVector.x, 2) + Mathf.Pow(aimerMaster.MouseVector.y, 2)));
    }  
}
