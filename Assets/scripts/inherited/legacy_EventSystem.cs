using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EventSystem_valueManager : MonoBehaviour
{
    public GameObject spinSpeed_Object;
    public GameObject pourTime_Object;
    public GameObject linearSpeed_Object;
    public GameObject range_Object;
    public GameObject drawer_Object;
    public GameObject FlowByMass_Object;

    public float spinSpeed;
    public float pourTime;
    public float linearSpeed;
    public float range_lower;
    public float range_upper;
    public float flow;
    public float mass;

    private TMP_Text spinSpeed_text;
    private TMP_Text pourTime_text;
    private TMP_Text linearSpeed_text;
    private TMP_Text range_lower_text;
    private TMP_Text range_upper_text;
    private TMP_Text flow_text;
    private TMP_Text mass_text;

    private Drawer drawer;

    // selection-to-flow Array, by index=selection, value={value, name}
    public float[] selectToFlowValue = new float[16];
    public string[] selectToFlowString = new string[16];



    // Start is called before the first frame update
    void Start()
    {
        // cachings


        spinSpeed_Object.transform.GetChild(0).GetComponent<Slider>().onValueChanged.AddListener(onspinSpeedChanged);
        pourTime_Object.transform.GetChild(0).GetComponent<InputField>().onValueChanged.AddListener(onpourTimeChanged);
        linearSpeed_Object.transform.GetChild(0).GetComponent<Slider>().onValueChanged.AddListener(onlinearSpeedChanged);
        range_Object.transform.GetChild(0).GetChild(1).GetComponent<Slider>().onValueChanged.AddListener(onLowerRangeChanged);
        range_Object.transform.GetChild(0).GetChild(2).GetComponent<Slider>().onValueChanged.AddListener(onUpperRangeChanged);
        FlowByMass_Object.transform.GetChild(0).GetComponent<Slider>().onValueChanged.AddListener(onFlowChanged);
        FlowByMass_Object.transform.GetChild(1).GetComponent<InputField>().onValueChanged.AddListener(onMassChanged);

        spinSpeed_text = spinSpeed_Object.transform.GetChild(2).GetComponent<TMP_Text>();
        pourTime_text = pourTime_Object.transform.GetChild(2).GetComponent<TMP_Text>();
        linearSpeed_text = linearSpeed_Object.transform.GetChild(2).GetComponent<TMP_Text>();
        range_lower_text = range_Object.transform.GetChild(2).GetComponent<TMP_Text>();
        range_upper_text = range_Object.transform.GetChild(3).GetComponent<TMP_Text>();
        flow_text = FlowByMass_Object.transform.GetChild(4).GetComponent<TMP_Text>();
        mass_text = FlowByMass_Object.transform.GetChild(5).GetComponent<TMP_Text>();

        drawer = drawer_Object.GetComponent<Drawer>();

        // initial signal sending
        spinSpeed_Object.transform.GetChild(0).GetComponent<Slider>().value = spinSpeed;
        pourTime_Object.transform.GetChild(0).GetComponent<InputField>().text = pourTime.ToString();
        linearSpeed_Object.transform.GetChild(0).GetComponent<Slider>().value = linearSpeed;
        range_Object.transform.GetChild(0).GetChild(1).GetComponent<Slider>().value = range_lower;
        range_Object.transform.GetChild(0).GetChild(2).GetComponent<Slider>().value = range_upper;
        FlowByMass_Object.transform.GetChild(0).GetComponent<Slider>().value = flow;
        FlowByMass_Object.transform.GetChild(1).GetComponent<InputField>().text = mass.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void onspinSpeedChanged(float value)
    {
        spinSpeed = value;
        spinSpeed_text.text = value.ToString();
        drawer.isStartSimulation = true;
    }

    void onpourTimeChanged(string value)
    {
        float tryparse = 0.0f;
        if (float.TryParse(value, out tryparse) )
        {
            pourTime = tryparse;
            pourTime_text.text = (Mathf.Round(tryparse * 100)/100).ToString();
            drawer.isStartSimulation = true;
        }
    }

    void onlinearSpeedChanged(float value)
    {
        linearSpeed = value;
        linearSpeed_text.text = value.ToString();
        drawer.isStartSimulation = true;
    }

    void onLowerRangeChanged(float value)
    {
        range_lower = value;
        range_lower_text.text = value.ToString();
        drawer.isStartSimulation = true;
    }

    void onUpperRangeChanged(float value)
    {
        range_upper = value;
        range_upper_text.text = value.ToString();
        drawer.isStartSimulation = true;
    }

    void onFlowChanged(float value)
    {
        // changed the flow text
        int index = (int)value;
        flow_text.text = selectToFlowString[index];

        // change the time section
        flow = selectToFlowValue[index];
        if (mass > 0)
        {
            float calculated_time = mass / flow;
            onpourTimeChanged((Mathf.Round(calculated_time * 100) / 100).ToString().ToString());
        }
    }

    void onMassChanged(string value)
    {
        float tryparse = 0.0f;
        if (float.TryParse(value, out tryparse))
        {
            mass = tryparse;
            mass_text.text = tryparse.ToString();
            // if flow available, changes time.
            if (flow > 0)
            {
                float calculated_time = mass / flow;
                onpourTimeChanged((Mathf.Round(calculated_time * 100) / 100).ToString().ToString());
            }
        }
    }
}
