using UnityEngine;
using War;
using War.Camera;
using Zenject;


namespace Installers {

    public class BattleInstaller : MonoInstaller {

        public override void InstallBindings () {
            var c = Container;

            c.Bind<BF>().AsSingle().NonLazy();
            var root = GameObject.Find("Root");
            c.BindInstance(root).WhenInjectedInto<BF>();
            c.Bind<GameInitData>().FromMethod(
                ctx => ctx.Container.Resolve<SceneSwitcher>().Data[0] as GameInitData
            ).AsSingle();
            c.Bind<CameraWrapper>().FromComponentInHierarchy().AsSingle();
        }

    }

}
