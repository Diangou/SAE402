using UnityEngine;

public class EnemyDamageManager : MonoBehaviour
{
    public Enemy enemy;


    public void Hurt()
    {
        if (enemy != null)
        {
            enemy.Hurt();
        }
    }
}
