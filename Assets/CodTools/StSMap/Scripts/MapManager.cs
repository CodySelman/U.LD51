using System.Linq;
using UnityEngine;

namespace CodTools.StsMap
{
    public class MapManager : MonoBehaviour
    {
        public static MapManager Instance;

        public MapConfig config;
        public MapView view;
        public MapPlayerTracker playerTracker;

        public Map CurrentMap { get; set; }

        void Awake() {
            // singleton setup
            if (Instance == null) {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
            } else if (Instance != this) {
                Destroy(gameObject);
            }
        }

        public void GenerateNewMap()
        {
            var map = MapGenerator.GetMap(config);
            CurrentMap = map;
            view.ShowMap(map);
        }

        public void ShowMap() {
            view.ShowMap(CurrentMap);
        }

        public void ToggleMapView() {
            // TODO play show/hide animation
            view.ToggleMapVisible();
        }
    }
}
