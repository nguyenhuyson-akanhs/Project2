using UnityEngine;
using UnityEditor;
using AYellowpaper.SerializedCollections;

// [SỬA 1]: Đổi typeof thành tên Class C# chứa Dictionary của bạn
[CustomEditor(typeof(ItemManager))]
public class ItemListEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        // [SỬA 2]: Đổi "" thành tên nút bấm bạn muốn hiển thị trên Inspector
        if (GUILayout.Button("Auto-Fill Keys from Item SO IDs", GUILayout.Height(30)))
        {
            // [SỬA 3]: Đổi "" thành ĐÚNG TÊN BIẾN Dictionary trong script chính của bạn
            SerializedProperty dictProp = serializedObject.FindProperty("_ItemList");
            if (dictProp == null) {Debug.Log("null here"); return; }

            // [GIỮ NGUYÊN]: "_serializedList" là tên biến nội bộ của thư viện SerializedDictionary, KHÔNG SỬA
            SerializedProperty keyValuesProp = dictProp.FindPropertyRelative("_serializedList");
            if (keyValuesProp == null) Debug.Log("null here");

            if (keyValuesProp != null && keyValuesProp.isArray)
            {
                // [SỬA 4]: Đổi "" thành tên hành động Undo (tùy chỉnh tùy ý)
                Undo.RecordObject(target, "Auto Fill Dictionary Keys");

                for (int i = 0; i < keyValuesProp.arraySize; i++)
                {
                    SerializedProperty element = keyValuesProp.GetArrayElementAtIndex(i);

                    // [GIỮ NGUYÊN]: "Key" và "Value" là tên thuộc tính nội bộ của SerializedDictionary, KHÔNG SỬA
                    SerializedProperty keyProp = element.FindPropertyRelative("Key");
                    SerializedProperty valueProp = element.FindPropertyRelative("Value");

                    // [SỬA 5]: Đổi 'ItemSO' thành tên class ScriptableObject của bạn
                    ItemSO itemSO = valueProp.objectReferenceValue as ItemSO;

                    // [SỬA 6]: Đổi 'itemID' thành ĐÚNG TÊN BIẾN chứa ID trong class ItemSO của bạn
                    if (itemSO != null && !string.IsNullOrEmpty(itemSO._NameID))
                    {
                        keyProp.stringValue = itemSO._NameID;
                    }
                }

                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(target);

                // [SỬA 7]: Đổi nội dung thông báo hiển thị dưới Console theo ý bạn
                Debug.Log("Cập nhật Key thành công!");
            }
        }
    }
}