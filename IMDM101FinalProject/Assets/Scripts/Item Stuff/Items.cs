using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items {
	[SerializeField]
	private float
		knifeDamage,
		knifeSpeed,
		akDamage,
		akSpeed,
		deagDamage,
		deagSpeed;

	public enum Item {
		KNIFE,
		AK,
		DEAGLE
	}

	private Item item;
	private Holdable current;
	private Gun backup;
	private Knife knife;
	private List<Renderer[]> models;
	private List<Animator> animators;

	public Items(List<Renderer[]> models, List<Animator> animators) {
		this.models = models;
		this.animators = animators;

		knife = new Knife(knifeSpeed, knifeDamage, 1f, models[0], animators[0]);

		Held = Item.KNIFE;
	}



	public Item Held {
		get {
			return item;
		}
		set {
			item = value;

			if (item == Item.KNIFE) {
				if (current is Gun) {
					backup = (Gun)current;
				}
				
				if (current != null) {
					foreach(Renderer rend in current.mesh) {
						rend.enabled = false;
					}
				}
				current = knife;
				foreach (Renderer rend in current.mesh) {
					rend.enabled = true;
				}
				current.mesh[3].enabled = false;
			} else if (item == Item.AK) {
				foreach(Renderer rend in current.mesh) {
					rend.enabled = false;
				}
				if (backup.getItemType() == Item.AK) {
					current = backup;
				} else {
					current = new Gun(Item.AK, akSpeed, akDamage, 1000f, .2f, 25f, models[1], animators[1]);
				}
				backup = null;
				foreach (Renderer rend in current.mesh) {
					rend.enabled = true;
				}

			} else if (item == Item.DEAGLE) {
				foreach (Renderer rend in current.mesh) {
					rend.enabled = false;
				}
				if (backup.getItemType() == Item.DEAGLE) {
					current = backup;
				} else {
					current = new Gun(Item.DEAGLE, deagSpeed, deagDamage, 1000f, .5f, 6f, models[2], animators[1]);
				}
				backup = null;
				foreach (Renderer rend in current.mesh) {
					rend.enabled = true;
				}
			}
		}
	}

	public void Use() {
		current.Use();
	}


}

