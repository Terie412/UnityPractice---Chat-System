using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

[ExecuteAlways]
public class ScrollViewMgr : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public GameObject prefab;
    public bool autoMoveToBottom;
    public int maxCache;
    public int cellWidth;
    public int cellHeight;

    private LoopScrollPrefabSource m_prefabSource;
    private LoopScrollPrefabSource prefabSource
    {
        get
        {
            if(m_prefabSource == null)
            {
                //m_prefabSource = new LoopScrollPrefabSource(prefab.name);
            }
            return m_prefabSource;
        }
    }
    private List<object> dataCache;
    private int dataHeadIndex;
    private int dataTailIndex;

    private ScrollRect scrollRect;
    private RectTransform viewPort;
    private RectTransform content;

    void Awake()
    {
        dataHeadIndex = -1;
        dataTailIndex = -1;

        scrollRect = this.GetComponent<ScrollRect>();
        viewPort = scrollRect.viewport;
        content = scrollRect.content;
    }

    private void LateUpdate()
    {
        UpdateContent();
    }

    public void AddCell(object message)
    {
        GameObject go = m_prefabSource.GetObject();
        go.transform.SetParent(content);
        go.transform.SetAsLastSibling();
        go.SetActive(true);

        dataCache.Add(message);
        go.SendMessage("ProvideData", message, SendMessageOptions.DontRequireReceiver);

        UpdateContent();
    }

    private void UpdateContent()
    {
        int childCount = content.childCount;
        content.sizeDelta = new Vector2(content.sizeDelta.x, childCount * cellHeight);

        
    }

    //ScrollRect scrollRect;
    //RectTransform viewPort;
    //RectTransform content;
    //List<GameObject> cellList = new List<GameObject>();
    //Vector2 targetAnchorPosition;
    //float moveDeltaTime = 0.3f;
    //float nowSlerpTime = 0.0f;
    //bool ifUpdatePos = false;
    //Text countLengthText;
    //public int maxCellCount = 30;
    //public delegate void ModifyGameObjectHandler(GameObject go, object goInfo);
    //public ModifyGameObjectHandler mgHandler; // 这里书写修改元素信息的逻辑



    //public void AddCell(GameObject cellPrefab, object goInfo)
    //{
    //    GameObject cell;

    //    if (cellList.Count < maxCellCount)
    //    {
    //        cell = Instantiate(cellPrefab, content);
    //        cellList.Add(cell);
    //        cell.name = (cellList.Count).ToString();
    //    }
    //    else
    //    {
    //        cell = content.GetChild(0).gameObject;
    //        cell.transform.SetAsLastSibling();
    //    }

    //    mgHandler(cell, goInfo);

    //    RectTransform rt = cell.transform as RectTransform;
    //    rt.sizeDelta = new Vector2(content.rect.width, rt.sizeDelta.y);
    //    ResizeContent();
    //    GoToBottom();
    //}

    //private void ResizeContent()
    //{
    //    float childHeight = 0;
    //    for(int i=0;i<content.transform.childCount;i++)
    //    {
    //        var child = content.transform.GetChild(i);
    //        childHeight += child.GetComponent<RectTransform>().sizeDelta.y;
    //    }
    //    if(content.sizeDelta.y < childHeight)
    //    {
    //        content.sizeDelta = content.sizeDelta + new Vector2(0, childHeight - content.sizeDelta.y);
    //    }
    //}

    //private void GoToBottom()
    //{
    //    float offset = content.sizeDelta.y - viewPort.rect.height;
    //    offset = offset < 0 ? 0 : offset;
    //    targetAnchorPosition = new Vector2(0, offset);
    //    TweenUtil.ChangeAnchorPositionTo(content, targetAnchorPosition, 0.3f);
    //}

    //private void OnDisable()
    //{
    //    for(int i=0;i<cellList.Count;i++)
    //    {
    //        GameObject go = cellList[i];
    //        Destroy(go);
    //    }
    //    cellList.Clear();
    //}

    #region 接口方法
    public void OnBeginDrag(PointerEventData eventData)
    {
       
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
    #endregion
}
