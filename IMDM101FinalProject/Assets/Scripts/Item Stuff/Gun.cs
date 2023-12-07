using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Holdable {
	private float ammo, reloadSpeed, magSize;
	private Items.Item type;

	public Gun(Items.Item type, float moveSpeed, float damage, float ammo, float reloadSpeed, float magSize, Renderer[] mesh, Animator anim) {
		name = type;
		this.speed = moveSpeed;
		this.damage = damage;
		this.ammo = ammo;
		this.reloadSpeed = reloadSpeed;
		this.magSize = magSize;

		this.mesh = mesh;
		this.anim = anim;
	}

	public Items.Item getItemType() {
		return type;
	}
	public override void Use() {

	}
}