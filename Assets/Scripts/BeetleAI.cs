using UnityEngine;

public class BeetleAI : EnemyAI
{
    public Material skin;
    public SkinnedMeshRenderer mesh;
    protected new void Start()
    {
        mesh.material = skin;
        base.Start();
    }
}
