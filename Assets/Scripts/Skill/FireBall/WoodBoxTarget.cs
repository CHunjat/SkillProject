using UnityEngine;

public class WoodBoxTarget : MonoBehaviour, IDamageable2D
{
    public int environHitCount = 2;

    public void TakeDamage(float amount, GameObject attacker)
    {
        environHitCount--;
        if (environHitCount <= 0)
        {
            DestoryItem();
        }


    }
    public void DestoryItem()
    {
        Debug.Log("나무파괴");
    }
}
