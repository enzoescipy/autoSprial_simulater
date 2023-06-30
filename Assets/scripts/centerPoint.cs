using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centerPoint : MonoBehaviour
{
    //caching
    private DrawManager drawManager;
    private Transform AreaHighlight_transform;
    private GameObject needle;

    public float radious;
    public float outerOffset; // offset from outer shell. if positive value, center position will be shifted inside by this amount.
    public float ratio;
    public float needleRadious;
    public float needleAngleOffset = 0;
    public bool isHighlighted = true;
    public bool[] isNeedleActivated;

    private void Awake()
    {
        drawManager = transform.parent.GetComponent<DrawManager>();
        AreaHighlight_transform = transform.GetChild(0);
        needle = drawManager.needle_prefab;

        float distance = drawManager.mainRadious - radious - outerOffset;
        transform.Translate(distance, 0, 0); // initialize distance
        AreaHighlight_transform.localPosition = Vector3.zero;
        AreaHighlight_transform.localScale = Vector3.one * radious * 2; //initialize view of gear
        if (isHighlighted == false)
        {
            AreaHighlight_transform.GetComponent<Renderer>().enabled = false;   
        }
        needle_instantiate(); // instantiate the neddles
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void revolute_gear(float revolution)
    {
        transform.Rotate(0, 0, revolution);
    }

    private void needle_instantiate()
    {
        float length = isNeedleActivated.Length;
        float revolute_amount = 2*Mathf.PI/length;
        for (int i=0; i<length; i++) 
        {
            if (isNeedleActivated[i] == false)
            {
                continue;
            }
            GameObject newNeedle = Instantiate(needle, Vector3.zero, Quaternion.identity);
            Transform trans = newNeedle.transform;
            trans.SetParent(transform);
            trans.localPosition = new Vector3(Mathf.Cos(i * revolute_amount + needleAngleOffset) * needleRadious, Mathf.Sin(i * revolute_amount + needleAngleOffset) * needleRadious, 0);
        }

    }
}
