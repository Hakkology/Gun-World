using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;



public class BoxPrefabScript : MonoBehaviour
{
    public BoxManager boxManager;
    public GunManager gunManager;
    string BulletTag = "Bullet";

    public BoxesScriptableObject boxesScriptableObject;
    GameObject BoxObject;
    Rigidbody boxRigidBody;

    int slotNumber;
    float hp;
    BoxMaterial Material;

    

    private void Start() {
        boxRigidBody = GetComponent<Rigidbody>();

        BoxObject = boxesScriptableObject.BoxPrefab;
        hp = boxesScriptableObject.hp;
        Material = boxesScriptableObject.Material;
        boxRigidBody.mass = boxesScriptableObject.weight;
        slotNumber = GetSlotNumber();
    }

    private void Update() {
        DestroyBox();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag(BulletTag)) {
            hp -= collision.gameObject.GetComponent<BulletDamageScript>().Damage;
            Destroy(collision.gameObject);
        }
    }

    //public int GetSlotNumber() { 
    //    int slot = 1;
    //    for (int i = 0; i < boxManager.BoxesPerSlot.EachSlotLocation.Count; i++) {
    //        if (boxManager.BoxesPerSlot.EachSlotLocation[i].EachBoxLocation.Contains(this.boxesScriptableObject.BoxPrefab)) {
    //            slot = i; break;
    //        }
    //    }
    //    return slot;
    //}

    public int GetSlotNumber() {
        int n=1;
        if (gameObject.transform.parent != null) {
            n = Convert.ToInt32(gameObject.transform.parent.name[5..]);
        }
        return n - 1;
    }

    public void DestroyBox() {
        if (hp <= 0) {
            Destroy(gameObject);
            FindObjectOfType<BoxManager>().BoxCounters[slotNumber]++;
            FindObjectOfType<BoxManager>().DestroyCounter[slotNumber]++;
            //boxManager.BoxCounters[slotNumber]++;
        }
    } 
}


