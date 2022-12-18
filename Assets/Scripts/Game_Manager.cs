using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class SlotState {
    //Checking Slot Status
    public int SlotID { get; set; }
    public List<GameObject> GunsList { get; set; }
    public bool IsAvailable { get; set; }
    public bool IsGunned { get; set; }

    public SlotState(int ID, List<GameObject> gunsList, bool isAvailable, bool isGunned) {
        SlotID = ID;
        GunsList = gunsList;
        IsAvailable = isAvailable;
        IsGunned = isGunned;
    }
}

public class Game_Manager : MonoBehaviour
{
    //Global Variables
    public float DollarCounter;
    public float GoldCounter;

    //List of Slots
    public List<Image> Slots = new List<Image>(12);

    //Buy Values for Slots
    //initial value for testing

    float BuyValue1 = Mathf.Pow(2, 2) * 200;
    float BuyValue2 = Mathf.Pow(2, 3) * 200;
    float BuyValue3 = Mathf.Pow(2, 4) * 200;
    float BuyValue4 = Mathf.Pow(2, 5) * 200;
    float BuyValue5 = Mathf.Pow(2, 6) * 200;
    float BuyValue6 = Mathf.Pow(2, 7) * 200;
    float BuyValue7 = Mathf.Pow(2, 8) * 200;
    float BuyValue8 = Mathf.Pow(2, 9) * 200;
    float BuyValue9 = Mathf.Pow(2, 10) * 200;
    float BuyValue10 = Mathf.Pow(2, 11) * 200;
    float BuyValue11 = Mathf.Pow(2, 12) * 200;

    public static List<GameObject> listofGuns;

    //Slot Status Objects
    public List<SlotState> SlotStates = new List<SlotState>(12) {
        new SlotState (1, listofGuns, true, false),
        new SlotState (2, listofGuns, false, false),
        new SlotState (3, listofGuns, false, false),
        new SlotState (4, listofGuns, false, false),
        new SlotState (5, listofGuns, false, false),
        new SlotState (6, listofGuns, false, false),
        new SlotState (7, listofGuns, false, false),
        new SlotState (8, listofGuns, false, false),
        new SlotState (9, listofGuns, false, false),
        new SlotState (10, listofGuns, false, false),
        new SlotState (11, listofGuns, false, false),
        new SlotState (12, listofGuns, false, false)
    };

    //Buttons to Unlock Slots
    [Header("Buttons for Slots")]
    public List<GameObject> BuyButtons= new List<GameObject> (11);

    //Box Section Controls
    [Header("Box Sections")]
    public List<GameObject> BoxSections = new List<GameObject>(12);

    //Dollars to Buy Slots
    [Header("Money to Buy Slots")]
    public List<TextMeshProUGUI> BuyValueTexts = new List<TextMeshProUGUI>(11);

    //Currency Text Modifiers
    [Header("Global Currency Text Modifiers")]
    public TextMeshProUGUI Dollars;
    public TextMeshProUGUI Gold;

    void Start()
    {
        //Global Currency Variables
        DollarCounter= 0;
        GoldCounter= 0;

        //Initial Texts of Slots
        BuyValueTexts[0].text = BuyValue1.ToString() + "$";
        for (int i = 1; i < BuyValueTexts.Count-1; i++) {
            BuyValueTexts[i].text = ("Not available yet");
        }

        BoxSections[0].SetActive(true);
        for (int i = 1; i < 12; i++) {
            BoxSections[i].SetActive(false);
        }
    }

    void Update()
    {
        Dollars.text = DollarCounter.ToString() + " $";
        Gold.text = GoldCounter.ToString() + " Gold";
    }

    //For each button
    public void SlotUnlockButton2() {
        
        if (DollarCounter > BuyValue1)  {
            DollarCounter -= BuyValue1;
            SlotStates[1].IsAvailable = true;
            BuyButtons[0].SetActive(false);
            BoxSections[1].SetActive(true);
            BuyValueTexts[1].text = (BuyValue2.ToString() + "$");
        }
    }

    public void SlotUnlockButton3() {

        if (DollarCounter > BuyValue2) {
            DollarCounter -= BuyValue2;
            SlotStates[2].IsAvailable = true;
            BuyButtons[1].SetActive(false);
            BoxSections[2].SetActive(true);
            BuyValueTexts[2].text = (BuyValue3.ToString() + "$");
        }
    }

    public void SlotUnlockButton4() {

        if (DollarCounter > BuyValue3) {
            DollarCounter -= BuyValue3;
            SlotStates[3].IsAvailable = true;
            BuyButtons[2].SetActive(false);
            BoxSections[3].SetActive(true);
            BuyValueTexts[3].text = (BuyValue4.ToString() + "$");
        }
    }

    public void SlotUnlockButton5() {

        if (DollarCounter > BuyValue4) {
            DollarCounter -= BuyValue4;
            SlotStates[4].IsAvailable = true;
            BuyButtons[3].SetActive(false);
            BoxSections[4].SetActive(true);
            BuyValueTexts[4].text = (BuyValue5.ToString() + "$");
        }
    }

    public void SlotUnlockButton6() {

        if (DollarCounter > BuyValue5) {
            DollarCounter -= BuyValue5;
            SlotStates[5].IsAvailable = true;
            BuyButtons[4].SetActive(false);
            BoxSections[5].SetActive(true);
            BuyValueTexts[5].text = (BuyValue6.ToString() + "$");
        }
    }

    public void SlotUnlockButton7() {

        if (DollarCounter > BuyValue6) {
            DollarCounter -= BuyValue6;
            SlotStates[6].IsAvailable = true;
            BuyButtons[5].SetActive(false);
            BoxSections[6].SetActive(true);
            BuyValueTexts[6].text = (BuyValue7.ToString() + "$");
        }
    }

    public void SlotUnlockButton8() {

        if (DollarCounter > BuyValue7) {
            DollarCounter -= BuyValue7;
            SlotStates[7].IsAvailable = true;
            BuyButtons[6].SetActive(false);
            BoxSections[7].SetActive(true);
            BuyValueTexts[7].text = (BuyValue8.ToString() + "$");
        }
    }

    public void SlotUnlockButton9() {

        if (DollarCounter > BuyValue8) {
            DollarCounter -= BuyValue8;
            SlotStates[8].IsAvailable = true;
            BuyButtons[7].SetActive(false);
            BoxSections[8].SetActive(true);
            BuyValueTexts[8].text = (BuyValue9.ToString() + "$");
        }
    }

    public void SlotUnlockButton10() {

        if (DollarCounter > BuyValue9) {
            DollarCounter -= BuyValue9;
            SlotStates[9].IsAvailable = true;
            BuyButtons[8].SetActive(false);
            BoxSections[9].SetActive(true);
            BuyValueTexts[9].text = (BuyValue10.ToString() + "$");
        }
    }

    public void SlotUnlockButton11() {

        if (DollarCounter > BuyValue10) {
            DollarCounter -= BuyValue10;
            SlotStates[10].IsAvailable = true;
            BuyButtons[9].SetActive(false);
            BoxSections[10].SetActive(true);
            BuyValueTexts[10].text = (BuyValue11.ToString() + "$");
        }
    }

    public void SlotUnlockButton12() {

        if (DollarCounter > BuyValue11) {
            DollarCounter -= BuyValue11;
            SlotStates[11].IsAvailable = true;
            BuyButtons[10].SetActive(false);
            BoxSections[11].SetActive(true);
        }
    }

    public void AddGunButton() {
        for (int i = 0; i < SlotStates.Count; i++) {
            if (SlotStates[i].IsAvailable && !!SlotStates[i].IsGunned) {
                AddGun();
                SlotStates[i].IsGunned = true;
            }
        }
    }

    public void AddGun() {

    }
}
