using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawmanager_linedrawer: MonoBehaviour
{
    public GameObject linePreFab;
    [HideInInspector]
    public List<GameObject> currentLineList = new List<GameObject>();
    [HideInInspector]
    public List<LineRenderer> lineRendererList = new List<LineRenderer>();  

    // position list mamagement
    private List<List<Vector2>> targetPosList = new List<List<Vector2>>();

    private float time;
    //[HideInInspector]
    public bool isStartSimulation = false; // if EventSystem UI value's changed, this bool value will be toggled.
    public bool isInSimulation = false;

    // caches
    private List<Transform> childNeedles = new List<Transform>();

    private void Awake()
    {

    }


    // Start is called before the first frame update
    void Start()
    {   
        // action of collecting childNeedls must be in the start code, not awake. (because needle prefab instantiation is in the awake section)
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            for (int j = 1; j < child.childCount; j++)
            {
                Transform needle = child.GetChild(j);
                childNeedles.Add(needle);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        // if durning Simulation, keep process the simulation
        if (isStartSimulation == false && isInSimulation == true)
        {
            UpdateLine();
        }
        else if (isStartSimulation == true)
        {
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
        targetPosList.Clear();
        currentLineList.Clear();
        lineRendererList.Clear();
        for (int i=0; i<childNeedles.Count;i++)
        {
            Transform child = childNeedles[i];
            GameObject targetLine = Instantiate(linePreFab, Vector3.zero, Quaternion.identity);
            targetLine.transform.position = child.position;
            currentLineList.Add(targetLine);
            lineRendererList.Add(targetLine.GetComponent<LineRenderer>());
            Vector2 initialpos = new Vector2(child.position.x, child.position.y);
            targetPosList.Add(new List<Vector2>());
            targetPosList[i].Add(initialpos);
            targetPosList[i].Add(initialpos);
            lineRendererList[i].SetPosition(0, targetPosList[i][0]);
            lineRendererList[i].SetPosition(1, targetPosList[i][1]);
        }
    }

    private void DestroyLine()
    {
        for (int i=0;i< currentLineList.Count;i++)
        {
            Destroy(currentLineList[i]);
        }
    }

    private void UpdateLine()
    {
        for (int i = 0; i < childNeedles.Count; i++)
        {
            Transform child = childNeedles[i];
            Vector2 newTargetPos = new Vector2(child.position.x, child.position.y);
            targetPosList[i].Add(newTargetPos);
            lineRendererList[i].positionCount++;
            lineRendererList[i].SetPosition(lineRendererList[i].positionCount - 1, newTargetPos);
        }
    }
}
