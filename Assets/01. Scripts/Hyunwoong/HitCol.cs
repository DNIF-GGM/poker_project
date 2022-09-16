using UnityEngine;

public class HitCol : MonoBehaviour {
    private void OnTriggerEnter(Collider other) {
        other.GetComponent<UnitBase>().OnDamage(3f);
    }
}