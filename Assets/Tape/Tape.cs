using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tapes
{
    public class Tape : MonoBehaviour
    {
        [SerializeField] private GameObject numberPrefab, bonusPrefab;
        [SerializeField] private Color mouseOverColor;

        public Number number;
        private float numberSpawnTime;
        private tState state = tState.idle;
        private Color originColor;
        private SpriteRenderer rend;
        private float buffer;
        private enum tState
        {
            idle,
            active,
        }

        private void Awake() => rend = GetComponent<SpriteRenderer>();
 
        private void Start()
        {
            originColor = rend.color;
            numberSpawnTime = Balance.instance.SpawnNumbersTime;

            Invoke(nameof(SpawnTapeTile), numberSpawnTime-3);
        }

        private void SpawnTapeTile()
        {
            state = tState.active;

            GameObject tile = Instantiate(numberPrefab, transform);
            number = tile.GetComponent<Number>();

            Invoke(nameof(SpawnTapeTile), numberSpawnTime);

            float chance = Random.Range(0f, 1f);
            if (chance > 0.6f) Instantiate(bonusPrefab, transform);
        }
        private void OnMouseDown()
        {
            if (state != tState.active) return;

            rend.color = originColor;
            state = tState.idle;

            number.Interact();
            number = null;
        }
        private void OnMouseEnter()
        {
            if (state != tState.active) return;

            rend.color = mouseOverColor;
        }
        private void OnMouseExit()
        {
            if (state != tState.active) return;

            rend.color = originColor;
        }
        public void StopTape()
        {
            state = tState.idle;

            if (number != null) Destroy(number.gameObject);

            StopAllCoroutines();
            CancelInvoke();
        }
    }
}
