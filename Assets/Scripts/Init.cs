using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// 自動スリープを無効にする場合
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
