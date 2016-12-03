using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class Delay : MonoBehaviour
{
	private Queue<RenderTexture> renderTextureQueue;
	private readonly int delayQueueCount = 15;

	public Delay ()
	{
		renderTextureQueue = new Queue<RenderTexture> ();
	}

	void OnRenderImage (RenderTexture src, RenderTexture dest)
	{
		if (enabled) {
			SetDelay (src, dest);
		} else {
			Graphics.Blit (src, dest);
		}
	}

	private void SetDelay (RenderTexture src, RenderTexture dest)
	{
		RenderTexture temporary = RenderTexture.GetTemporary (src.width, src.height);
		if (temporary.IsCreated ()) {
			renderTextureQueue.Enqueue (temporary);
		} else {			
			renderTextureQueue.Enqueue (src);
		}
		if (renderTextureQueue.Count == delayQueueCount) {
			src = (RenderTexture)renderTextureQueue.Dequeue ();
			Graphics.Blit (src, dest);
		} 
	}
}