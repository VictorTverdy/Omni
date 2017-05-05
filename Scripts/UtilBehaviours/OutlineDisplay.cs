using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class OutlineDisplay : MonoBehaviour {
    [SerializeField] public bool displayWhenMouseOver;
    //[SerializeField] public Outline[] vOutlines;

    public List<Outline> lOutline;

    void Awake()
    {
        if (displayWhenMouseOver)
            display(false);
    }
    
    public void SetDisable()
    {
        displayWhenMouseOver = false;
        display(false);
    }


    public void SetEnable()
    {
        displayWhenMouseOver = true;
    }

    void OnMouseEnter()
    {
        if(displayWhenMouseOver)
        display(true);
    }

    void OnMouseExit()
    {
        if (displayWhenMouseOver)
            display(false);
    }

    private void display(bool v)
    {
        for (int i = 0; i < lOutline.Count; i++)
        {
            lOutline[i].enabled = v;
        }
    
    }
}
