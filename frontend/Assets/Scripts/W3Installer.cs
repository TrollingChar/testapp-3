using Messengers;
using Net;
using UI;
using UI.Panels;
using UnityEngine;
using Zenject;


public class W3Installer : MonoInstaller {

    [SerializeField] private GameObject _root;

    public override void InstallBindings () {
        var c = Container;

        c.Bind<PlayerInfoReceivedMessenger>().AsSingle();
        c.Bind<HubChangedMessenger>()        .AsSingle();
        c.Bind<StartGameMessenger>()         .AsSingle();

        c.Bind<ConnectionMenu>().FromComponentInHierarchy();
        c.Bind<MainMenu>()      .FromComponentInHierarchy();
        c.Bind<GameModeMenu>()  .FromComponentInHierarchy();

        c.BindInstance(_root);
        c.Bind<Loop>()          .FromNewComponentOn(_root).AsSingle().NonLazy();
        c.Bind<WSConnection>()  .FromNewComponentOn(_root).AsSingle().NonLazy();
    }

}
