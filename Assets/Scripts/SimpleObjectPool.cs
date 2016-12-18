using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
	/// <summary>
	///  A very simple object pooling class which creates prefag objects.
	/// </summary>
	public class SimpleObjectPool : MonoBehaviour
	{
		/// <summary>
		/// The prefab that this object pool returns instances of. The reference must be set by dragging and dropping it in Unity editor
		/// </summary>
		public GameObject prefab;
		/// <summary>
		/// Collection of currently inactive instances of the prefab
		/// </summary>
		private Stack<GameObject> inactiveInstances = new Stack<GameObject> ();

		/// <summary>
		/// Returns an instance of the prefab
		/// </summary>
		/// <returns>The object.</returns>
		public GameObject GetObject ()
		{
			GameObject spawnedGameObject;

			// If there is an inactive instance of the prefab ready to return, return that
			if (inactiveInstances.Count > 0) {
				// remove the instance from teh collection of inactive instances
				spawnedGameObject = inactiveInstances.Pop ();
			}
            // Otherwise, create a new instance
            else {
				spawnedGameObject = (GameObject)GameObject.Instantiate (prefab);

				// Add the PooledObject component to the prefab so we know it came from this pool
				PooledObject pooledObject = spawnedGameObject.AddComponent<PooledObject> ();
				pooledObject.pool = this;
			}
			// Put the instance in the root of the scene and enable it
			spawnedGameObject.transform.SetParent (null);
			spawnedGameObject.SetActive (true);

			// Return a reference to the instance
			return spawnedGameObject;
		}

		/// <summary>
		/// Returns an instance of the prefab to the pool
		/// </summary>
		/// <param name="toReturn">To return.</param>
		public void ReturnObject (GameObject toReturn)
		{
			PooledObject pooledObject = toReturn.GetComponent<PooledObject> ();

			// If the instance came from this pool, return it to the pool
			if (pooledObject != null && pooledObject.pool == this) {
				// Make the instance a child of this and disable it
				toReturn.transform.SetParent (transform);
				toReturn.SetActive (false);

				// Add the instance to the collection of inactive instances
				inactiveInstances.Push (toReturn);
			}
            // Otherwise, just destroy it
            else {
				#if DEBUG
				Debug.LogWarning (toReturn.name + " was returned to a pool it wasn't spawned from! Destroying.");
				#endif
				Destroy (toReturn);
			}
		}
	}

	/// <summary>
	/// A component that simply identifies the pool that a GameObject came from
	/// </summary>
	public class PooledObject : MonoBehaviour
	{
		public SimpleObjectPool pool;
	}
}