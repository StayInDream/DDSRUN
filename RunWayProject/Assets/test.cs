using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DataManager.Instance.ReadText("日常任务" );
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
