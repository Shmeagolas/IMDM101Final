using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items
{
	[SerializeField] private float
		knifeDamage,
		knifeSpeed,
		akDamage,
		akSpeed,
		deagDamage,
		deagSpeed;
	[SerializeField] private MeshFilter knifeMesh, akMesh, deagMesh;

	 public enum Item {
		KNIFE,
		AK,
		DEAGLE
	}

	private Item item;
	private Holdable current;
	private Gun backup;
	private Knife knife;
	[SerializeField] private GameObject[] models;

	public Items(){
		item = Item.KNIFE;
		knife = new Knife(knifeSpeed, knifeDamage, 1f, knifeMesh);
		current = knife;
	}

	public Item Held {
		get {
			return item;
		}
		set {
			item = value;

			if(item == Item.KNIFE) {
				if(current is Gun) {
					backup = (Gun)current;
				}
				current = knife;
			}else if(item == Item.AK) {
				if(backup.getItemType() == Item.AK) {
					current = backup;
				} else {
					current = new Gun(Item.AK, akSpeed, akDamage, 1000f, .2f, 25f, akMesh);
				}
				backup = null;

			} else if(item == Item.DEAGLE) {
				if (backup.getItemType() == Item.DEAGLE) {
					current = backup;
				} else {
					current = new Gun(Item.DEAGLE, deagSpeed, deagDamage, 1000f, .5f, 6f, deagMesh);
				}
				backup = null;
			}
		}
	}

	public void Use() {
		current.Use();
	}


}
