using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNetwork : MonoBehaviour
{
	private PlayerControls playerControls;

	private PlayerControls PlayerControls
	{
		get
		{
			if (playerControls != null)
			{
				return playerControls;
			}
			return playerControls = new PlayerControls();
		}
	}

	private void Update()
	{
		
	}



}
