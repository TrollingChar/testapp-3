using Assets;
using Messengers;
using Net;
using UI;
using UI.Panels;
using UnityEngine;
using War;
using Zenject;


public class W3Installer : MonoInstaller {

    public const string IdRoot = "Root";
    public const string IdBF = "BF";
    public const string IdAssets = "Assets";

    [SerializeField] private GameObject _root, _bfPrefab, _assetContainer;


    public override void InstallBindings () {
        var c = Container;

        c.Bind<PlayerInfoReceivedMessenger>().AsSingle();
        c.Bind<HubChangedMessenger>()        .AsSingle();
        c.Bind<StartGameMessenger>()         .AsSingle();

        c.Bind<ConnectionMenu>()    .FromComponentInHierarchy().AsSingle();
        c.Bind<MainMenu>()          .FromComponentInHierarchy().AsSingle();
        c.Bind<GameModeMenu>()      .FromComponentInHierarchy().AsSingle();
        c.Bind<OpponentSearchMenu>().FromComponentInHierarchy().AsSingle();

        c.BindInstance(_root)          .WithId(IdRoot);
        c.BindInstance(_bfPrefab)      .WithId(IdBF);
        c.BindInstance(_assetContainer).WithId(IdAssets);
        
        c.BindInstance(_assetContainer.GetComponent<AssetContainer>());
        c.Bind<Loop>()        .FromNewComponentOn(_root).AsSingle().NonLazy();
        c.Bind<WSConnection>().FromNewComponentOn(_root).AsSingle().NonLazy();
        c.Bind<BF>()          .FromMethod(ctx => ctx.Container.Resolve<Loop>().BF);
    }

}
