using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable {

	internal float speed, damage;
	internal Items.Item name;
	internal Renderer[] mesh;
	internal Animator anim;
	public virtual void UseAsync(Transform hit, float distance) { }


}