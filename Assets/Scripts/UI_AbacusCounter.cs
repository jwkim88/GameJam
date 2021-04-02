using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_AbacusCounter : MonoBehaviour
{
    public List<RectTransform> beads = new List<RectTransform>();
    [SerializeField] protected List<RectTransform> leftSideSlots = new List<RectTransform>();
    [SerializeField] protected List<RectTransform> rightSideSlots = new List<RectTransform>();
    [SerializeField] AnimationCurve curve;
    int beadIndex = 0;

    public void UpdateBeadAvailability(int availability)
    {
        beadIndex = availability;
        for(int i = 0; i < beads.Count; i++)
        {
            beads[i].gameObject.SetActive(i < availability);
        }
    }

    public void MoveBead()
    {
        if(beadIndex > 0)
        {
            beadIndex--;
            StartCoroutine(MoveBeadOverTime(beads[beadIndex], rightSideSlots[beadIndex].anchoredPosition));
        }
    }

    IEnumerator MoveBeadOverTime(RectTransform bead, Vector2 targetPos)
    {
        float duration = 0.8f;
        float timer = 0;
        Vector2 startPos = bead.anchoredPosition;
        while(timer < duration)
        {
            timer += Time.deltaTime;
            bead.anchoredPosition = Vector2.Lerp(startPos, targetPos, curve.Evaluate(timer/duration));
            yield return null;
        }
    }
}
