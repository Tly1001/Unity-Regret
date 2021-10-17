using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Wander,
    Follow,
    Die
};
public class EnemyController : MonoBehaviour
{
    GameObject player;

    public EnemyState currState = EnemyState.Wander;

    public float range;
    public float speed;

    private bool chooseDir = false;

    // will be fixed later
    private bool dead = false;
    private Vector3 randomDir;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        switch(currState)
        {
            case(EnemyState.Wander):
                Wander();
                break;
                case(EnemyState.Follow):
                Follow();
                break;
                case(EnemyState.Die):

                break;
        }

        if(isPlayerInRange(range) && currState != EnemyState.Die) {
            currState = EnemyState.Follow;
        } else {
            currState = EnemyState.Wander;
        }
    }

    private bool isPlayerInRange(float range) {
        // distance between player and enemies position
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection() {
        // ChooseDir = true;
        yield return new WaitForSeconds(Random.Range(2f, 8f));
        // this rotates the enemy which we don't want but it decides the enemies face direction overall
        // randomDir = new Vector3(0,0,Random.Range(0,360));
        // Quanternion nextRotation = Quaternion.Erler(randomDir);
        // transform.rotation = Quaternion.Lerp(transform.rotation, nextRotation, randomDir(0.5f, 2.5f));
        // ChooseDirection = false;


    }

    void Wander() {
        if(!chooseDir) {
            StartCoroutine(ChooseDirection());
        }
        transform.position += -transform.right * speed * Time.deltaTime;
        if(isPlayerInRange(range)) {
            currState = EnemyState.Follow;
        }
    }

    void Follow() {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }

    public void Death() {
        Destroy(gameObject);
    }
}
