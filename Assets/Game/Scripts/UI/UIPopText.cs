using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPopText : MonoBehaviour
{
    public Animator animator;
    TextMeshProUGUI ScoreText;

    void OnEnable()
    {
        AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(gameObject, clipInfo[0].clip.length);
        ScoreText = animator.GetComponent<TextMeshProUGUI>();
    }

    public void SetText(string text)
    {
        ScoreText.text = text;
    }
}
