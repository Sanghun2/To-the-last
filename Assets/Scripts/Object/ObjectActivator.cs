using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    [SerializeField] GameObject[] objects;

    public void ShowObject(int index) {
        if (0 <= index && index < objects.Length) {
            for (int i = 0; i < objects.Length; i++) {
                objects[i].SetActive(i == index);
            }
        }
        else {
            Debug.LogError($"out of index => i:{index}, length:{objects.Length}");
        }
    }
}
