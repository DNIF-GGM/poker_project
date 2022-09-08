using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMovement : Card
{
    public bool _isMove = false;

    public void CardMove(Transform deckTrn,Transform target){
        Vector3 velo = Vector3.zero;

        StartCoroutine(Moving(deckTrn, target));
    }

    private IEnumerator Moving(Transform deckTrn,Transform target){
        while(_isMove){
            
            this.transform.position = Vector3.MoveTowards(deckTrn.position,target.position,Time.deltaTime);
            this.transform.LookAt(target.position);

            yield return null;
        }
    }
}
