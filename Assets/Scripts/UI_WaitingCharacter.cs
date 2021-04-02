using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class UI_WaitingCharacter : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public int slotIndex;
    private UI_WaitingRoomPanel panel;
    private CharacterData cd;
    private Image image;
    private RectTransform rect;
    
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI yearsText;
    [SerializeField] AnimationCurve fadeCurve;
    public void Initialize(UI_WaitingRoomPanel panel)
    {
        this.panel = panel;
        this.rect = GetComponent<RectTransform>();
        this.image = GetComponent<Image>();
        this.nameText.enabled = false;
        this.yearsText.enabled = false;
    }
    public void AssignCharacter(CharacterData cd, int slotIndex)
    {
        this.slotIndex = slotIndex;
        this.cd = cd;
        this.nameText.text = cd.name;
        this.image.sprite = cd.waitingRoomSprite;
        this.image.SetNativeSize();
        this.image.rectTransform.sizeDelta = new Vector2(cd.waitingRoomSprite.rect.width, cd.waitingRoomSprite.rect.height) * 0.5f;
    }

    public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("Character Selected: " + cd.name);
        panel.OnCharacterSelected(this, cd);
    }

    public void OnPointerEnter(PointerEventData data)
    {
        this.nameText.enabled = true;
        this.yearsText.enabled = true;
    }

    public void OnPointerExit(PointerEventData data)
    {
        this.nameText.enabled = false;
        this.yearsText.enabled = false;
    }

    public void SetParent(RectTransform parent)
    {
        rect.SetParent(parent);
        rect.anchoredPosition = Vector2.zero;
    }
    public void UpdateFade()
    {
        float fadeAmount = fadeCurve.Evaluate(1 - ((float)cd.yearsInPurgatory / 1000));
        this.image.color = new Color(1, 1, 1, fadeAmount);
    }

    public void UpdateTimeText()
    {
        this.yearsText.text = cd.yearsInPurgatory + " Years in Purgatory";
    }

}
