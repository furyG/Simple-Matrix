using Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tapes
{
    public class Tape : MonoBehaviour
    {
        public event Action<Tape> tapeClicked;
        public List<Numberable> numberables => _numberables;
        public Numberable Number => _currentNumber;

        private List<Numberable> _numberables;
        private Numberable _currentNumber;
        private float bonusSpawnChance;
        private float spawnNumberTime;
        private float numberRunDuration;

        private Balance balance = Balance.instance;
        private StateMachine mainStateMachine;
        private TimerInteractor timerInteractor;

        private void Awake()
        {
            timerInteractor = Game.GetInteractor<TimerInteractor>();

            mainStateMachine = C.main.MainStateMachine;
        }
        private void Start()
        {
            spawnNumberTime = balance.SpawnNumbersTime;
            bonusSpawnChance = balance.BonusSpawnChance;
            numberRunDuration = balance.NumberRunDuration;

            _numberables = new List<Numberable>();
            Invoke(nameof(SpawnNumber), spawnNumberTime);

            if(mainStateMachine != null)
            {
                mainStateMachine.onStateChanged += OnStateChangedEvent;
            }
        }

        private void OnStateChangedEvent(IState state)
        {
            if(state == mainStateMachine.countState)
            {
                Disable();
            }
        }
        private void Disable()
        {
            mainStateMachine.onStateChanged -= OnStateChangedEvent;

            CancelInvoke(nameof(SpawnNumber));
            if(Number != null)
            {
                Number?.StopAllCoroutines();
                Destroy(Number.gameObject);
            }
            numberables.Clear();
        }
        private void SpawnNumber()
        {
            timerInteractor.StartRoundTimer();

            _currentNumber = TapeObjectsFactory.instance.Get<Number>(transform);

            float chance = UnityEngine.Random.Range(0f, 1f);
            if (chance < bonusSpawnChance)
            {
                TapeObjectsFactory.instance.Get<Bonus>(transform);
            }
            Invoke(nameof(SpawnNumber), numberRunDuration + spawnNumberTime);
        }

        private void OnMouseDown()
        {
            if (Number == null) return;

            _currentNumber.SetNumberable();

            SetNumberOnTape();

            tapeClicked?.Invoke(this);
            _currentNumber = null;

            CancelInvoke(nameof(SpawnNumber));
            Invoke(nameof(SpawnNumber), spawnNumberTime);
        }

        private void SetNumberOnTape()
        {
            _numberables = new List<Numberable>();

            float buffer = _currentNumber.transform.localScale.x / 2 + 0.1f;
            float currentNumberX = _currentNumber.transform.localPosition.x;

            _numberables = transform.GetComponentsInChildren<Numberable>().ToList();

            if (_numberables.Count > 1)
            {
                Numberable closestNumber = GetClosest(currentNumberX);
                float closestNumberX = closestNumber.transform.localPosition.x;

                float delta = Mathf.Abs(closestNumberX - currentNumberX);
                if (delta < buffer)
                {
                    ChangeNumberOnPos(closestNumber);
                }
            }
            SortChildren();
        }

        private void SortChildren()
        {
            _numberables = _numberables.OrderBy(x => x.transform.localPosition.x).ToList();

            for (int i = 0; i < _numberables.Count; i++)
            {
                for (int j = 0; j < transform.childCount; j++)
                {
                    Transform child = transform.GetChild(j);
                    float childX = child.localPosition.x;

                    Numberable numberable = _numberables[i];
                    float numberableX = _numberables[i].transform.localPosition.x;

                    if (numberableX == childX)
                    {
                        child.SetSiblingIndex(_numberables.IndexOf(numberable));
                    }
                }
            }
        }

        private bool CanSpawnZero(float x)
        {
            int index = _numberables.IndexOf(_numberables.FirstOrDefault(index => index.transform.localPosition.x == x));

            if (_numberables.Count <= index || index == -1) return true;

            return !_numberables[index];
        }
        private Numberable GetClosest(float numberX)
        {
            Numberable closest = _numberables[0];

            foreach(var number in _numberables)
            {
                if (number.Equals(_currentNumber)) continue;

                float closestX = closest.transform.localPosition.x;
                float onTapeNumberX = number.transform.localPosition.x;

                if (Mathf.Abs(onTapeNumberX - numberX) < Mathf.Abs(closestX - numberX))
                {
                    closest = number;
                }
            }
            return closest;
        }
        private void ChangeNumberOnPos(Numberable closest)
        {
            Vector2 newNumPos = new(closest.transform.localPosition.x, 0);
            _currentNumber.transform.localPosition = newNumPos;

            int closestIndex = _numberables.IndexOf(closest);
            Destroy(_numberables[closestIndex].gameObject);
            _numberables.RemoveAt(closestIndex);

            _numberables.Insert(closestIndex, _currentNumber);
            _numberables.RemoveAt(closestIndex);
        }
        public void TryAddZero(float x)
        {
            if (!CanSpawnZero(x)) return;

            Zero zero = TapeObjectsFactory.instance.Get<Zero>(transform);
            zero.transform.localPosition = new(x, 0);
            zero.transform.SetSiblingIndex(numberables.IndexOf(numberables.FirstOrDefault(index => index.transform.localPosition.x == x)));
            _numberables.Add(zero);

            SortChildren();
        }
    }
}
