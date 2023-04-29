using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Base Colors", menuName = "Gameplay/Colors")]
public class BaseColors : ScriptableObject
{
    [SerializeField] private Color32 _gray;
    [SerializeField] private Color32 _black;
    [SerializeField] private Color32 _orange;
    [SerializeField] private Color32 _backgroundWhite;

    public Color32 gray => _gray;
    public Color32 black => _black;
    public Color32 orange => _orange;
    public Color32 backgroundWhite => _backgroundWhite;

}
