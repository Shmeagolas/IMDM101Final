using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Holdable {
	private float ammo, reloadSpeed, magSize, moveSpeed, damage;
	private Items.Item type;

	public Gun(Items.Item type, float moveSpeed, float damage, float ammo, float reloadSpeed, float magSize) {
		this.type = type;
		this.moveSpeed = moveSpeed;
		this.damage = damage;
		this.ammo = ammo;
		this.reloadSpeed = reloadSpeed;
		this.magSize = magSize;
	}

	public Items.Item getItemType() {
		return type;
	}
	public void use() {
		
	}
}
