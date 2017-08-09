using Messengers;
using Net;
using Zenject;


namespace Installers {

    public class GlobalInstaller : MonoInstaller {

        public override void InstallBindings () {
            var c = Container;

            c.Bind<PlayerInfoReceivedMessenger>().AsSingle();
            c.Bind<HubChangedMessenger>()        .AsSingle();
            c.Bind<StartGameMessenger>()         .AsSingle();
            
            c.Bind<WSConnection>().FromNewComponentOn(gameObject).AsSingle();
            c.Bind<SceneSwitcher>().AsSingle();
            c.Bind<PlayerInfo>()   .AsSingle();
            c.Bind<int>().WithId(Injectables.Id).FromMethod(ctx => ctx.Container.Resolve<PlayerInfo>().Id);
        }

    }

}
