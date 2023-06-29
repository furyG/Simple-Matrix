using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Tile : MonoBehaviour
{
    public TileNeighbour tileNeigbour { get; private set; }

    [SerializeField] private BaseColors baseColors;
    [SerializeField] private Image blockerImage;

    private TileAnimationHandler _tileAnimationHandler;
    private TextMeshProUGUI _vizualizer;

    private void Awake()
    {
        _tileAnimationHandler = GetComponent<TileAnimationHandler>();
        _vizualizer = GetComponent<TextMeshProUGUI>();
        _vizualizer.color = baseColors.gray;

        blockerImage.color = baseColors.gray;

        tileNeigbour = new TileNeighbour(this);     
    }
    private void OnEnable()
    {
        if (_tileAnimationHandler)
        {
            _tileAnimationHandler.OnTakeAnimationOver += OnTakeAnimationOverEvent;
        }
    }
    private void OnDisable()
    {
        if (_tileAnimationHandler)
        {
            _tileAnimationHandler.OnTakeAnimationOver -= OnTakeAnimationOverEvent;
        }
    }
    private void OnTakeAnimationOverEvent()
    {
        tileNeigbour.ClearTileNumber();
    }
    public void PlayTakingAnimation()
    {
        _tileAnimationHandler.PlayTaking();
    }
    public void ClearNumberRender()
    {
        _vizualizer.text = string.Empty;
    }
    public void SetNumberRender(int number)
    {
        _vizualizer.text = number.ToString();
    }
    public void BlockTile()
    {
        blockerImage.enabled = true;
    }
    public void UnblockTile()
    {
        blockerImage.enabled = false;
    }
}
