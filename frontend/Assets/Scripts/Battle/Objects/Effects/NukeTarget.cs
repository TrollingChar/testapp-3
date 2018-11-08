using Core;


namespace Battle.Objects.Effects {

    public class NukeTarget : Effector {

        public int ActivationI;


        public override void OnSpawn () {
            ActivationI = The.Battle.Teams.I + The.Battle.Teams.Teams.Count - 1;
            
            // add sprite for player that launched it
            
            // show CAUTION CAUTION for some seconds
            The.Battle.Alert.Text = "ОПАСНО!!";
            The.Battle.Alert.Alpha = 1f;
            
            The.Battle.TweenTimer.Wait (new Time {Seconds = 3f});
        }

    }

}