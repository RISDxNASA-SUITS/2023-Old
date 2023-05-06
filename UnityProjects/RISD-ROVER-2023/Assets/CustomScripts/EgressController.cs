using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EgressController : MonoBehaviour
{
    private bool isTesting = false;
    public enum Test
    {
        PrepareUIA, PurgeN2, O2Pressure, EMUwater, AirLock, EMUPressure, AirLockDepress
    }

    private Test currTest;

    private int currStep;

    private float startTime;

    private TMPro.TMP_Text messageText;
    private GameObject UIAText, N2Text, O2PressureText, EMUwaterText, AirLockText,EMUPressureText, AirLockDepressText;
    private GameObject UIAComplete, N2Complete, O2PressureComplete, EMUwaterComplete, AirLockComplete, EMUPressureComplete, AirLockDepressComplete;
    private GameObject message, Egress, Nav;
    private bool[] isCompleted = new bool[7];

    private MapController _mapControllerScript;

    public void PerformTask(float s)
    {
        if(Time.time - startTime > s /10)
        {
            currStep++;
            startTime = Time.time;
        }
    }

    public void StartTest(int index)
    {
        isTesting = true;
        currTest = (Test)index;
        currStep = 0;
        startTime = Time.time;
        message.SetActive(true);
    }

    private void CompleteTest(Test t)
    {
        int index = (int)t;
        isCompleted[index] = true;
        bool allComplete = true;

        for(int i = 0; i < 7; i++) {
            if (!isCompleted[i])
            {
                allComplete = false;
                break;
            }
        }

        if (allComplete) {
            gameObject.SetActive(false);
            Nav.SetActive(true);
            _mapControllerScript.RecordStartTime();
        }
    }

    public void PerformTest()
    {
        switch (currTest)
        {
            case Test.PrepareUIA:
                switch (currStep)
                {
                    case 0:
                        messageText.text = "Switch O2 Vent to OPEN";
                        PerformTask(1);
                        break;
                    case 1:
                        messageText.text = "When UIA Supply Pressure (uia_ < 23 psi, proceed";
                        PerformTask(1);
                        break;
                    case 2:
                        messageText.text = "Switch O2 Vent to CLOSE";
                        PerformTask(1);
                        break;
                    default:
                        message.SetActive(false);
                        isTesting = false;
                        UIAComplete.SetActive(true);
                        UIAText.SetActive(false);
                        CompleteTest(Test.PrepareUIA);
                        // UIAText.color = new Color(0, 255, 0);
                        break;
                }
                break;

            case Test.PurgeN2:
                switch (currStep)
                {
                    case 0:
                        messageText.text = "Switch O2 Supply to OPEN";
                        PerformTask(1);
                        break;
                    case 1:
                        messageText.text = "When UIA Supply Pressure is > 3000 psi, proceed";
                        PerformTask(1);
                        break;
                    case 2:
                        messageText.text = "Switch O2 Supply to CLOSE";
                        PerformTask(1);
                        break;
                    case 3:
                        messageText.text = "Switch O2 Vent to OPEN";
                        PerformTask(1);
                        break;
                    case 4:
                        messageText.text = "When UIA Supply Pressure is < 23 psi, proceed";
                        PerformTask(1);
                        break;
                    case 5:
                        messageText.text = "Switch O2 Vent to CLOSE";
                        PerformTask(1);
                        break;
                    default:
                        message.SetActive(false);
                        isTesting = false;
                        N2Complete.SetActive(true);
                        N2Text.SetActive(false);
                        CompleteTest(Test.PurgeN2);
                        // N2Text.color = new Color(0, 255, 0);
                        break;
                }
                break;

            case Test.O2Pressure:
                switch (currStep)
                {
                    case 0:
                        messageText.text = "Switch O2 Vent to OPEN";
                        PerformTask(1);
                        break;
                    case 1:
                        messageText.text = "When UIA Supply Pressure is > 1500 psi, proceed";
                        PerformTask(1);
                        break;
                    case 2:
                        messageText.text = "Switch O2 Supply to CLOSE";
                        PerformTask(1);
                        break;
                    default:
                        message.SetActive(false);
                        isTesting = false;
                        O2PressureComplete.SetActive(true);
                        O2PressureText.SetActive(false);
                        CompleteTest(Test.O2Pressure);
                        // O2PressureText.color = new Color(0, 255, 0);
                        break;
                }
                break;

                case Test.EMUwater:
                switch (currStep)
                {
                    case 0:
                        messageText.text = "Dump waste water";
                        PerformTask(1);
                        break;
                    case 1:
                        messageText.text = "Switch EV-1 Waste to";
                        PerformTask(1);
                        break;
                    case 2:
                        messageText.text = "When water level if < 5%, proceed";
                        PerformTask(1);
                        break;
                    case 3:
                        messageText.text = "Switch EV-1 Waste to CLOSE";
                        PerformTask(1);
                        break;
                    case 4:
                        messageText.text = "Refill EMU Water";
                        PerformTask(1);
                        break;
                    case 5:
                        messageText.text = "Switch EV-1 Supply to OPEN";
                        PerformTask(1);
                        break;
                    case 6:
                        messageText.text = "When water level is > 95%, proceed";
                        PerformTask(1);
                        break;
                    case 7:
                        messageText.text = "Switch EV-1 Supply to CLOSE";
                        PerformTask(1);
                        break;
                    default:
                        message.SetActive(false);
                        isTesting = false;
                        EMUwaterComplete.SetActive(true);
                        EMUwaterText.SetActive(false);
                        CompleteTest(Test.EMUwater);
                        // EMUwaterText.color = new Color(0, 255, 0);
                        break;
                }
                break;

                case Test.AirLock:
                switch (currStep)
                {
                    case 0:
                        messageText.text = "Switch Depress Pump to ON";
                        PerformTask(1);
                        break;
                    case 1:
                        messageText.text = "When airlock pressure is < 0.1 psi, proceed";
                        PerformTask(1);
                        break;
                    case 2:
                        messageText.text = "Switch Depress Pump to OFF";
                        PerformTask(1);
                        break;
                    default:
                        message.SetActive(false);
                        isTesting = false;
                        AirLockComplete.SetActive(true);
                        AirLockText.SetActive(false);
                        CompleteTest(Test.AirLock);
                        // AirLockText.color = new Color(0, 255, 0);
                        break;
                }
                break;

                case Test.EMUPressure:
                switch (currStep)
                {
                    case 0:
                        messageText.text = "Switch O2 Supply to OPEN";
                        PerformTask(1);
                        break;
                    case 1:
                        messageText.text = "When UIA Supply Pressure > 3000 psi, proceed";
                        PerformTask(1);
                        break;
                    case 2:
                        messageText.text = "Switch O2 Supply to CLOSE";
                        PerformTask(1);
                        break;
                    default:
                        message.SetActive(false);
                        isTesting = false;
                        EMUPressureComplete.SetActive(true);
                        EMUPressureText.SetActive(false);
                        CompleteTest(Test.EMUPressure);
                        // EMUPressureText.color = new Color(0, 255, 0);
                        break;
                }
                break;

                case Test.AirLockDepress:
                switch (currStep)
                {
                    case 0:
                        messageText.text = "Switch Depress Pump to ON";
                        PerformTask(1);
                        break;
                    case 1:
                        messageText.text = "When airlock pressure is < 0.1 psi, proceed";
                        PerformTask(1);
                        break;
                    case 2:
                        messageText.text = "Switch Depress Pump to OFF";
                        PerformTask(1);
                        break;
                    default:
                        message.SetActive(false);
                        isTesting = false;
                        AirLockDepressComplete.SetActive(true);
                        AirLockDepressText.SetActive(false);
                        CompleteTest(Test.AirLockDepress);
                        // AirLockDepressText.color = new Color(0, 255, 0);
                        break;
                }
                break;
        }
    }

    // public void EndTest ()
    // {
    //    float delay = 3.0f;
    //    Egress = GameObject.Find("Egress");
    //    Destroy(Egress, delay);
    // }


    // Start is called before the first frame update
    void Start()
    {
        message = GameObject.Find("egress msg");
        messageText = GameObject.Find("egress msg text").GetComponent<TMPro.TMP_Text>();
        UIAText = GameObject.Find("UIA Text");
        N2Text = GameObject.Find("N2 Text");
        O2PressureText = GameObject.Find("O2 Pressure Text");
        EMUwaterText = GameObject.Find("EMU water Text");
        AirLockText = GameObject.Find("Airlock Text");
        EMUPressureText = GameObject.Find("EMU Text");
        AirLockDepressText = GameObject.Find("Airlock Depressure Text");

        UIAComplete = GameObject.Find("UIA Text g");
        N2Complete = GameObject.Find("N2 Text g");
        O2PressureComplete = GameObject.Find("O2 Pressure Text g");
        EMUwaterComplete = GameObject.Find("EMU water Text g");
        AirLockComplete = GameObject.Find("Airlock Text g");
        EMUPressureComplete = GameObject.Find("EMU Text g");
        AirLockDepressComplete = GameObject.Find("Airlock Depressure Text g");

        UIAComplete.SetActive(false);
        N2Complete.SetActive(false);
        O2PressureComplete.SetActive(false);
        EMUwaterComplete.SetActive(false);
        AirLockComplete.SetActive(false);
        EMUPressureComplete.SetActive(false);
        AirLockDepressComplete.SetActive(false);
        // CompleteText.SetActive(false);
        message.SetActive(false);

        Nav = GameObject.Find("Main Panel");
        _mapControllerScript = GameObject.Find("Map Panel").GetComponent<MapController>();

        Nav.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTesting)
        {
            PerformTest();
        }
    }
}
