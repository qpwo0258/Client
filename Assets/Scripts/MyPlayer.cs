using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayer : Player
{
	NetworkManager _network;
	Rigidbody rigid;

	void Start()
    {
		StartCoroutine("CoSendPacket");
		_network = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
		rigid = GetComponent<Rigidbody>();
	}

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) {
			rigid.AddForce(Vector3.up * 10f, ForceMode.Impulse);
        }
		float x = Input.GetAxis("Horizontal");
		float y = Input.GetAxis("Vertical");
		Vector3 dir = new Vector3(x, 0, y) * 10f;
		dir.y = rigid.velocity.y;
		rigid.velocity = dir;
    }

	IEnumerator CoSendPacket()
	{
		while (true)
		{
			yield return new WaitForSeconds(0.25f);

			C_Move movePacket = new C_Move();
			movePacket.posX = transform.position.x;
			movePacket.posY = 0;
			movePacket.posZ = transform.position.z;
			_network.Send(movePacket.Write());
		}
	}
}
