using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class Delay : MonoBehaviour {
	private Queue myQ;

	private int delayQueueCount = 80;
	public int DelayQueueCount {
		get { return delayQueueCount; }
		set {delayQueueCount = value;} 
	}

	public Delay() {
		this.myQ = new Queue();
	}	

	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		RenderTexture temporary = RenderTexture.GetTemporary (src.width, src.height);
		if (src.IsCreated()) {
			Debug.Log ("Source");
		}
		if (temporary.IsCreated ()) {
			Debug.Log ("Delay");
			myQ.Enqueue (temporary);
		} else {				// we could remove this else, but I can observe a bit delay or interruption in the video when removed
			myQ.Enqueue (src);
		}
		if (myQ.Count == DelayQueueCount) {
			src = (RenderTexture)myQ.Dequeue ();
			Graphics.Blit(src,dest);
		} 
	}
}