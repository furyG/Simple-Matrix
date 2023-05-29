using System.Collections.Generic;
using UnityEngine;

namespace Tapes
{
    public class TapeSpawner : MonoBehaviour
    {
        private List<TapeManager> _tapes;
        private List<List<TileNeighbour>> _tilesMatrix;
        private TapeComplicationHandler _complicationHandler;
        private TilesBlockHandler _tileBlockHandler;
        private int n = 0;

        private void Start()
        {
            _tileBlockHandler = new TilesBlockHandler();
            _complicationHandler = new TapeComplicationHandler(this, _tileBlockHandler);
        }
        private void OnDestroy()
        {
            _complicationHandler?.OnDestroy();
        }

        public void ClearTapes()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            n = 0;
        }
        public TapeManager SpawnTape()
        {
            TapeManager t = TapeObjectsFactory.GetInstance().Get<TapeManager>(transform);
            _tilesMatrix.Add(t.GetComponent<TapeContentSpawner>().SpawnTiles());
            _tapes.Add(t);

            n++;
            t.gameObject.name = n.ToString();

            return t;
        }
        public void InitializeTileNeighbours()
        {
            for(int y = 0; y < _tilesMatrix.Count; y++)
            {
                for(int x = 0; x < _tilesMatrix[y].Count; x++)
                {
                    _tilesMatrix[y][x].InitializeTileNeighbour(x, y, _tilesMatrix);
                }
            }
            _tileBlockHandler.tileNeighbours = _tilesMatrix;
        }
        public void StopTapes()
        {
            if (_tapes == null) return;

            foreach (var tape in _tapes)
            {
                tape.StopTape();
            }

            _tileBlockHandler.OnTapesStop();
        }
        public void SpawnTapes(bool newGame)
        {
            _tilesMatrix = new List<List<TileNeighbour>>();
            _tapes = new List<TapeManager>();

            if (newGame)
            {
                _complicationHandler.OnNewGame();
            }

            int tapesAmount = _complicationHandler.tapesAmount;

            for (int i = tapesAmount - 1; i > -1; i--)
            {
                SpawnTape();
            }

            InitializeTileNeighbours();
        }
    }
}



