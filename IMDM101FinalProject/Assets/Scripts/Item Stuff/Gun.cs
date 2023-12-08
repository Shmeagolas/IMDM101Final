using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Gun : Holdable {
	private float ammo, reloadSpeed, magSize;
	private Items.Item type;
	private bool canShoot;

	public Gun(Items.Item type, float speed, float damage, float ammo, float reloadSpeed, float magSize, Renderer[] mesh, Animator anim) {
		name = type;
		this.speed = speed;
		this.damage = damage;
		this.ammo = ammo;
		this.reloadSpeed = reloadSpeed;
		this.magSize = magSize;
		canShoot = true;
		this.mesh = mesh;
		this.anim = anim;
	}

	public Items.Item getItemType() {
		return type;
	}
	public override async void UseAsync(Transform hit, float distance) {
		if (canShoot) {
			anim.SetTrigger("shoot");
			canShoot = false;
			await Wait(speed);
			canShoot = true;


			if (hit.gameObject.GetComponent<SlimeBehaviour>()) {
				hit.gameObject.GetComponent<SlimeBehaviour>().Damage(damage);
			}

		}
	}


	private async Task Wait(float seconds) {
		await Task.Delay(Mathf.FloorToInt(seconds * 1000f));
	}
}