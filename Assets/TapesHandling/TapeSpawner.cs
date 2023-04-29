using Architecture;
using UnityEngine;
using System.Collections.Generic;

namespace Tapes
{
    public class TapeSpawner : MonoBehaviour
    {
        private List<TapeManager> _tapes;

        private List<List<TileNeighbour>> _tilesMatrix;

        private int tapesAmount
        {
            get => _tapesAmount;
            set
            {
                _tapesAmount = value;
                _tapesAmount = Mathf.Clamp(value, Balance.GetInstance().StartTapesAmount, 5);
            }
        }
        private int _tapesAmount;
        

        private int n = 0;
        private LevelInteractor levelInteractor;

        private void Awake()
        {
            tapesAmount = Balance.GetInstance().StartTapesAmount;
            levelInteractor = Game.GetInteractor<LevelInteractor>();
        }

        private void Start()
        {
            if(levelInteractor != null)
            {
                levelInteractor.levelChanged += OnLevelChanged;
            }
        }

        private void OnDisable()
        {
            if(levelInteractor != null)
            {
                levelInteractor.levelChanged -= OnLevelChanged;
            }
        }

        private void OnLevelChanged(int level)
        {
            if (level == 3)
            {
                SpawnTape();
                InitializeTileNeighbours();
            }
        }

        private TapeManager SpawnTape()
        {
            TapeManager t = TapeObjectsFactory.GetInstance().Get<TapeManager>(transform);
            _tilesMatrix.Add(t.GetComponent<TapeContentSpawner>().SpawnTiles());
            _tapes.Add(t);

            n++;
            t.gameObject.name = n.ToString();

            return t;
        }
        private void ClearTapes()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
        private void InitializeTileNeighbours()
        {
            for(int y = 0; y < _tilesMatrix.Count; y++)
            {
                for(int x = 0; x < _tilesMatrix[y].Count; x++)
                {
                    _tilesMatrix[y][x].InitializeTileNeighbour(x, y, _tilesMatrix);
                }
            }
        }
        public void StopTapes()
        {
            foreach (var tape in _tapes)
            {
                tape.StopTape();
            }
        }
        public void SpawnTapes()
        {
            ClearTapes();

            _tilesMatrix = new List<List<TileNeighbour>>();
            _tapes = new List<TapeManager>();
            for (int i = tapesAmount - 1; i > -1; i--)
            {
                SpawnTape();
            }

            InitializeTileNeighbours();
        }
    }
}



