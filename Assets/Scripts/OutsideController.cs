using UnityEngine;

public class OutsideController : MonoBehaviour
{
    public bool IsPlayerOutside { get; set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            Destroy(collision.gameObject);
            IsPlayerOutside = true;
        }
        if (collision.transform.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        }
    }
}
