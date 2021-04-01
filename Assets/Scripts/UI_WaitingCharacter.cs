using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


public class UI_WaitingCharacter : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private UI_WaitingRoomPanel panel;
    private CharacterData cd;
    private Image image;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI yearsText;
    public void Initialize(UI_WaitingRoomPanel panel)
    {
        this.panel = panel;
        this.image = GetComponent<Image>();
        this.nameText.enabled = false;
        this.yearsText.enabled = false;
    }
    public void AssignCharacter(CharacterData cd)
    {
        this.cd = cd;
        this.nameText.text = cd.name;

    }
    public void OnPointerClick(PointerEventData data)
    {
        Debug.Log("Character Selected: " + cd.name);
        panel.OnCharacterSelected(cd);
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

    public void UpdateFade()
    {
        this.image.color = new Color(1, 1, 1, 1 - ((float)cd.yearsInPurgatory / 1000));
    }

    public void UpdateTimeText()
    {
        this.yearsText.text = cd.yearsInPurgatory + " Years in Purgatory";
    }
}
