using UnityEngine;

public abstract class RendererBase : MonoBehaviour
{
    public virtual void ChangeColor(Color color) { }
    public virtual void ChangeColor32(Color32 color) { }

    public virtual void ChangeSprite(Sprite sprite) { }
    public virtual void ChangeFont(int size) { }
}
