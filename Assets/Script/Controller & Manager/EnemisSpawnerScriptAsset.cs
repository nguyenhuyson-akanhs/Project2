using UnityEngine;

[CreateAssetMenu(fileName = "EnemisSpawner", menuName = "ScriptableObject/EnemisSpawner")]
public class EnemisSpawnerScriptAsset : ScriptableObject
{
    [Header("Basic")]
    [Range(0f, 1f)] public float _SpawnRate;
}
