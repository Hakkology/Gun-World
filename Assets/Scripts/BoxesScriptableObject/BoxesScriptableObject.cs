using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxMaterial {
    wooden,
    steelwooden,
    steel,
    titaniumsteel,
    titanium
}

[CreateAssetMenu(menuName = "Boxes", fileName = "BoxesScriptableObject")]
public class BoxesScriptableObject : ScriptableObject
{
    public GameObject BoxPrefab;
    public BoxMaterial Material;
    public float hp;
    public int weight;
}
