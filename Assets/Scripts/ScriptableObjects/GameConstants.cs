using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    public int playerHighScore;
    public int deathImpulse;

    // Goomba
    public float enemyPatroltime;
    public float maxOffset;

    // Fire Mario
    public int flickerInterval;
}