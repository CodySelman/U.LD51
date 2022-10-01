using System;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using CodTools.Utilities;

namespace CodTools.StsMap
{
    public class MapPlayerTracker : MonoBehaviour
    {
        public bool lockAfterSelecting = false;
        public float enterNodeDelay = 1f;
        public MapManager mapManager;
        public MapView view;

        public static MapPlayerTracker Instance;

        public bool Locked { get; set; }

        private void Awake()
        {
            // singleton setup
            if (Instance == null) {
                Instance = this;
            } else if (Instance != this) {
                Destroy(gameObject);
            }
        }

        public void SelectNode(MapNode mapNode)
        {
            if (Locked) return;

            if (MapManager.Instance.CurrentMap.path.Count == 0)
            {
                // player has not selected the node yet, he can select any of the nodes with y = 0
                if (mapNode.Node.point.y == 0)
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed();
            }
            else
            {
                Point currentPoint = MapManager.Instance.CurrentMap.path[^1];
                Node currentNode = MapManager.Instance.CurrentMap.GetNode(currentPoint);

                if (currentNode != null && currentNode.outgoing.Any(point => point.Equals(mapNode.Node.point)))
                    SendPlayerToNode(mapNode);
                else
                    PlayWarningThatNodeCannotBeAccessed();
            }
        }

        private void SendPlayerToNode(MapNode mapNode)
        {
            Locked = lockAfterSelecting;
            MapManager.Instance.CurrentMap.path.Add(mapNode.Node.point);
            // TODO saving
            // MapManager.Instance.SaveMap();
            
            view.SetAttainableNodes();
            view.SetLineColors();
            mapNode.ShowSelectionAnimation();

            DOTween.Sequence().AppendInterval(enterNodeDelay).OnComplete(() => EnterNode(mapNode));
        }

        private static void EnterNode(MapNode mapNode)
        {
            // we have access to blueprint name here as well
            // Debug.Log("Entering node: " + mapNode.Node.blueprintName + " of type: " + mapNode.Node.nodeType);
            EvEnterMapNode ev = new EvEnterMapNode(mapNode, MapManager.Instance.CurrentMap.path);
            if (EventManager.instance is not null) {
                EventManager.instance.Raise(ev);
            }
        }

        private void PlayWarningThatNodeCannotBeAccessed()
        {
            Debug.Log("Selected node cannot be accessed");
        }
    }
}