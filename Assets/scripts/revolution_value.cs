using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class revolution_value : MonoBehaviour
{
    public GameObject drawManager;
    private DrawManager drawScript;
    private void Awake()
    {
        drawScript = drawManager.GetComponent<DrawManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TMP_Text>().text = (Mathf.Round(drawScript.turn_count*100)/100).ToString();
    }
}
