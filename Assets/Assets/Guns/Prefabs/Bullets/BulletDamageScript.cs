using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamageScript : MonoBehaviour
{

    Rigidbody bulletRB;
    public float Damage;
    public float ProjectileSpeed;

    // Start is called before the first frame update
    void Start()
    {
        bulletRB= GetComponent<Rigidbody>();
    }

    private void Update() {
        bulletRB.AddForce(new Vector3(1, 0, 0) * ProjectileSpeed, ForceMode.Force);
    }


}
