using System.Collections.Generic;
using UnityEngine;
using Architecture;
using System;
using System.Linq;

public class ButtonsInteractor : Interactor
{
    private List<Clickable> buttons;

    public T GetButton<T>() where T : Clickable
    {
        foreach(var b in buttons) 
        { 
            if(b.GetComponent<T>()!=null) return b.GetComponent<T>();
        }
        return null;
    }  

    public void SetButtonsArray(Clickable[] buttons)
    {
        this.buttons = buttons.ToList();
    }
}
