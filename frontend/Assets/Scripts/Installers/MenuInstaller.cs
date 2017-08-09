using UI.Panels;
using Zenject;


namespace Installers {

	public class MenuInstaller : MonoInstaller {

		public override void InstallBindings () {
			var c = Container;
			
			c.Bind<Loop>().FromNewComponentOn(gameObject).AsSingle().NonLazy();
			
			c.Bind<ConnectionMenu>()    .FromComponentInHierarchy().AsSingle();
			c.Bind<MainMenu>()          .FromComponentInHierarchy().AsSingle();
			c.Bind<GameModeMenu>()      .FromComponentInHierarchy().AsSingle();
			c.Bind<OpponentSearchMenu>().FromComponentInHierarchy().AsSingle();
		}

	}

}
