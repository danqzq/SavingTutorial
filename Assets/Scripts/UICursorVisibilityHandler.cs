using UnityEngine;
using UnityEngine.EventSystems;

namespace Dan
{
    public class UICursorVisibilityHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            Cursor.visible = true;
            TileBuilder.CanManuallyBuild = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Cursor.visible = false;
            TileBuilder.CanManuallyBuild = true;
        }
    }
}