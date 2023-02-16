using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tape : MonoBehaviour
{
    public GameObject numberPrefab, bonusPrefab;
    public int numberSpawnTime;
    public Color mouseOverColor;
    public GameObject number;

    private lState state = lState.idle;
    private Color originColor;
    private SpriteRenderer rend;
    private float buffer;
    private enum lState
    {
        idle,
        active,
    }

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();

        originColor = rend.color;
    }

    public void InvokeTape(float invokeTime)
    {
        Invoke(nameof(SpawnNumber), invokeTime);
        Invoke(nameof(SpawnBonus), numberSpawnTime * 3);
    }
    private void SpawnNumber()
    {
        state = lState.active;

        number = Instantiate(numberPrefab, transform);
        Vector3 lScale = number.transform.localScale;
        lScale = new(lScale.x / 10, 1, lScale.z);
        number.transform.localScale = lScale;

        buffer = number.transform.localScale.x + 0.3f;

        float edge = Random.Range(-0.1f, 0.1f);
        float lPosX = (0.5f - lScale.x / 2) * Mathf.Sign(edge);
        number.transform.localPosition = new(lPosX, 0f);

        StartCoroutine(NumberMove(number.transform));

        Invoke(nameof(SpawnNumber), numberSpawnTime+2);
    }

    private IEnumerator NumberMove(Transform number)
    {
        float startX = number.transform.localPosition.x;

        float elapsedTime = 0f;
        while(elapsedTime < numberSpawnTime + 2)
        {
            elapsedTime += Time.deltaTime;
            float xPos = Mathf.Lerp(startX, startX*-1, elapsedTime / (numberSpawnTime+2));
            number.transform.localPosition = new(xPos, 0f);
            yield return null;
        }
        Destroy(number.gameObject);
    }

    private void SpawnBonus()
    {
        float chance = Random.Range(0f, 1f);
        if (chance > 0.6f)
        {
            GameObject bonus = Instantiate(bonusPrefab, transform);
            Vector3 bScale = bonus.transform.localScale;
            bScale = new(bScale.x / 8, 1, bScale.z);
            bonus.transform.localScale = bScale;
            bonus.transform.localPosition = BonusPosition();
        }
        Invoke(nameof(SpawnBonus), numberSpawnTime + 2);
    }
    private Vector3 BonusPosition()
    {
        float xPos = Random.Range(-0.3f, 0.3f);
        if (transform.childCount > 0)
        {
            int index = Random.Range(0, transform.childCount);
            xPos = transform.GetChild(index).localPosition.x;
            if (transform.GetChild(index).name != 0.ToString())
            {
                xPos -= buffer * Mathf.Sign(xPos);
            }
        }
        return new(xPos, 0, 0);
    }
    private void OnMouseDown()
    {
        if (state != lState.active) return;

        rend.color = originColor;
        state = lState.idle;
        C.tape.SetNumber(this);
        number = null;

        StopAllCoroutines();
    }
    private void OnMouseEnter()
    {
        if (state != lState.active) return;

        rend.color = mouseOverColor;
    }
    private void OnMouseExit()
    {
        if (state != lState.active) return;

        rend.color = originColor;
    }
    public void StopTape()
    {
        state = lState.idle;

        if (number != null) Destroy(number);

        StopAllCoroutines();
        CancelInvoke();
    }
}
