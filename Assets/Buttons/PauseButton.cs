using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseButton : Clickable
{
    protected override ButtonType type => ButtonType.Pause;

    protected override void OnClick()
    {
        Time.timeScale = Time.timeScale <= 0 ? 1 : 0;
    }
}
