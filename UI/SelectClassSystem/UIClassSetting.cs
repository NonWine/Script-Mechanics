using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class UIClassSetting : MonoBehaviour
{
    [SerializeField] private GameObject outlineObject;
    [SerializeField] private GameObject buttonExplain;
    [SerializeField] private GameObject explainPanel;
    
    public void TurnOnClassSetting()
    {
        if (!outlineObject.activeInHierarchy)
            buttonExplain.SetActive(true);
        outlineObject.SetActive(true);
    }

    public GameObject GetExplainPanel() { return explainPanel; }
    public GameObject GetButtonExplain() { return buttonExplain; }

    public GameObject GetOutlineObjcet() { return outlineObject; }


}
