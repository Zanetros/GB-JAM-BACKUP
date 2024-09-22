using UnityEngine;

public interface IEnemyMoveable
{
    public Rigidbody2D rb { get; set; }
    public bool isFancingRight { get; set; }
    void MoveEnemy(Vector2 velocity);
    void CheckForLeftOrRightFacing(Vector2 velocity);
}
