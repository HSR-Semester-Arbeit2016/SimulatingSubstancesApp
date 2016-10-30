using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class Delay : MonoBehaviour
{
	private Queue<RenderTexture> renderTextureQueue;
	private bool isEnabled = false;

	public bool IsEnabled {
		get { return isEnabled; }
		set { isEnabled = value; }
	}

	private int delayQueueCount = 15;

	private int DelayQueueCount {
		get { return delayQueueCount; }
	}

	public Delay ()
	{
		this.renderTextureQueue = new Queue<RenderTexture> ();
	}

	void OnRenderImage (RenderTexture src, RenderTexture dest)
	{
		if (isEnabled) {
			this.setDelay (src, dest);
		} else {
			Graphics.Blit (src, dest);
		}
	}

	private void setDelay (RenderTexture src, RenderTexture dest)
	{
		RenderTexture temporary = RenderTexture.GetTemporary (src.width, src.height);
		if (temporary.IsCreated ()) {
			renderTextureQueue.Enqueue (temporary);
		} else {			
			renderTextureQueue.Enqueue (src);
		}
		if (renderTextureQueue.Count == DelayQueueCount) {
			src = (RenderTexture)renderTextureQueue.Dequeue ();
			Graphics.Blit (src, dest);
		} 
	}
}