using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject _loadPanel;
    [SerializeField] private Slider _slider;

    public void SceneLoad(int sceneIndex)
    {
        _loadPanel.SetActive(true);
        StartCoroutine(LoadAsynk(sceneIndex));
    }

    private IEnumerator LoadAsynk(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            _slider.value = progress;
            yield return null;
        }
    }
}
