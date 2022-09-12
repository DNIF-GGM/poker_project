using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class  StraightFlush : MonoBehaviour
{
    Sequence sq;
    public void SkillEvent(){
        sq = DOTween.Sequence();

    }

    private void OnDisable() {
        sq.Kill();
    }
}
