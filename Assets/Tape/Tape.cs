using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tapes
{
    public class Tape : SpawnableObject
    {
        public event Action<Tape> tapeClicked;
        public override SpawnableType Type => SpawnableType.Tape;
        public List<Numberable> numberables => _numberables;
        public Numberable Number => currentNumber;

        private Numberable currentNumber;
        private float bonusSpawnChance;
        private float spawnNumberTime;
        [SerializeField] private List<Numberable> _numberables;
        [SerializeField] private float[] numPoses;

        private StateMachine mainStateMachine;

        private void Awake()
        {
            spawnNumberTime = Balance.instance.SpawnNumbersTime;
            bonusSpawnChance = Balance.instance.BonusSpawnChance;

            mainStateMachine = C.main.MainStateMachine;
            if(mainStateMachine != null)
            {
                mainStateMachine.stateChanged += OnStateChangedEvent;
            }
        }
        private void Start()
        {
            _numberables = new List<Numberable>();
            Invoke(nameof(SpawnNumber), spawnNumberTime);
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
            mainStateMachine.stateChanged -= OnStateChangedEvent;

            CancelInvoke(nameof(SpawnNumber));
            if(Number != null)
            {
                Number?.StopAllCoroutines();
                Destroy(Number.gameObject);
            }
        }
        private void SpawnNumber()
        {
            currentNumber = TapeContentFabric.instance.GetAndSetParent(SpawnableType.Number, transform) as Number;

            //float chance = UnityEngine.Random.Range(0f, 1f);
            //if (chance < bonusSpawnChance)
            //{
            //    TapeContentFabric.instance.GetAndSetParent(SpawnableType.Bonus, transform);
            //}

            Invoke(nameof(SpawnNumber), spawnNumberTime);
        }

        private void OnMouseDown()
        {
            if (Number == null) return;

            currentNumber.boarded = true;
            currentNumber.StopAllCoroutines();

            SetNumberOnTape();

            tapeClicked?.Invoke(this);
            currentNumber = null;
        }

        private void SetNumberOnTape()
        {
            _numberables = new List<Numberable>();

            float buffer = currentNumber.transform.localScale.x / 2 + 0.1f;
            float currentNumberX = currentNumber.transform.localPosition.x;

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
            int index = numberables.IndexOf(numberables.FirstOrDefault(index => index.transform.localPosition.x == x));

            if (_numberables.Count <= index || index == -1) return true;

            return !_numberables[index];
        }
        private Numberable GetClosest(float numberX)
        {
            Numberable closest = _numberables[0];

            foreach(var number in _numberables)
            {
                if (number.Equals(currentNumber)) continue;

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
            currentNumber.transform.localPosition = newNumPos;

            int closestIndex = _numberables.IndexOf(closest);
            Destroy(_numberables[closestIndex].gameObject);
            _numberables.RemoveAt(closestIndex);

            _numberables.Insert(closestIndex, currentNumber);
            _numberables.RemoveAt(closestIndex);
        }
        public void TryAddZero(float x)
        {
            if (!CanSpawnZero(x)) return;

            Transform z = TapeContentFabric.instance.GetAndSetParent(SpawnableType.Zero, transform).transform;
            z.localPosition = new(x, 0);
            z.SetSiblingIndex(numberables.IndexOf(numberables.FirstOrDefault(index => index.transform.localPosition.x == x)));
            _numberables.Add(z.GetComponent<Numberable>());

            SortChildren();
        }
    }
}
