using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class needle : MonoBehaviour
{
    public bool attached = true;
    private int index;

    //cache
    private centerPoint mother;
    private Transform mother_transform;

    // Start is called before the first frame update
    void Start()
    {
        mother_transform = transform.parent;
        mother = mother_transform.GetComponent<centerPoint>();
        //find index of self in mother.isNeedleActivated.
        for (int i = 0; i < mother_transform.childCount; i++)
        {
            Transform target = mother_transform.GetChild(i);
            if (GameObject.ReferenceEquals(target.gameObject, gameObject))
            {
                index = i - 1;
                break;
            }
        }

        OnMouseDown();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (attached)
        {
            Color temp = GetComponent<SpriteRenderer>().color;
            temp.a = 0.2f;
            GetComponent<SpriteRenderer>().color = temp;

            mother.isNeedleActivated[index] = false;

            attached = false;
        } else
        {
            Color temp = GetComponent<SpriteRenderer>().color;
            temp.a = 1.0f;
            GetComponent<SpriteRenderer>().color = temp;

            mother.isNeedleActivated[index] = false;
            attached = true;
        }

    }
}
