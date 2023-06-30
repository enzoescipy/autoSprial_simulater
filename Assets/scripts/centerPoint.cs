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
    public float ratio;
    public float distance;
    public bool[] isNeedleActivated;

    private void Awake()
    {
        drawManager = transform.parent.GetComponent<DrawManager>();
        AreaHighlight_transform = transform.GetChild(0);
        needle = drawManager.needle_prefab;

        transform.Translate(distance, 0, 0); // initialize distance
        AreaHighlight_transform.localScale = Vector3.one * (drawManager.mainRadious - distance); //initialize view of gear
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
            trans.localPosition = new Vector3(Mathf.Cos(i * revolute_amount) * radious, Mathf.Sin(i * revolute_amount) * radious, 0);
        }

    }
}
