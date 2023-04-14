using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IRenderer
{
    Image image { get; }
    void InitializeSprites();
    void ChangeColor(Color color);
    void ChangeSprite(Sprite sprite);
    void ChangeSprite(int number);
}
