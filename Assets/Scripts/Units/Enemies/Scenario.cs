

namespace Terra{

    public class Scenario
    {
        public float scenarioValue;
        public OverlayTile targetTile;
        public OverlayTile positionTile;
        
        public Scenario(float scenarioValue, OverlayTile targetTile, OverlayTile positionTile){
            this.scenarioValue = scenarioValue;
            this.targetTile = targetTile;
            this.positionTile = positionTile;
        }

        public Scenario()
        {
            this.scenarioValue = -100000;
            this.targetTile = null;
            this.positionTile = null;
        }
    }
}