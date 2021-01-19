using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Game Units" )]
public class Unit : ScriptableObject
{
    public new string name;

    public float healthPoints;
    public float damagePoints;

    public float movementSpeed;
    public float attackSpeed;
}
