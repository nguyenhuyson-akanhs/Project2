using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    public string _SceneName;

    public virtual void OnInteract()
    {
        LoadSceneManger.Instance.OnInteract(_SceneName);
    }
}
