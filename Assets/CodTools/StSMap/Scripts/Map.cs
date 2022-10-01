using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodTools.StsMap
{
    public class Map
    {
        public List<Node> nodes;
        public List<Point> path;
        public string bossNodeName;
        public string configName; // similar to the act name in Slay the Spire

        public Map(string configName, string bossNodeName, List<Node> nodes, List<Point> path)
        {
            this.configName = configName;
            this.bossNodeName = bossNodeName;
            this.nodes = nodes;
            this.path = path;
        }

        #region Node Convenience Methods
        public Node GetBossNode()
        {
            return nodes.FirstOrDefault(n => n.nodeType == NodeType.Boss);
        }

        public float DistanceBetweenFirstAndLastLayers()
        {
            var bossNode = GetBossNode();
            var firstLayerNode = nodes.FirstOrDefault(n => n.point.y == 0);

            if (bossNode == null || firstLayerNode == null)
                return 0f;

            return bossNode.position.y - firstLayerNode.position.y;
        }

        public Node GetNode(Point point)
        {
            return nodes.FirstOrDefault(n => n.point.Equals(point));
        }

        public Node GetCurrentNode() {
            if (path.Count > 0) {
                return GetNode(path.Last());
            }
            else {
                Debug.LogError("GetCurrentNode called when path has no current node.");
                return nodes[0];
            }
        }

        public Node GetPreviousNode() {
            if (path.Count > 1) {
                return GetNode(path[^2]);
            }
            else {
                Debug.LogError("GetPreviousNode called when path has no previous node.");
                return nodes[0];
            }
        }
        #endregion
    }
}