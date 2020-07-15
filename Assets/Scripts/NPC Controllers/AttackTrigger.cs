using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    NonPlayerCharacter npcController;
    [SerializeField] int AttackIndex;
    [SerializeField] float timeUntilDamage;
    Transform[] AOEzones;
    
    bool attacking;
    float startTime;
 
    // Start is called before the first frame update
    void Start()
    {
        npcController = GetComponentInParent<NonPlayerCharacter>();
        AOEzones = new Transform[2] { transform.GetChild(0), transform.GetChild(1) };
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            float elapsedTime = Time.time - startTime;
            // (Time.time - startTime) / timeUntilDamage
            AOEzones[1].transform.localScale = new Vector3(
                AOEzones[0].localScale.x, 
                AOEzones[0].localScale.y, 
                ((Time.time - startTime) / timeUntilDamage) * AOEzones[0].localScale.z
                );
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (attacking) { return; }

        npcController.SetAttackIndex(AttackIndex);

        AOEzones[0].gameObject.SetActive(true);
        AOEzones[1].gameObject.SetActive(true);

        startTime = Time.time;
        StartCoroutine(StartAOEZone());
    }

    IEnumerator StartAOEZone()
    {
        attacking = true;
        yield return new WaitForSeconds(timeUntilDamage);
        
        attacking = false;
        AOEzones[0].gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);

        AOEzones[1].transform.localScale = new Vector3(AOEzones[0].localScale.x, AOEzones[0].localScale.y, 0);
        AOEzones[1].gameObject.SetActive(false);
    }

}
