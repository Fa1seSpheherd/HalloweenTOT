using System.Collections;
using UnityEngine;

public class GamePlay : MonoBehaviour
{
    public GameObject visitorsPrefab;
    public CameraZoom cameraZoom;

    bool gameIsRunning = true;

    void Start()
    {
        StartCoroutine(SpawnVisitors());
    }

    void OnDestroy()
    {
        gameIsRunning = false;
    }

    IEnumerator SpawnVisitors()
    {
        while (gameIsRunning) {
            GameObject visitors = Instantiate(visitorsPrefab);
            yield return new WaitForSeconds(7f);
            yield return new WaitWhile(() => { return cameraZoom.IsBusy; });
            Destroy(visitors);
        }
    }
    
}
