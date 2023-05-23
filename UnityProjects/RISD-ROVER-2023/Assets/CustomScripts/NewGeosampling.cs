using System.Collections;
using System.Collections.Generic;
using TSS.Msgs;
using UnityEngine;
using UnityEngine.UI;

public class NewGeosampling : MonoBehaviour
{
    public class SampleMetadata {
        public int SampleNum;
        public int MissionNum;
        public string RockType;
        public string Petrology;

        public SampleMetadata(int sampleNum, int missionNum, string rockType)
        {
            SampleNum = sampleNum;
            MissionNum = missionNum;
            RockType = rockType;
        }
    };

    public class SampleSpec {
        public float SiO2;
        public float TiO2;
        public float Al2O3;
        public float FeO;
        public float MnO;
        public float MgO;
        public float CaO;
        public float K2O;
        public float P2O3;

        public SampleSpec(float siO2, float tiO2, float al2O3, float feO, float mnO, float mgO, float caO, float k2O, float p2O3)
        {
            SiO2 = siO2;
            TiO2 = tiO2;
            Al2O3 = al2O3;
            FeO = feO;
            MnO = mnO;
            MgO = mgO;
            CaO = caO;
            K2O = k2O;
            P2O3 = p2O3;
        }
    }

    private GameObject numberScanned;
    private TMPro.TMP_Text numberText;
    private GameObject geoLoadingComplete;

    private GameObject compositionInfo;
    private TMPro.TMP_Text missionNum_Text, rockType_Text, sampleNum_Text;
    private TMPro.TMP_Text SiO2_Text, TiO2_Text, Al2O3_Text, FeO_Text, MnO_Text, MgO_Text, CaO_Text, K2O_Text, P2O3_Text;

    private SampleMetadata[] sampleMetadatas = {
        new SampleMetadata(70215312, 17, "Mare basalt"),
        new SampleMetadata(1555611, 15, " Vesicular basalt"), 
        new SampleMetadata(12002492, 12, " Olivine basalt"),
        new SampleMetadata(14310220, 14, " Feldspathic basalt"),
        new SampleMetadata(1205226, 12, "Pigeonite basalt"),
        new SampleMetadata(1555562, 15, "Olivine basalt"),
        new SampleMetadata(1001730, 11, "Ilmenite basalt"),
    };

    private SampleSpec[] sampleSpecs = {
        new SampleSpec(40.58f, 12.83f, 10.91f, 13.18f, 0.19f, 6.7f, 10.64f, -0.11f, 0.34f),
        new SampleSpec(36.89f, 2.44f, 9.6f, 14.52f, 0.24f, 5.3f, 8.22f, -0.13f, 0.29f),
        new SampleSpec(41.62f, 2.44f, 9.52f, 18.12f, 0.27f, 11.1f, 8.12f, -0.12f, 0.28f),
        new SampleSpec(46.72f, 1.1f, 19.01f, 7.21f, 0.14f, 7.83f, 14.22f, 0.43f, 0.65f),
        new SampleSpec(46.53f, 3.4f, 11.68f, 16.56f, 0.24f, 6.98f, 11.11f, -0.02f, 0.38f),
        new SampleSpec(42.45f, 1.56f, 11.44f, 17.91f, 0.27f, 10.45f, 9.37f, -0.08f, 0.34f),
        new SampleSpec(42.56f, 9.38f, 12.03f, 11.27f, 0.17f, 9.7f, 10.52f, 0.28f, 0.44f),
    };

    

    void Awake()
    {
        numberScanned = GameObject.Find("Number Scanned");
        numberText = GameObject.Find("Number Text").GetComponent<TMPro.TMP_Text>();
        geoLoadingComplete = GameObject.Find("Geo Loading Complete");
        
        compositionInfo = GameObject.Find("Composition Info");
        missionNum_Text = GameObject.Find("Mission Num").GetComponent<TMPro.TMP_Text>();
        rockType_Text = GameObject.Find("Rock Type").GetComponent<TMPro.TMP_Text>();
        sampleNum_Text = GameObject.Find("Sample Number").GetComponent<TMPro.TMP_Text>();
        SiO2_Text = GameObject.Find("Percent 1").GetComponent<TMPro.TMP_Text>();
        TiO2_Text = GameObject.Find("Percent 2").GetComponent<TMPro.TMP_Text>();
        Al2O3_Text = GameObject.Find("Percent 3").GetComponent<TMPro.TMP_Text>();
        FeO_Text = GameObject.Find("Percent 4").GetComponent<TMPro.TMP_Text>();
        MnO_Text = GameObject.Find("Percent 5").GetComponent<TMPro.TMP_Text>();
        MgO_Text = GameObject.Find("Percent 6").GetComponent<TMPro.TMP_Text>();
        CaO_Text = GameObject.Find("Percent 7").GetComponent<TMPro.TMP_Text>();
        K2O_Text = GameObject.Find("Percent 8").GetComponent<TMPro.TMP_Text>();
        P2O3_Text = GameObject.Find("Percent 9").GetComponent<TMPro.TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        numberScanned.SetActive(false);
        geoLoadingComplete.SetActive(false);
        compositionInfo.SetActive(false);
    }

    public void SpecMsgUpdateCallback(SpecMsg msg)
    {
        for (int i = 0; i < sampleSpecs.Length; i++)
        {
            if (
                sampleSpecs[i].SiO2 == msg.SiO2 &&
                sampleSpecs[i].TiO2 == msg.TiO2 &&
                sampleSpecs[i].Al2O3 == msg.Al2O3 &&
                sampleSpecs[i].FeO == msg.FeO &&
                sampleSpecs[i].MnO == msg.MnO &&
                sampleSpecs[i].MgO == msg.MgO &&
                sampleSpecs[i].CaO == msg.CaO &&
                sampleSpecs[i].K2O == msg.K2O &&
                sampleSpecs[i].P2O3 == msg.P2O3
            )
            {
                missionNum_Text.text = "Mission #" + sampleMetadatas[i].MissionNum.ToString();
                
                return;
            }
        }
    }
}
