using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class EachBox {
    public List <GameObject> EachBoxLocation;
}

[System.Serializable]
public class EachSlot {
    public List<EachBox> EachSlotLocation;
}

public class BoxSlots {

    //Each box slot properties
    public int SlotID { get; set; }
    public List<Transform> SpawnLocations { get; set; }
    public List<GameObject> SpawnObjects { get; set; }
    public int BoxCounter { get; set; }
    public TextMeshProUGUI BoxCounterText { get; set; }
    public Slider ProgressBar { get; set; }
    public bool IsAvailable { get; set; }

    //Each box slot constructor
    public BoxSlots(int slotID, List<GameObject> spawnObjects, List<Transform> spawnLocation,  int boxCounter, TextMeshProUGUI boxCounterText, Slider progressBar, bool isAvailable) {
        SlotID = slotID;
        SpawnObjects = spawnObjects;
        SpawnLocations = spawnLocation;
        BoxCounter = boxCounter;
        BoxCounterText = boxCounterText;
        ProgressBar = progressBar;
        IsAvailable = isAvailable;
    }
}

public class BoxManager : MonoBehaviour {

    //Boxes

    //Adding Cube GameObjects and their transforms
    public EachSlot BoxesPerSlot = new EachSlot();

    //Adding Prefabs for Instantiation
    public List<GameObject> BoxPrefabs = new List<GameObject>(5);

    //Adding Box Slot GameObjects
    public List<GameObject> BoxSlots= new List<GameObject>(12);

    //Adding Transform Locations for each Box
    public List<Transform> BoxSpawnLocations = new List<Transform>(60);
    public List<Vector3> BoxSpawnLocationsSave = new List<Vector3>(60);

    //Box Counter as Field
    public List<int> BoxCounters = new List<int>(12);

    //Box Destroy Count per Slot
    public List<int> DestroyCounter= new List<int>(12);

    //Slider Objects
    public List<Slider> ProgressBars = new List<Slider>(12);

    //Text Counting Destroyed Boxes
    public List<TextMeshProUGUI> BoxCounterTextList = new List<TextMeshProUGUI>(12);

    public Game_Manager Game_ManagerScript;
    List<SlotState> SlotStatesBox = new List<SlotState>();

    void Start() {
        for (int i = 0; i < SlotStatesBox.Count*5; i++) {
            BoxSpawnLocationsSave[i] = BoxSpawnLocations[i].position;
        }
    }

    void Update() {

        //Connection to Game Manager for Slot Status (Is Available?)
        //If available (purchased by the player), boxes can be spawned. Updating same boolean from this side.
        SlotStatesBox = Game_ManagerScript.SlotStates;

        //Instantiation of all Box Slots
        List<BoxSlots> boxSlots = new List<BoxSlots>(12) {
        new BoxSlots(1, GetSlotNumber(0), GetSlotTransform(0), BoxCounters[0], BoxCounterTextList[0], ProgressBars[0], SlotStatesBox[0].IsAvailable),
        new BoxSlots(2, GetSlotNumber(1), GetSlotTransform(1), BoxCounters[1], BoxCounterTextList[1], ProgressBars[1], SlotStatesBox[1].IsAvailable),
        new BoxSlots(3, GetSlotNumber(2), GetSlotTransform(2), BoxCounters[2], BoxCounterTextList[2], ProgressBars[2], SlotStatesBox[2].IsAvailable),
        new BoxSlots(4, GetSlotNumber(3), GetSlotTransform(3), BoxCounters[3], BoxCounterTextList[3], ProgressBars[3], SlotStatesBox[3].IsAvailable),
        new BoxSlots(5, GetSlotNumber(4), GetSlotTransform(4), BoxCounters[4], BoxCounterTextList[4], ProgressBars[4], SlotStatesBox[4].IsAvailable),
        new BoxSlots(6, GetSlotNumber(5), GetSlotTransform(5), BoxCounters[5], BoxCounterTextList[5], ProgressBars[5], SlotStatesBox[5].IsAvailable),
        new BoxSlots(7, GetSlotNumber(6), GetSlotTransform(6), BoxCounters[6], BoxCounterTextList[6], ProgressBars[6], SlotStatesBox[6].IsAvailable),
        new BoxSlots(8, GetSlotNumber(7), GetSlotTransform(7), BoxCounters[7], BoxCounterTextList[7], ProgressBars[7], SlotStatesBox[7].IsAvailable),
        new BoxSlots(9, GetSlotNumber(8), GetSlotTransform(8), BoxCounters[8], BoxCounterTextList[8], ProgressBars[8], SlotStatesBox[8].IsAvailable),
        new BoxSlots(10, GetSlotNumber(9), GetSlotTransform(9), BoxCounters[9], BoxCounterTextList[9], ProgressBars[9], SlotStatesBox[9].IsAvailable),
        new BoxSlots(11, GetSlotNumber(10), GetSlotTransform(10), BoxCounters[10], BoxCounterTextList[10], ProgressBars[10], SlotStatesBox[10].IsAvailable),
        new BoxSlots(12, GetSlotNumber(11), GetSlotTransform(11), BoxCounters[11], BoxCounterTextList[11], ProgressBars[11], SlotStatesBox[11].IsAvailable),
        };

        #region IndexOutofBounds?
        //for (int i = 0; i < 12; i++) {
        //    if (boxSlots[i] == null) {
        //        boxSlots[i] = new BoxSlots(i + 1, GetSlotNumber(i), GetSlotTransform(i), 0, BoxCounterTextList[i], ProgressBars[i], SlotStatesBox[i].IsAvailable);
        //    }
        //} 
        #endregion

        //Updating values of each slot objects
        for (int i = 0; i < boxSlots.Count; i++) {
            boxSlots[i].BoxCounterText.text = boxSlots[i].BoxCounter.ToString();
            boxSlots[i].ProgressBar.value = boxSlots[i].BoxCounter;
            ProgressBarValues(boxSlots[i], boxSlots[i].ProgressBar);
        }
        OnBoxDestruct(boxSlots);

        for (int i = 0; i < boxSlots.Count; i++) {
            if (boxSlots[i].BoxCounter > 0 && boxSlots[i].BoxCounter % 5 == 0) {
                StartCoroutine(RespawnBox(boxSlots[i]));
            }
        }
    }

    //0 is slot number, 1 is box number
    public int[] GetBoxSlotandNumber (int n) {
        int k = n % BoxesPerSlot.EachSlotLocation.Count;
        int j = (int) Mathf.Floor(BoxesPerSlot.EachSlotLocation.Count / 5);
        int[] BoxSlotandNumber = { j, k };
        return BoxSlotandNumber;
    }

    //GameObject GetBoxonSlot(int n, int m) {
    //    return BoxesPerSlot.EachSlotLocation[n].EachBoxLocation[m];
    //}

    //Slot Number of Each Spawn Objects (Boxes)
    public List<GameObject> GetSlotNumber(int n) {
        return BoxesPerSlot.EachSlotLocation[n].EachBoxLocation;
    }

    //public GameObject GetParentofBoxes(int n) {
    //    string parentname = String.Concat("Boxes" + n);
    //    return GameObject.Find(parentname);
    //}

    //Does not work;
    //public int GetSlotNumber (GameObject gameObject) {
    //    int n = 0;
    //    for (int i = 0; i < ProgressBars.Count; i++) {
    //        if (GetSlotNumber(i).Contains(gameObject)) {
    //            n = i;
    //        }
    //    }
    //    return n;
    //}

    //Slot Number of Each Spawn Locations (Box Spawn Locations)
    List<Transform> GetSlotTransform(int n) {
        List<Transform> TransformperSlot = new List<Transform>(5);
        for (int i = 0; i < TransformperSlot.Count; i++) {
            TransformperSlot[i] = BoxesPerSlot.EachSlotLocation[n].EachBoxLocation[i].transform;
        }
        return TransformperSlot;
    }

    //Updating min/max values of progress bars
    public void ProgressBarValues(BoxSlots _boxSlots, Slider _progressBar) {
        if (_boxSlots.BoxCounter < 160) {
            _progressBar.minValue = 0;
            _progressBar.maxValue = 160;
        }
        else if (_boxSlots.BoxCounter < 400) {
            _progressBar.minValue = 160;
            _progressBar.maxValue = 400;
        }
        else if (_boxSlots.BoxCounter < 900) {
            _progressBar.minValue = 400;
            _progressBar.maxValue = 900;
        }
        else if (_boxSlots.BoxCounter < 1600) {
            _progressBar.minValue = 900;
            _progressBar.maxValue = 1600;
        }
        else {
            _progressBar.maxValue = _boxSlots.BoxCounter;
        }
    }

    //When boxes are destroyed by guns
    public void OnBoxDestruct(List<BoxSlots> boxSlots) {
        for (int i = 0; i < boxSlots.Count; i++) {
            for (int j = 0; j < boxSlots[i].SpawnLocations.Count; j++) {
                if (boxSlots[i].SpawnObjects[j].IsDestroyed()) {
                    boxSlots[i].SpawnObjects[j] = null;
                }
            }
        }
    }

    //Instantiation for new set of boxes
    
    public IEnumerator RespawnBox(BoxSlots boxSlots) {

            if (boxSlots.BoxCounter>0 && boxSlots.BoxCounter % 5 == 0) {
                if (boxSlots.BoxCounter < 80) {
                    yield return new WaitForSeconds(0.2f);
                    for (int j = 0; j < 5; j++) {
                        var box = Instantiate(BoxPrefabs[0], boxSlots.SpawnLocations[j].position, Quaternion.identity);
                        box.transform.SetParent(GameObject.Find(string.Concat("Boxes" + boxSlots.SlotID)).transform, true);
                        boxSlots.SpawnObjects.Add(box);
                    }
                }
                else if (boxSlots.BoxCounter > 0 && boxSlots.BoxCounter < 240) {
                    yield return new WaitForSeconds(0.2f);
                    for (int j = 0; j < 5; j++) {
                        var box = Instantiate(BoxPrefabs[1], boxSlots.SpawnLocations[j].position, Quaternion.identity);
                        box.transform.SetParent(GameObject.Find(string.Concat("Boxes" + boxSlots.SlotID)).transform, true);
                        boxSlots.SpawnObjects.Add(box);
                    }
                }
                else if (boxSlots.BoxCounter > 0 && boxSlots.BoxCounter < 800) {
                    yield return new WaitForSeconds(0.2f);
                    for (int j = 0; j < 5; j++) {
                        var box = Instantiate(BoxPrefabs[2], boxSlots.SpawnLocations[j].position, Quaternion.identity);
                        box.transform.SetParent(GameObject.Find(string.Concat("Boxes" + boxSlots.SlotID)).transform, true);
                        boxSlots.SpawnObjects.Add(box);
                    }
                }
                else if (boxSlots.BoxCounter > 0 && boxSlots.BoxCounter < 1800) {
                    yield return new WaitForSeconds(0.2f);
                    for (int j = 0; j < 5; j++) {
                        var box = Instantiate(BoxPrefabs[3], boxSlots.SpawnLocations[j].position, Quaternion.identity);
                        box.transform.SetParent(GameObject.Find(string.Concat("Boxes" + boxSlots.SlotID)).transform, true);
                        boxSlots.SpawnObjects.Add(box);
                    }
                }
                else if (boxSlots.BoxCounter > 0 && boxSlots.BoxCounter < 4000) {
                    yield return new WaitForSeconds(0.2f);
                    for (int j = 0; j < 5; j++) {
                        var box = Instantiate(BoxPrefabs[4], boxSlots.SpawnLocations[j].position, Quaternion.identity);
                        box.transform.SetParent(GameObject.Find(string.Concat("Boxes" + boxSlots.SlotID)).transform, true);
                        boxSlots.SpawnObjects.Add(box);
                    }
                }
            }
    }
}



