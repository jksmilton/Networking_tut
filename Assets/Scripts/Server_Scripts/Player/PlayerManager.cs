using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))] //The script requires an attatched network view to run
public class PlayerManager : MonoBehaviour {
	
	public float speed
	{
		get; set;
	}
	
	private CharacterController controller;
	
	private float horizontalMotion, verticalMotion;
	
	// Use this for initialization
	void Start () {
		speed = 10;
		if(Network.isServer)
		{
			controller = GetComponent<CharacterController>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Network.isClient)
		{
			return;//This should only be run on the server
		}
		
		Debug.Log("Processing clients movement on server");
		
		controller.Move(new Vector3(
			horizontalMotion * speed * Time.deltaTime,
			0,
			verticalMotion * speed * Time.deltaTime
			));
	}
	/**
	 * The client calls this to notify the server about new motion data
	 */
	[RPC]
	public void updateClientMotion(float hor, float ver)
	{
		horizontalMotion = hor;
		verticalMotion = ver;
	}
}
