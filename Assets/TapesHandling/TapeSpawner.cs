using Architecture;
using UnityEngine;

namespace Tapes
{
    public class TapeSpawner : MonoBehaviour
    {
        private int tapeAmount;
        private StateMachine mainStateMachine;
        private TapesInteractor tapesInteractor;

        private void Start()
        {
            tapeAmount = Balance.instance.StartTapesAmount;
            tapesInteractor = Game.GetInteractor<TapesInteractor>();

            mainStateMachine = C.main.MainStateMachine;

            if(mainStateMachine != null)
            {
                mainStateMachine.onStateChanged += OnStateChanged;
            }
        }

        private void OnDisable()
        {
            if(mainStateMachine != null)
            {
                mainStateMachine.onStateChanged -= OnStateChanged;
            }
        }

        private void SpawnTapes()
        {
            int n = 0;
            float height = Camera.main.orthographicSize;

            for (int i = tapeAmount - 1; i > -1; i--)
            {
                n++;

                Tape t = TapeObjectsFactory.instance.Get<Tape>(transform);

                t.name = n.ToString();

                Vector2 pos = new(0, (height / tapeAmount + 0.6f) * i);
                t.transform.position = pos;

                Vector3 scale = t.transform.localScale;
                scale = new Vector3(scale.x, scale.y - 0.1f * tapeAmount, scale.y);
                t.transform.localScale = scale;

                tapesInteractor.AddTapeToList(t.GetComponent<Tape>());
            }
        }
        private void OnStateChanged(IState state)
        {
            if(state == mainStateMachine.playState)
            {
                for(int i = 0; i < transform.childCount; i++)
                {
                    Destroy(transform.GetChild(i).gameObject);
                }

                SpawnTapes();
            }
        }
    }

}



