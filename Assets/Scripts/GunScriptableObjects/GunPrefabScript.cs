using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class GunPrefabScript : MonoBehaviour
{
    public GunScriptableObject gunScriptableObject;
    public BoxManager boxManager;
    public GunManager gunManager;

    int GunLevel;
    int GunMaxLevel = 20;

    public GameObject GunObject;
    GameObject muzzleFlash;
    GameObject Bullet;
    Transform BulletPoint;

    public string GunName;
    public float GunDamage, GunIdleTimer, GunReloadTimer, ProjectileSpeed;
    public int GunID, MagazineSize;
    public bool GunBurst, GunSpread, GunAreaofEffect;

    //Numerative variables
    public int spread, SlotNumber;
    int bulletsShot;

    //State variables
    bool readytoShoot, reloading;

    void Start()
    {
        reloading= false;
        readytoShoot = true;

        GunLevel = gunScriptableObject.GunLevel;
        GunID = gunScriptableObject.GunID;
        GunName = gunScriptableObject.WeaponName;

        GunObject = gunScriptableObject.Gun;
        muzzleFlash = gunScriptableObject.MuzzleFlash;
        Bullet = gunScriptableObject.Projectile;
        ProjectileSpeed = gunScriptableObject.bulletSpeed;

        GunObject = gunScriptableObject.Gun;
        MagazineSize = gunScriptableObject.MagazineSize;
        GunBurst = gunScriptableObject.Burst;
        GunSpread = gunScriptableObject.Spread;
        GunAreaofEffect = gunScriptableObject.AreaofEffect;

        if (gameObject.activeSelf) {
            BulletPoint = gameObject.transform.GetChild(0).transform;
        }

        

    }

    private void Update() {

        float CurrentProgress = GunLevel / GunMaxLevel;

        GunDamage = Mathf.Lerp(gunScriptableObject.MinDamage, gunScriptableObject.MaxDamage, CurrentProgress);
        GunIdleTimer = Mathf.Lerp(gunScriptableObject.maxIdleTimer, gunScriptableObject.minIdleTimer, CurrentProgress);
        GunReloadTimer = Mathf.Lerp(gunScriptableObject.maxReloadTime, gunScriptableObject.minReloadTime, CurrentProgress);

        for (int i = 0; i < gunManager.AllGuns.Count; i++) {
            GunPoint(GetSlotNumber());
        }

        StartCoroutine(Reload());
        StartCoroutine(AutoShoot());
        FindObjectOfType<GunManager>().GunIDs[GetSlotNumber()] = GunID;
    }


    int GetSlotNumber () {
        int n = 1;
        n = Convert.ToInt32(gameObject.transform.parent.gameObject.name[7..]);
        return n - 1;

    }

    void GunPoint(int n) {
        for (int i = 0; i < 5; i++) {
            if (boxManager.GetSlotNumber(i)[n] != null) {
                gameObject.transform.LookAt(boxManager.GetSlotNumber(i)[n].transform.position);
            }
            else {
                continue;
            }
        }
    }

    #region ShootingStates
    IEnumerator AutoShoot() {
        if (!reloading && readytoShoot && bulletsShot < MagazineSize) {
            //idleshooting
            ShootBullet();
            readytoShoot = false;
            yield return new WaitForSeconds(GunIdleTimer);
            readytoShoot = true;
        }
    }

    IEnumerator ClickShoot() {
        //clickshooting
        if (Input.GetMouseButtonDown(0)) {
            if (/*ClickCheck(GunObject) && */!reloading && readytoShoot && bulletsShot < MagazineSize) {
                ShootBullet();
                readytoShoot = false;
                yield return new WaitForSeconds(GunIdleTimer);
                readytoShoot = true;
            }
        }
    }

    //public bool ClickCheck(GameObject gameObject) {
    //    Ray Ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    RaycastHit2D hit = Physics2D.Raycast(Ray.origin, Ray.direction);

    //    if (hit.collider.gameObject == gameObject) {
    //        return true;
    //    }
    //    return false;
    //}

    public void ShootBullet() {
        InstantiateBullet();
        InstantiateMuzzleFlash();
        //Not stable
        //bullet.GetComponent<Rigidbody>().AddForce(ProjectileSpeed * transform.forward * 200000);
        //Does not Work
        //transform.Translate(Vector3.forward* ProjectileSpeed* Time.deltaTime, Space.Self);
        //Specifically added to bullet
        bulletsShot++;
    }

    void InstantiateBullet() {
        var bullet = Instantiate(gunScriptableObject.Projectile, BulletPoint.position, Quaternion.identity);
        bullet.GetComponent<BulletDamageScript>().Damage = GunDamage;
        bullet.GetComponent<BulletDamageScript>().ProjectileSpeed = ProjectileSpeed;
    }

    void InstantiateMuzzleFlash() {
        var muzzleFlash = Instantiate(gunScriptableObject.MuzzleFlash, BulletPoint.position, Quaternion.identity);
        Destroy(muzzleFlash, 0.3f);
    }
    #endregion



    #region Reloading Methods

    IEnumerator Reload() {
        if (bulletsShot == MagazineSize) {
            reloading = true;
            bulletsShot = 0;
            yield return new WaitForSeconds(GunReloadTimer);
            reloading = false;
        }
    }
    #endregion


}
