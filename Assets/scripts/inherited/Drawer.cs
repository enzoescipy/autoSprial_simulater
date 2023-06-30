using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{
    public GameObject EventSystem;
    private EventSystem_valueManager EventSyetem_script;

    // Drawer will simulate the nozzle's movement, then draw the orbit of the nozzle.
    public GameObject Nozzle;
    private Transform Nozzle_transform;
    // the nozzle body simulates the circular motion, and the nozzle simulates the linear motion.
    public GameObject Nozzle_body;
    private Transform Nozzle_body_transform;

    public GameObject linePreFab;
    [HideInInspector]
    public GameObject currentLine;
    [HideInInspector]
    public LineRenderer lineRenderer;
    //[HideInInspector]
    public List<Vector2> targetPosList;

    private float time;
    //[HideInInspector]
    public bool isStartSimulation = false; // if EventSystem UI value's changed, this bool value will be toggled.
    private bool isInSimulation = false;

    // cached EventSystem's variables , saved in integer form
    private int spinSpeed;
    private int pourTime;
    private int linearSpeed;
    private int range_lower;
    private int range_upper;
    // physical simulation needed values
    private int linear_direction = 1;

    // spinspeed to actuall degrees per second array
    public float[] spinspeedByDegree = new float[11];


    // Start is called before the first frame update
    void Start()
    {
        EventSyetem_script = EventSystem.GetComponent<EventSystem_valueManager>();

        Nozzle_transform = Nozzle.transform;
        Nozzle_body_transform = Nozzle_body.transform;

        //// degrees to radian
        //for (int i=0; i<11; i++)
        //{
        //    spinspeedByRadian[i] = spinspeedByRadian[i] / 180 * Mathf.PI;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        // if durning Simulation, keep process the simulation
        if (isStartSimulation == false && isInSimulation == true)
        {
            UpdateLine();
            if (time > pourTime)
            {
                isInSimulation = false;
            }
        }
        else if (isStartSimulation == true)
        {
            // anyway simulation started, brings up the simulation variables.
            spinSpeed = (int) EventSyetem_script.spinSpeed;
            pourTime = (int) EventSyetem_script.pourTime;
            linearSpeed = (int) EventSyetem_script.linearSpeed;
            range_lower = (int) EventSyetem_script.range_lower;
            range_upper = (int) EventSyetem_script.range_upper;

            // create line and initiate nozzle's position for the given simulation values.
            DestroyLine();
            CreateLine();

            // if isStartSimulation triggered durning simulation, restart the simulation
            if (isInSimulation == true)
            {
                isInSimulation = false;
                time = 0;
                // sth code that make line to zero to zero
            }

            // if isStartSimulation triggered before simulation, start the simulation
            else if (isInSimulation == false)
            {
                isStartSimulation = false;
                isInSimulation = true;
                time = 0;
            }
        }

    }

    private void CreateLine()
    {
        // initiate the nozzle and nozzle_body position
        Nozzle_body_transform.position = Vector3.zero;
        Nozzle_transform.position = Vector3.zero;

        // calculate the initial position of the nozzle
        Nozzle_transform.Translate(new Vector3(range_lower, 0, 0));
        Vector2 initial_pos2 = new Vector2(Nozzle_transform.position.x, Nozzle_transform.position.y);

        currentLine = Instantiate(linePreFab, Vector3.zero, Quaternion.identity); // instantiate new line
        lineRenderer = currentLine.GetComponent<LineRenderer>();
        // make line represents current position of mouse. adds two points because 2 is minimum length of linerenderer pos array.
        targetPosList.Clear();
        targetPosList.Add(initial_pos2);
        targetPosList.Add(initial_pos2);
        lineRenderer.SetPosition(0, targetPosList[0]);
        lineRenderer.SetPosition(1, targetPosList[1]);
    }

    private void DestroyLine()
    {
        Destroy(currentLine);
    }

    private void UpdateLine()
    {
        // move forward the nozzle.
        float move_amount = linear_direction * linearSpeed * Time.deltaTime;
        float current_x = Nozzle_transform.localPosition.x;
        float sum = move_amount + current_x;
        if (range_lower < sum && sum < range_upper)
        {
            Nozzle_transform.Translate(new Vector3(move_amount, 0, 0));
        }
        else
        {
            Nozzle_transform.Translate(new Vector3(-move_amount, 0, 0));
            linear_direction = linear_direction * -1;
        }

        // rotate the nozzle_body
        float rotation_amount = spinspeedByDegree[spinSpeed] * Time.deltaTime;
        Nozzle_body_transform.Rotate(0, 0, rotation_amount);

        // get the current nozzle's world position and send it to lineRenderer.SetPosition
        Vector2 newTargetPos = new Vector2(Nozzle_transform.position.x, Nozzle_transform.position.y);
        targetPosList.Add(newTargetPos);
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, newTargetPos);
    }
}
