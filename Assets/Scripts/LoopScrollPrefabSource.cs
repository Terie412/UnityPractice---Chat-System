using UnityEngine;
using System.Collections;

namespace UnityEngine.UI
{
    [System.Serializable]
    public class LoopScrollPrefabSource
    {
        public string prefabName;

        public LoopScrollPrefabSource(string prefabName)
        {
            this.prefabName = prefabName;
        }

        public virtual GameObject GetObject()
        {
            return SG.ResourceManager.Instance.GetObjectFromPool(prefabName);
        }

        public virtual void ReturnObject(Transform go)
        {
            go.SendMessage("ScrollCellReturn", SendMessageOptions.DontRequireReceiver);
            SG.ResourceManager.Instance.ReturnObjectToPool(go.gameObject);
        }
    }
}
