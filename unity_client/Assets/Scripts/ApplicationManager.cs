using UnityEngine;
using System.Collections;

public class ApplicationManager : MonoBehaviour {

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

	// Use this for initialization
	void Start () {
        NetManager.Instance.Connect("192.168.1.101", 8888);
        NetManager.Instance.StartNetThread();
        LoginController.Instance().RegisterProto();
	}
	
	// Update is called once per frame
	void Update () {
        MessageDispatcher.Instance.DispatchMessage();
	}

    void OnApplicationQuit()
    {
        NetManager.Instance.Disconnect();
    }
}
