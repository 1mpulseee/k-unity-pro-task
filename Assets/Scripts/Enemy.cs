using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed;
    void Start()
    {
        StartCoroutine(Move());
    }
    IEnumerator Move()
    {
        while (true)
        {
            if (transform.position.z < -4)
            {
                Player.instanse.TakeDamage(25);
                Destroy(gameObject);
            }
            transform.position -= new Vector3(0, 0, speed);
            yield return new WaitForSeconds(.02f);
        }
    }
}