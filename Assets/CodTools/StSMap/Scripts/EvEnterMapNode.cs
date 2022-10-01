using System.Collections.Generic;
using CodTools.Utilities;

namespace CodTools.StsMap
{
    public class EvEnterMapNode : IGameEvent
    {
        public MapNode MapNode { get; }
        public List<Point> Path { get; }

        public EvEnterMapNode(MapNode mapNode, List<Point> path) {
            MapNode = mapNode;
            Path = path;
        }
    }
}
