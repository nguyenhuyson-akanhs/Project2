using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class Test2 : MonoBehaviour
{
    [SerializeField] private string NameAddress;
    [SerializeField] private AssetReference AssetReference;
    private AsyncOperationHandle<GameObject> _AsyncOperationHandle;
    private GameObject G1,G2;

    protected virtual void OnClick1()
    {
        //1
        var handle1 = Addressables.InstantiateAsync(NameAddress);
        handle1.Completed += (AsyncOperationHandle<GameObject> task) =>
        {
            G1 = task.Result;
            Instantiate(G1);
        };

        Addressables.InstantiateAsync(NameAddress).Completed += PassHandle;

        //2
        Addressables.InstantiateAsync(AssetReference).Completed += (AsyncOperationHandle<GameObject> task) => { };
    }

    protected virtual void OnClick2()
    {
        G2 = _AsyncOperationHandle.Result;
        Instantiate(G2);
        Instantiate(_AsyncOperationHandle.Result);
    }


    protected virtual void PassHandle(AsyncOperationHandle<GameObject> task)
    {
        _AsyncOperationHandle = task;
    }

    protected virtual void OnClick3()
    {
        Addressables.LoadAssetAsync<GameObject>(AssetReference).Completed += (AsyncOperationHandle<GameObject> task) => { Instantiate(task.Result); };
        //Addressables.
    }

    // TODO: Release, case in use
}
