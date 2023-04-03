using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : Clickable
{
    private bool paused = true;

    protected override void OnClick()
    {
        base.OnClick();

        paused = !paused;
        Time.timeScale = Convert.ToInt32(paused);
    }
}
