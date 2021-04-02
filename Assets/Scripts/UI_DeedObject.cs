using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_DeedObject : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] DeedData deedData;
    [SerializeField] RectTransform hoverTextParent;
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI bodyText;
    [SerializeField] Image icon;

    public void AssignDeedData(DeedData dd)
    {
        this.deedData = dd;
        this.titleText.text = dd.name;
        this.bodyText.text = dd.desc;
        this.icon.sprite = dd.icon;
        this.icon.SetNativeSize();
    }
    public void OnPointerEnter(PointerEventData data)
    {
        hoverTextParent.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData data)
    {
        hoverTextParent.gameObject.SetActive(false);
    }
}
