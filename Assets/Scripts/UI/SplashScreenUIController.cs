using UnityEngine;
using System.Collections;
using Prime31.TransitionKit;
using UnityEngine.UI;

public class SplashScreenUIController : MonoBehaviour {

	[SerializeField] private Slider _slider;
	private AsyncOperation _async;

	void Awake () {
		EventManager.OnAsyncSceneLoadEvent += OnAsyncSceneLoad;
        Invoke("Transit", 0.5f);
    }
	
    void Transit()
    {
        GameManager.Instance.GameFlow.TransitToScene(UIConsts.SCENE_ID.MENU);
    }

	void OnAsyncSceneLoad(EventData e) {
		_async = (AsyncOperation)e.Data["asyncOperation"];
	}

	void OnDestroy() {
		EventManager.OnAsyncSceneLoadEvent -= OnAsyncSceneLoad;
	}

	void Update() {
		if (_async != null) {
			_slider.value = _async.progress;
		}
	}
}
