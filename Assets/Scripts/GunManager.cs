using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class EachGun {
    public List<GameObject> EachGunperSlot;
}

[System.Serializable]
public class EachSlotGun {
    public List<EachGun> AllGuns;
}

public class GunSlots {
    public int SlotID { get; set; }
    public int GunID { get; set; }
    public List<GameObject> GunObjects { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsGunned { get; set; }

    public GunSlots(int slotID, List<GameObject> gunObjects, bool isAvailable, bool isGunned) {
        SlotID = slotID;
        GunObjects = gunObjects;
        IsAvailable = isAvailable;
        IsGunned = isGunned;
    }
}

public class GunManager : MonoBehaviour
{
    //Visual Inputs
    public List<EachGun> AllGuns = new List<EachGun>(12);
    public List<Transform> AllBulletPoints = new List<Transform>(12*11);
    public List<int> GunIDs = new List<int>(12);

    public Game_Manager Game_ManagerScript;
    List<SlotState> SlotStatesBox = new List<SlotState>();

    public void Awake() {
        //bulletsLeft = magazineSize;
        //clickshooting= false;
        //readytoShoot = true;
    }

    public void Update() {

        SlotStatesBox = Game_ManagerScript.SlotStates;

        //for (int i = 0; i < GunIDs.Count; i++) {
        //    GunIDs[i] = GetActiveGuninSlot(i).GetComponent<GunPrefabScript>().GunID;
        //}
        

        //List<GunSlots> gunSlots = new List<GunSlots>() {
        //    new GunSlots (1, GetGunSlotNumber(0), SlotStatesBox[0].IsAvailable, false),
        //    new GunSlots (2, GetGunSlotNumber(1), SlotStatesBox[1].IsAvailable, false),
        //    new GunSlots (3, GetGunSlotNumber(2), SlotStatesBox[2].IsAvailable, false),
        //    new GunSlots (4, GetGunSlotNumber(3), SlotStatesBox[3].IsAvailable, false),
        //    new GunSlots (5, GetGunSlotNumber(4), SlotStatesBox[4].IsAvailable, false),
        //    new GunSlots (6, GetGunSlotNumber(5), SlotStatesBox[5].IsAvailable, false),
        //    new GunSlots (7, GetGunSlotNumber(6), SlotStatesBox[6].IsAvailable, false),
        //    new GunSlots (8, GetGunSlotNumber(7), SlotStatesBox[7].IsAvailable, false),
        //    new GunSlots (9, GetGunSlotNumber(8), SlotStatesBox[8].IsAvailable, false),
        //    new GunSlots (10, GetGunSlotNumber(9), SlotStatesBox[9].IsAvailable, false),
        //    new GunSlots (11, GetGunSlotNumber(10), SlotStatesBox[10].IsAvailable, false),
        //    new GunSlots (12, GetGunSlotNumber(11), SlotStatesBox[11].IsAvailable, false),
        //};

    }

    List<GameObject> GetGunSlotNumber(int n) {
        return AllGuns[n].EachGunperSlot;
    }

    //0 is slot number, 1 is box number
    public float[] GetGunSlotandNumber(float n) {
        float k = n % AllGuns[0].EachGunperSlot.Count;
        float j = Mathf.Floor(n / AllGuns[0].EachGunperSlot.Count);
        float[] BoxSlotandNumber = { j, k };
        return BoxSlotandNumber;
    }

    public int GetSlotNumberofaGun(GameObject gameObject) {
        int n= 0;
        for (int i = 0; i < AllGuns.Count; i++) {
            if (AllGuns[i].EachGunperSlot.Contains(gameObject)) {
                n = i;
            }
        }
        return n;
    }

    GameObject GetGuninSlot(int n, int m) {
        return AllGuns[n].EachGunperSlot[m];
    }

    public GameObject GetActiveGuninSlot (int n) {
        for (int i = 0; i < AllGuns[n].EachGunperSlot.Count; i++) {
            if (GetGuninSlot(n, i).activeInHierarchy) {
                return GetGuninSlot(n, i);
            }
        }
        return null;
    }

    public GunPrefabScript GetGunComponent(int n, int m) {
        return AllGuns[n].EachGunperSlot[m].GetComponent<GunPrefabScript>();
    }

    public void CreateGun() {
        for (int i = 0; i < AllGuns.Count; i++) {
            if (SlotStatesBox[i].IsAvailable && !SlotStatesBox[i].IsGunned) {
                AllGuns[i].EachGunperSlot[0].SetActive(true);
                SlotStatesBox[i].IsGunned = true;
                break;
            }
        }
    }

    public void MergeGuns() {
        int n = GetDuplicatedSlot(GunIDs);
        int m = 0;

        if (n > 0) {
            for (int i = 0; i < GunIDs.Count; i++) {
                if (m < 3 && GunIDs[i] != 0) {
                    AllGuns[i].EachGunperSlot[n].SetActive(false);
                    if (m<1) {
                        AllGuns[i].EachGunperSlot[n+1].SetActive(true);
                    }
                    SlotStatesBox[i].IsGunned = false;
                    m++;
                }
            }
        }
    }

    public int GetDuplicatedSlot(List<int> integers) {
        var groups = integers.GroupBy(x => x);
        var duplicatedInt = groups.Where(g => g.Count() >= 3 && !g.Equals(0)).FirstOrDefault();
        return duplicatedInt != null ? duplicatedInt.Key : default(int);
    }



}
