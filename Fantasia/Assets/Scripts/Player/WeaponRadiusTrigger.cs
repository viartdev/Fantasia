using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRadiusTrigger : MonoBehaviour
{
    FightController fightController;
    // Start is called before the first frame update
    void Start()
    {
        fightController = gameObject.GetComponentInParent<FightController>();
        if (fightController==null) {
            Debug.Log("FightController not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider collider) {
        fightController.TriggeredEnemies.Add(collider.gameObject);
    }
    void OnTriggerExit(Collider collider) {
        fightController.TriggeredEnemies.Remove(collider.gameObject);
    }
}
