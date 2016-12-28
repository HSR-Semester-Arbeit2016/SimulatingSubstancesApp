using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	/// <summary>
	/// This class generates a configurable delay effect in the video stream. Despite of its simplicity, this effect hides complex operations.
	/// In order to obtain this delay, it gets a temporary render texture from the RenderTexture src object, saves it in the Collection 
	/// and later passes it on to the Graphics.Blit(src, dest); method. 
	/// Render textures are textures that can be rendered to. They can be used to implement image based rendering effects,
	/// dynamic shadows, projectors, reflections or surveillance cameras.
	/// See OnRenderImage and SetDelay methods.
	/// This class works only properly on a computer, as it requires great RAM capacity. If run in a mobile phone it freezes the app or crash the phone.
	/// </summary>
	public class Delay : MonoBehaviour
	{
		private const int delayQueueCount = 15;
		private readonly Queue<RenderTexture> renderTextureQueue;

		/// <summary>
		/// Gets or sets a value indicating whether this instance is enabled. 
		/// Do not confuse with the <code>enabled</code> variable of the <code>Behaviour</code> class. If the latter is used, application can crash.
		/// </summary>
		/// <value><c>true</c> if this instance is enabled; otherwise, <c>false</c>.</value>
		public bool IsEnabled { get; set; }

		public Delay ()
		{
			renderTextureQueue = new Queue<RenderTexture> ();
		}

		/// <summary>
		/// Method of the MonoBehaviour class.This method is called after all rendering is complete to render image.
		/// It allows you to modify final image by processing it with shader based filters. The incoming image is source render texture. 
		/// The result should end up in destination render texture. You must always issue a Graphics.Blit() 
		/// or render a fullscreen quad if your override this method.
		/// </summary>
		/// <param name="src">Source.</param>
		/// <param name="dest">Destination.</param>
		private void OnRenderImage (RenderTexture src, RenderTexture dest)
		{
			if (IsEnabled) {
				SetDelay (src, dest);
			} else {
				Graphics.Blit (src, dest);  //Copies the src texture into dest render texture with a shader
			}
		}

		/// <summary>
		/// This methods obtains a RenderTexture temporary from the GetTemporary method called with src as argument. 
		/// Then, as the documentation says that temporary can be null, it checks it with 
		/// the IsCreated() method and if not, it puts it in the queue.
		/// If temporaryis null, it puts the original src in the queue, in order to not interrupt the image flow. 
		/// Then, after a given configurable delay (delayQueueCount) it gets the former temporaryobject, it assigns it to src 
		/// and finally we pass it on to the Graphics.Blit() method, creating so the delay effect.
		/// </summary>
		/// <param name="src">Source.</param>
		/// <param name="dest">Destination.</param>
		private void SetDelay (RenderTexture src, RenderTexture dest)
		{   //GetTemporary allocates a temporary render texture. Internally Unity keeps a pool of temporary render textures, 
			// so a call to GetTemporary most often just returns an already created one. 
			//[…] These temporary render textures are actually destroyed when they aren’t used for a couple of frames.
			var temporary = RenderTexture.GetTemporary (src.width, src.height);
			if (temporary.IsCreated ()) {
				renderTextureQueue.Enqueue (temporary);
			} else {
				renderTextureQueue.Enqueue (src);
			}
			if (renderTextureQueue.Count == delayQueueCount) {
				src = renderTextureQueue.Dequeue ();
				Graphics.Blit (src, dest);
			}
		}
	}
}
