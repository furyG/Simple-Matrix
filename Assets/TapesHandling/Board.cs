using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tapes
{
    public class Board : MonoBehaviour
    {
        private List<TapeManager> _tapes;
        private List<List<TileNeighbour>> _tilesMatrix;
        private BoardComplicationHandler _boardComplicationHandler;
        private TilesBlockHandler _tileBlockHandler;
        private int n = 0;

        private void OnDestroy()
        {
            _boardComplicationHandler?.OnDestroy();
        }

        public void Init()
        {
            _tileBlockHandler = new TilesBlockHandler();
            _boardComplicationHandler = new BoardComplicationHandler(this, _tileBlockHandler);

            SpawnTapes();
        }

        public void ClearTapesSpawn()
        {
            _tileBlockHandler.OnTapesClear();
            foreach(var tape in _tapes)
            {
                tape.ClearTapeContent();
            }
            StopTapesContentSpawning();
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
        public void StartTapesContentSpawning()
        {
            if (_tapes == null) return;
            foreach(var tape in _tapes)
            {
                tape.StartContentSpawning();
            }
        }
        public void OnNewGame()
        {
            ClearTapesSpawn();
            if(_tapes.Count > 3)
            {
                _boardComplicationHandler.OnNewGame();
                Destroy(_tapes.Last().gameObject);
                _tapes.Remove(_tapes.Last());
            }
        }
        private void StopTapesContentSpawning()
        {
            if (_tapes == null) return;

            foreach (var tape in _tapes)
            {
                tape.StopContentSpawning();
            }
        }
        private void SpawnTapes()
        {
            _tilesMatrix = new List<List<TileNeighbour>>();
            _tapes = new List<TapeManager>();

            int tapesAmount = _boardComplicationHandler.tapesAmount;

            for (int i = tapesAmount - 1; i > -1; i--)
            {
                SpawnTape();
            }

            InitializeTileNeighbours();
        }
    }
}



