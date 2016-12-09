using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class Delay : MonoBehaviour
    {
        private readonly int delayQueueCount = 15;
        private readonly Queue<RenderTexture> renderTextureQueue;

        public bool IsEnabled { get; set; }

        public Delay()
        {
            renderTextureQueue = new Queue<RenderTexture>();
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            if (IsEnabled)
            {
                SetDelay(src, dest);
            }
            else
            {
                Graphics.Blit(src, dest);
            }
        }

        private void SetDelay(RenderTexture src, RenderTexture dest)
        {
            var temporary = RenderTexture.GetTemporary(src.width, src.height);
            if (temporary.IsCreated())
            {
                renderTextureQueue.Enqueue(temporary);
            }
            else
            {
                renderTextureQueue.Enqueue(src);
            }
            if (renderTextureQueue.Count == delayQueueCount)
            {
                src = renderTextureQueue.Dequeue();
                Graphics.Blit(src, dest);
            }
        }
    }
}