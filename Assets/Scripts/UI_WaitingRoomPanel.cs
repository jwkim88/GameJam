using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WaitingRoomPanel : MonoBehaviour
{
    [SerializeField] GameManager gm;
    [SerializeField] UI_WaitingCharacter characterPrefab;
    [SerializeField] RectTransform characterParent;
    [SerializeField] List<RectTransform> waitingRoomSlots = new List<RectTransform>();
    [SerializeField] RectTransform judgmentSlot;
    List<UI_WaitingCharacter> characters = new List<UI_WaitingCharacter>();
    UI_WaitingCharacter activeCharacter;
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>(); 
    }
    public void AssignCharacters(List<CharacterData> characters)
    {
        for(int i = 0; i < characters.Count; i++)
        {
            AssignCharacter(characters[i]);
        }
    }

    public UI_WaitingCharacter SpawnCharacter()
    {
        return Instantiate(characterPrefab, characterParent); ;
    }

    public void AssignCharacter(CharacterData cd)
    {
        if (characters.Find(c => c.cd == cd)) return;
        UI_WaitingCharacter c = SpawnCharacter();
        c.Initialize(this);
        
        characters.Add(c);
        for(int i = 0; i < waitingRoomSlots.Count; i++)
        {
            if(waitingRoomSlots[i].childCount == 0)
            {
                c.SetParent(waitingRoomSlots[i]);
                c.AssignCharacter(cd, i);
                break;
            }
        }
        
    }

    public void UpdateCharacterFadeState()
    {
        for(int i = 0; i < characters.Count; i++)
        {
            if (characters[i] == null) continue;
            characters[i].UpdateFade();
            characters[i].UpdateTimeText();
            if (characters[i].ShouldFade)
            {
                characters[i].FadeAway();
            }
        }
    }

    public void OnCharacterSelected(UI_WaitingCharacter waitingCharacter, CharacterData cd)
    {
        if (activeCharacter != null) return;
        activeCharacter = waitingCharacter;
        activeCharacter.Reappear(judgmentSlot);
        gm.OnCharacterSelected(cd);
    }

    public void OnCharacterSentToHeaven()
    {
        if (activeCharacter == null) return;

        StartCoroutine(ShowAnimationThenDestroyCharacter("Heaven"));
    }

    public void OnCharacterSentToHell()
    {
        if (activeCharacter == null) return;
        StartCoroutine(ShowAnimationThenDestroyCharacter("Hell"));
    
    }

    public void OnCharacterReturnedToPurgatory()
    {
        if (activeCharacter == null) return;
        activeCharacter.Reappear(waitingRoomSlots[activeCharacter.slotIndex]);
        
        activeCharacter = null;
    }

    void RemoveActiveCharacter()
    {
        characters.Remove(activeCharacter);
        Destroy(activeCharacter.gameObject);
        activeCharacter = null;
    }

    public void PurgeFadedSouls()
    {
        bool shouldFade = false;
        for(int i = 0; i < characters.Count; i++)
        {
            
        }
    }


    IEnumerator ShowAnimationThenDestroyCharacter(string animationName)
    {
        animator.Play(animationName);
        yield return new WaitForSeconds(0.5f);
        RemoveActiveCharacter();
    }
    
}
