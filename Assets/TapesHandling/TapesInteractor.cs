using Architecture;
using System;
using System.Collections.Generic;
using System.Linq;
using Tapes;
using UnityEngine;

public class TapesInteractor : Interactor
{
    public event Action<List<List<Numberable>>> tapesChanged;

    private TapesRepository repository;
    private PointsInteractor pointsInteractor;
    private StateMachine mainStateMachine;

    public List<List<Numberable>> numberables
    {
        get => repository.numberables;
        set => repository.numberables = value;
    }

    public List<Tape> tapes
    {
        get => repository.tapes;
        set => repository.tapes = value;
    }

    public override void OnCreate()
    {
        repository = Game.GetRepository<TapesRepository>();
        pointsInteractor = Game.GetInteractor<PointsInteractor>();

        mainStateMachine = C.main.MainStateMachine;
        if(mainStateMachine != null)
        {
            mainStateMachine.onStateChanged += OnStateChanged;
        }
    }
    private void OnTapeClicked(Tape tape)
    {
        Numberable lastBoardedNumber = tape.Number;

        foreach (var t in tapes)
        {
            if (t.Equals(tape)) continue;
            t.TryAddZero(lastBoardedNumber.transform.localPosition.x);
        }

        UpdateNumbersMatrix();

        List<Numberable> combo = lastBoardedNumber.TryGetCombo();
        if (combo != null)
        {
            pointsInteractor.RecieveCombo(combo);
        }
    }

    private void UpdateNumbersMatrix()
    {
        numberables = new List<List<Numberable>>();

        for (int y = 0; y < tapes.Count; y++)
        {
            //Debug.Log(numberables.Count);
            numberables.Add(tapes[y].numberables);
        }
        foreach (var y in numberables)
        {
            int yI = numberables.IndexOf(y);
            for(int x = 0; x < y.Count; x++)
            {
                Transform child = tapes[yI].transform.GetChild(x);

                if (child.TryGetComponent<Numberable>(out var number))
                {
                    if (number.boarded) numberables[yI] [x] = number;
                }
                //Debug.Log($"{yI}'s row {x} column {numberables[yI][x].Number} value");
            }
        }
        tapesChanged.Invoke(numberables);
    }

    private void OnStateChanged(IState state)
    {
        if(state == mainStateMachine.countState)
        {
            ResetLists();
        }
    }
    private void ResetLists()
    {
        Unsubscribe();

        tapes.Clear();
    }
    private void Subscribe(Tape tape)
    {
        tape.tapeClicked += OnTapeClicked;
    }
    private void Unsubscribe()
    {
        if (tapes.Count < 1) return;

        foreach (var tape in tapes)
        {
            tape.tapeClicked -= OnTapeClicked;
        }
    }
    public void AddTapeToList(Tape tape)
    {
        tapes.Add(tape);
        Subscribe(tape);
    }
}
