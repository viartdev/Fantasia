using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightController : MonoBehaviour {
    public float PunchDistance = 2.0f;    
    public int AttackDamage = 10;
    public List<GameObject> TriggeredEnemies { get; set; }
    // Start is called before the first frame update
    void Start() {
        TriggeredEnemies = new List<GameObject>();
    }

    // Update is called once per frame
    Vector3 GetPunchVector() {
        Vector3 punchVector = transform.forward * PunchDistance;
        punchVector.y += PunchDistance / 2;
        return punchVector;
    }
    void FixedUpdate() {
        if (Input.GetButtonDown("Fire1")) {            
            foreach (var enemy in TriggeredEnemies) {
                if (enemy == null) {
                    TriggeredEnemies.Remove(enemy);
                    continue;
                }                
                SimpleEnemyController simpleEnemyController = enemy.GetComponentInParent<SimpleEnemyController>();
                if (simpleEnemyController!=null) {
                    simpleEnemyController.ApplyDamage(AttackDamage);
                    simpleEnemyController.ApplyPunch(GetPunchVector());
                }
            }
        }
    }
}
