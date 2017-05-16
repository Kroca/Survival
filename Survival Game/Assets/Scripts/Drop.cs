using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drop : MonoBehaviour, IDropHandler {

	public Inventory inv;

    public void Start()
    {
        inv = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }
    public void OnDrop (PointerEventData eventData)
	{
		Drag drag = eventData.pointerDrag.GetComponent<Drag> ();
		if (drag != null) {
			if (transform.childCount > 0) {
				transform.GetChild (0).SetParent (drag.old);
			}
			drag.transform.SetParent (transform);
            inv.setSlot();
		}
	}
		
}
