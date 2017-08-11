using Scenes;
using UI.Panels;
using UnityEngine;
using War;
using War.Camera;
using Zenject;


namespace Installers {

    public class BattleInstaller : MonoInstaller {

        public override void InstallBindings () {
            var c = Container;

            c.Bind<BF>().FromComponentInHierarchy().AsSingle();

            c.Bind<BattleSceneInitializer>().FromNewComponentOn(gameObject).AsSingle().NonLazy();
            c.Bind<GameInitData>().FromMethod(
                ctx => ctx.Container.Resolve<SceneSwitcher>().Data[0] as GameInitData
            ).AsSingle();
            
            c.Bind<CameraWrapper>().FromComponentInHierarchy().AsSingle();
            c.Bind<HintArea>()     .FromComponentInHierarchy().AsSingle();
        }

    }

}
