
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Guns", fileName ="GunScriptableObject")]
public class GunScriptableObject : ScriptableObject
{
    public int GunLevel=1;
    public int GunID;
    public GameObject Gun;
    public GameObject MuzzleFlash;
    public GameObject Projectile;
    public string WeaponName;
    public float MinDamage, MaxDamage, minIdleTimer, maxIdleTimer, minReloadTime, maxReloadTime, bulletSpeed;
    public int MagazineSize;
    public bool Burst, Spread, AreaofEffect;
}
