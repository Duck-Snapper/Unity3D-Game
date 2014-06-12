using UnityEngine;
using System.Collections;

public class matchmaker : MonoBehaviour {

	public string versionNumber;
	public GameObject overview;
	GameObject player;
	bool connecting = true;

	// Use this for initialization
	void Start () {
		//PhotonNetwork.ConnectUsingSettings(versionNumber);
		PhotonNetwork.ConnectToBestCloudServer (versionNumber);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI()
	{
		if(connecting){
			GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
		}	
		else{
			GUILayout.Label(PhotonNetwork.GetPing().ToString() + " Ping");
		}
	}

	void OnJoinedLobby()
	{
		PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed()
	{
		Debug.Log("Can't join random room!");
		PhotonNetwork.CreateRoom(null);
	}

	void OnJoinedRoom(){
		SpawnPlayer ();
		connecting = false;

	}

	void SpawnPlayer(){

		player = PhotonNetwork.Instantiate("Player", new Vector3(0f,4f,0f), Quaternion.identity, 0);
		overview.SetActive(false);
		((MonoBehaviour)player.GetComponent("MouseLook")).enabled = true;
		((MonoBehaviour)player.GetComponent("FPSInputController")).enabled = true;
		((MonoBehaviour)player.GetComponent("CharacterMotor")).enabled = true;
		player.transform.FindChild("Main Camera").gameObject.SetActive(true);
	}

}
