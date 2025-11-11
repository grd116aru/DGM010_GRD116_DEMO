using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage;
    private float damageCooldown = .5f; // 1 second between hits
    private float lastDamageTime = 0f;
    private void Start()
    {
        if (damage == 0) 
        { 
            damage = 20; 
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time > lastDamageTime + damageCooldown)
        {
            GameManager.Instance.ReduceHP(damage);
            lastDamageTime = Time.time;
        }
    }
}
