using UnityEngine;
public class CheckPlayerCollisionWithEnemy : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            if (collision.transform.GetComponent<MeshRenderer>().material.color == transform.GetComponent<MeshRenderer>().material.color)
            {
                Destroy(collision.transform.gameObject);
            }
        }
    }
}
