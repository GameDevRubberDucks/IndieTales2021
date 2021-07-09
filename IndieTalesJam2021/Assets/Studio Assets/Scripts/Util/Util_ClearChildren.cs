using UnityEngine;

namespace RubberDucks.Util
{
    public class Util_ClearChildren : MonoBehaviour
    {
        //--- Static Methods ---//
        public static void ClearChildren(Transform _transform, bool _destroyImmediate = false)
        {
            // Destroy the normal method or destroy immediate (immediate is usually used in editor)
            if (_destroyImmediate)
            {
                // Immediate destruction removes the child from the list of children right away
                // So, we need to keep looping until the list is empty
                // Otherwise, we'll only remove half of the children since the childCount variable will keep decreasing
                while (_transform.childCount > 0)
                    DestroyChild(_transform, 0, true);
            }
            else
            {
                // Normal destruction does NOT remove the child from the list right away
                // So, we need to iterate through the list fully
                // Waiting until the list is empty will result in an infinite loop
                for (int i = 0; i < _transform.childCount; i++)
                    DestroyChild(_transform, i, false);
            }
        }

        public static void ClearChildren(GameObject _gameObject, bool _destroyImmediate = false)
        {
            ClearChildren(_gameObject.transform, _destroyImmediate);
        }



        //--- Utility Methods ---//
        private static void DestroyChild(Transform _parent, int _childIndex, bool _destroyImmediate)
        {
            // Get the object reference
            GameObject child = _parent.GetChild(_childIndex).gameObject;

            // Use the correct destruction method, either the immediate version or the default
            if (_destroyImmediate)
                DestroyImmediate(child);
            else
                Destroy(child);
        }
    }
}
