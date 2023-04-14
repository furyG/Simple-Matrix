using Architecture;
using UnityEngine;
using System.Collections.Generic;

namespace Tapes
{
    public class TapeSpawner : MonoBehaviour
    {
        public List<TapeManager> tapes => _tapes;
        private List<TapeManager> _tapes;
        private int tapesAmount
        {
            get => _tapesAmount;
            set
            {
                _tapesAmount = value;
                _tapesAmount = Mathf.Clamp(value, Balance.instance.StartTapesAmount, 5);
            }
        }
        private int _tapesAmount;

        private int n = 0;

        private StateMachine mainStateMachine;
        private LevelInteractor levelInteractor;
        private TapesHandler _tapesHandler;

        private void Start()
        {
            tapesAmount = Balance.instance.StartTapesAmount;
            levelInteractor = Game.GetInteractor<LevelInteractor>();

            mainStateMachine = C.main.MainStateMachine;

            if(mainStateMachine != null)
            {
                mainStateMachine.onStateChanged += OnStateChanged;
            }
            if(levelInteractor != null)
            {
                levelInteractor.levelChanged += OnLevelChanged;
            }

            _tapesHandler = new TapesHandler(this);
        }

        private void OnDisable()
        {
            if(mainStateMachine != null)
            {
                mainStateMachine.onStateChanged -= OnStateChanged;
            }
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
            }
        }

        private void SpawnTapes()
        {
            _tapes = new List<TapeManager>();
            for (int i = tapesAmount - 1; i > -1; i--)
            {
                SpawnTape();
            }
        }
        private TapeManager SpawnTape()
        {
            TapeManager t = TapeObjectsFactory.instance.Get<TapeManager>(transform);
            _tapes.Add(t);
            
            n++;
            t.gameObject.name = n.ToString();

            _tapesHandler.Subscribe(t);

            return t;
        }
        private void OnStateChanged(IState state)
        {
            if(state == mainStateMachine.playState)
            {
                for(int i = 0; i < transform.childCount; i++)
                {
                    _tapesHandler?.Unsubscribe(tapes[i]);
                    Destroy(transform.GetChild(i).gameObject);
                }

                SpawnTapes();
            }
            if(state == mainStateMachine.countState)
            {
                foreach(var tape in _tapes)
                {
                    tape.StopTape();
                }
            }
        }
    }
}



