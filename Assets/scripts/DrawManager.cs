using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
    //caches
    private List<GameObject> childCenterPoints = new List<GameObject>();

    public GameObject needle_prefab; // prefab needle

    public float mainRadious; // size of the whole mechanics
    public float mainRatio;

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            childCenterPoints.Add(transform.GetChild(i).gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //debug
        mainRevolution(100 * Time.deltaTime);
    }

    public void mainRevolution(float revolution)
    {
        transform.Rotate(0, 0, revolution);
        
        for (int i=0; i < childCenterPoints.Count; i++)
        {
            GameObject child = childCenterPoints[i];
            centerPoint script = child.GetComponent<centerPoint>();
            script.revolute_gear(-mainRatio * revolution / script.ratio);
        }
    }
}
