using Messengers;
using Net;
using UnityEngine;
using Zenject;


public class W3Installer : MonoInstaller {

    [SerializeField] private GameObject _root;

    public override void InstallBindings () {
        var c = Container;

        c.Bind<PlayerInfoReceivedMessenger>().AsSingle();
        c.Bind<HubChangedMessenger>().AsSingle();
        c.Bind<StartGameMessenger>().AsSingle();

        c.BindInstance(_root);
        c.Bind<Loop>()        .FromNewComponentOn(_root).AsSingle().NonLazy();
        c.Bind<WSConnection>().FromNewComponentOn(_root).AsSingle().NonLazy();
    }

}
