    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenPanel : MonoBehaviour
{
    [SerializeField] GameManager gm;
    Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Play("Visible");
        animator.SetBool("Show", true);
    }

    public void OnStartClicked()
    {
        animator.SetBool("Show", false);
        gm.OnGameStart();
    }

}
