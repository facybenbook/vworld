﻿using UnityEngine;
using System.Collections;

public class GameModeCreative : GameMode {
	protected override void HandleInput() {
		base.HandleInput();
		if(!pauseMenuUp) {
			if (Input.GetMouseButtonDown(1)) {
				RaycastHit hit;
				if (Physics.Raycast(transform.GetChild(0).position, transform.GetChild(0).forward,out hit, 100 )) {
					EditTerrain.SetBlock(hit, new BlockAir());
				}
			} else if (Input.GetMouseButtonDown(0)) {
				RaycastHit hit;
				if (Physics.Raycast(transform.GetChild(0).position, transform.GetChild(0).forward,out hit, 100 )) {
					EditTerrain.SetBlock(hit, new BlockDirt(), true);
				}
			}
		}
		transform.position += transform.forward * 3 * Input.GetAxis("Vertical");
		transform.position += transform.right * 3 * Input.GetAxis("Horizontal");
		transform.position += transform.up * 3 * Input.GetAxis("UpDown");
	}
}
