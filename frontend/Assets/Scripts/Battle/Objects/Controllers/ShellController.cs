using DataTransfer.Data;
using UnityEngine;


namespace Battle.Objects.Controllers {

    // todo сделать лучше ControllerBuilder.MakeNew.GravityAffected.WindAffected.DirectionChanging.Controller;
    // todo и не создавать класс под каждый объект
    public class ShellController : StandardController {

        protected override void DoUpdate (TurnData td) {
            Object.GameObject.transform.localEulerAngles = new Vector3(0, 0, Object.Velocity.Angle * Mathf.Rad2Deg);
            base.DoUpdate(td);
            Wait();
        }

    }

}
