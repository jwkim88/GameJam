using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_CharacterPanel : MonoBehaviour
{
    public Image portrait;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI deathText;
    [SerializeField] protected CharacterManager cm;
    [SerializeField] protected RectTransform deathCertificateParent;
    [SerializeField] UI_DeedObject[] deedObjects = new UI_DeedObject[2];
    public void ShowCharacter(CharacterData cd)
    {
        portrait.sprite = cd.sprite;
        portrait.SetNativeSize();
        portrait.enabled = true;
        nameText.text = cd.name;
        Debug.Log("Searching for " + cd.death);
        DeathData dd = cm.deaths.Find(d => d.id == cd.death);
        deathText.text = dd.desc;
        nameText.enabled = true;
        deathText.enabled = true;
        deathCertificateParent.gameObject.SetActive(true);
        DeedData deedData;
        for(int i = 0; i < cd.deeds.Count; i++)
        {
            deedData = cm.deeds.Find(d => d.id == cd.deeds[i]);
            deedObjects[i].AssignDeedData(deedData);
        }
    }

    public void HideCharacter()
    {
        portrait.enabled = false;
        deathText.enabled = false;
        nameText.enabled = false;
        deathCertificateParent.gameObject.SetActive(false);
    }
}
