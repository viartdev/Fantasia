using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyController : MonoBehaviour {
    public GameObject PlayerObject;

    private const int PunchMultiplier = 200;
    public int HealthPoints = 100;
    public int AttackDamage = 5;
    public float EnemySpeed = 2.0f;



    private float EnemyMaxSpeed = 10.0f;



    Rigidbody Rigidbody;

    // Start is called before the first frame update
    void Start() {
        PlayerObject = GameObject.FindGameObjectsWithTag("Player")[0];
        Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected virtual void GoToPlayer() {
        Vector3 target = PlayerObject.transform.position - transform.position;
        target /= target.magnitude;
        transform.LookAt(PlayerObject.transform);
        if (Rigidbody.velocity.x > EnemyMaxSpeed || Rigidbody.velocity.y > EnemyMaxSpeed) {
        }
        else {
            Rigidbody.AddForce(target * EnemySpeed * 1.5f);
        }
    }
    protected virtual void TryAttack() {
        
    }
    void FixedUpdate() {
        //Ray playerRay = new Ray(transform.position, PlayerObject.transform.position);
        //Debug.DrawLine(transform.position, PlayerObject.transform.position, Color.red, 1.0f);
        GoToPlayer();
        TryAttack();


        if (HealthPoints <= 0) {
            Destroy(gameObject);
        }
    }

    public void ApplyDamage(int damage) {
        Debug.Log(string.Format("{0}_{1}", gameObject.name, HealthPoints));
        HealthPoints -= damage;
    }
    public void ApplyPunch(Vector3 direction) {
        Rigidbody.AddForce(direction * PunchMultiplier);
        //transform.position += direction;
    }

}
