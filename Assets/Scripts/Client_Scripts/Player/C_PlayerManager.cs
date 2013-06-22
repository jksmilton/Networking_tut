using UnityEngine;
using System.Collections;

public class C_PlayerManager : MonoBehaviour {

    private NetworkPlayer owner;

    //stored to only send RPCs when data has actually changed
    private float lastMotionH, lastMotionV;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Network.isServer)
        {
            return; //you shouldn't be here
        }
        //Only apply update if you are the correct client
        if ((owner != null) && (Network.player == owner))
        {
            float motionH = Input.GetAxis("Horizontal");
            float motionV = Input.GetAxis("Vertical");
            if ((motionH != lastMotionH) || (motionV != lastMotionV))
            {
                networkView.RPC("updateClientMotion", RPCMode.Server, motionH, motionV);
                lastMotionH = motionH;
                lastMotionV = motionV;
            }
        }

	}

    [RPC]
    public void setOwner(NetworkPlayer player)
    {
        Debug.Log("setting the owner");
    }
}
