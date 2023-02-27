using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tapes
{
    public class TapeSpawner : TapeHandler
    {
        [SerializeField] private GameObject tapePrefab;

        public void SpawnTapes(int tapeAmount)
        {
            if (transform.childCount > 0) ClearField();
    
            float height = Camera.main.orthographicSize;
    
            for (int i = tapeAmount - 1; i > -1; i--)
            {
                Vector2 pos = new(0, (height / tapeAmount + 0.6f) * i);

                GameObject t = Instantiate(tapePrefab.gameObject, pos,
                    Quaternion.identity, transform);
            
                Vector3 scale = t.transform.localScale;
                scale = new Vector3(scale.x, scale.y - 0.1f * tapeAmount, scale.y);
                t.transform.localScale = scale;

                tapes.Add(t.GetComponent<Tape>());
            }
        }
    
        private void ClearField()
        {
            foreach (var t in tapes)
            {
                Destroy(t.gameObject);
            }
            tapes.Clear();
            preferredXPos.Clear();
        }

    }

}



