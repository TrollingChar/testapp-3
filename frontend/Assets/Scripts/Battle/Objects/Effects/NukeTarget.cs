using Core;


namespace Battle.Objects.Effects {

    public class NukeTarget : Effector {

        public int ActivationI;


        public override void OnSpawn () {
            var battle = The.Battle;
            ActivationI = battle.Teams.I + battle.Teams.Teams.Count - 1;
            
            if (battle.MyTurn) {
                UnityEngine.Object.Instantiate (The.BattleAssets.NukeTarget, GameObject.transform, false);
            }
            
            battle.Alert.Text = "ОПАСНО!!";
            battle.Alert.Alpha = 1f;
            
            battle.TweenTimer.Wait (new Time {Seconds = 3f});
        }

    }

}