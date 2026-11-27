using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneTag
{
    Menu,
    NormalMap,
    CombatArea,

}

public class LoadSceneManger : MonoBehaviour
{
    public static LoadSceneManger Instance { get; private set; }

    public static Dictionary<string, SceneTag> _SceneTag = new();

    public static Action<Transform> _SetPlayerNewTransform;

    public string _CurrentScene { get; private set; }
    public string _LastTown { get; private set; }

    [Header("Slider")]
    public GameObject _LoadbarContainer;
    protected Slider _LoadBarSlider;

    protected virtual void OnEnable()
    {
        CollideEnemisArea._EnterComabatArea += OnInteract;
    }

    protected virtual void OnDisable()
    {
        CollideEnemisArea._EnterComabatArea += OnInteract;
    }

    protected virtual void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        // add tag for each scene here
        _SceneTag.Add("MainMenu", SceneTag.Menu);
        _SceneTag.Add("Town_1", SceneTag.NormalMap);
        _SceneTag.Add("CombatArea_1", SceneTag.CombatArea);

        // slider
        _LoadBarSlider = _LoadbarContainer.GetComponent<Slider>();
    }

    protected virtual void Start()
    {
        _CurrentScene = "MainMenu";
        _LastTown = "";

    }

    protected bool _IsLoadSceneManagerBusy = false;
    public void OnInteract(string NameScene)
    {
        if (_IsLoadSceneManagerBusy) return;

        _IsLoadSceneManagerBusy = true;
        _ = ChangeSceneAsync(NameScene);
    }

    protected virtual LoadPreUpdateManager FindScript(string NameScene)
    {
        Scene scene = SceneManager.GetSceneByName(NameScene);
        GameObject[] rootObjects = scene.GetRootGameObjects();

        foreach (GameObject obj in rootObjects)
        {
            PlayerNewTransform newTransform = obj.GetComponent<PlayerNewTransform>();
            if (newTransform != null)
            {
                _SetPlayerNewTransform?.Invoke(newTransform._NewTrasform);
            }

            LoadPreUpdateManager load = obj.GetComponent<LoadPreUpdateManager>();
            if (load != null)
            {
                return load;
            }
        }
        return null;
    }
    protected virtual async Awaitable ChangeSceneAsync(string NameScene)
    {

        _LoadbarContainer.SetActive(true);
        

        await UnloadSceneAsync(_CurrentScene);
        // ----- Enter -----
        // do animation or smt to face in v.v
        AsyncOperation LoadAsync = SceneManager.LoadSceneAsync(NameScene, LoadSceneMode.Additive);
        LoadAsync.allowSceneActivation = false;


        // ----- MainLoad -----
        while (Mathf.Clamp01(LoadAsync.progress) < 0.9f)
        {
            _LoadBarSlider.value = LoadAsync.progress;
            Debug.Log(_LoadBarSlider.value);
            //loadbar there
            //Debug.Log($"Progress: {Mathf.Clamp01(LoadAsync.progress) * 100}%");

            await Awaitable.NextFrameAsync();
        }
        //Debug.Log("Done 90%");

        LoadAsync.allowSceneActivation = true;
        while (!LoadAsync.isDone) await Awaitable.NextFrameAsync();

        await Awaitable.NextFrameAsync();

        LoadPreUpdateManager LoadPreUpdateScript = FindScript(NameScene);

        await LoadPreUpdateScript.IntitaliazeObject((progress) =>
        {
            _LoadBarSlider.value = 0.9f + progress;
            Debug.Log(_LoadBarSlider.value);
        });


        await Awaitable.NextFrameAsync();
        // ----- EndLoad -----
        // do face out v.v

        _CurrentScene = NameScene;
        if (_SceneTag[_CurrentScene] == SceneTag.NormalMap) { _LastTown = _CurrentScene; }
        _IsLoadSceneManagerBusy = false;

        Debug.Log(_LoadBarSlider.value);

        Debug.Log(_LoadbarContainer.activeInHierarchy);
    }

    protected virtual async Awaitable UnloadSceneAsync(string NameScene)
    {
        Debug.Log(NameScene);
        if (NameScene == "") return;
        if (SceneManager.GetSceneByName(NameScene).isLoaded == false) return;

        AsyncOperation UnloadAsync = SceneManager.UnloadSceneAsync(NameScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        await UnloadAsync;
        await Awaitable.NextFrameAsync();
        await Resources.UnloadUnusedAssets();
        await Awaitable.NextFrameAsync();
        GC.Collect();
        await Awaitable.NextFrameAsync();

        
    }
}
