using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    Animator animator;

    public GameObject redGem;
    public GameObject cutetGrass;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Destroyed()
    {
        Vector3 spawnPositionGrass = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        Instantiate(cutetGrass, spawnPositionGrass, cutetGrass.transform.rotation);
        animator.SetTrigger("Destroyed");
    }

    public void RemoveObject()
    {
        Destroy(gameObject);

        int rng = Random.Range(1, 20);

        if (rng > 10)
        {
            Vector3 spawnPositionGem = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            Instantiate(redGem, spawnPositionGem, redGem.transform.rotation);
        }
    }

}
