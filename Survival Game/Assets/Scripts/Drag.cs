using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler {

	public Transform canvas;
	public Transform old;
	private GameObject player;
	public Item item;
	public bool fastCell;

	// Use this for initialization
	void Start () {
		fastCell = false;
		canvas = GameObject.Find ("Canvas").transform;
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OnBeginDrag (PointerEventData eventData)
	{
		old = transform.parent;
		transform.SetParent (canvas);
		GetComponent<CanvasGroup> ().blocksRaycasts = false;
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		GetComponent<CanvasGroup> ().blocksRaycasts = true;
		if (transform.parent == canvas) {
			transform.SetParent (old);
		}
	}

	public void OnDrag (PointerEventData eventData)
	{
		transform.position = Input.mousePosition;
	}


	public void OnPointerClick (PointerEventData eventData)
	{
		if (eventData.button == PointerEventData.InputButton.Right) {
			player.BroadcastMessage ("remove", this);
		}
	}

}
