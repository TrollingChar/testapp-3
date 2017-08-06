using Net;
using Ninject;
using Ninject.Modules;
using UI;
using Utils.Singleton;


namespace Bindings {

    public class W3Module : NinjectModule {

        private Core _core;


        public W3Module (Core core) {
            _core = core;
        }


        public override void Load () {
            Bind<Core>().ToConstant(_core);
            Bind<WSConnection>()
                .ToMethod(ctx => ctx.Kernel.Get<Core>().GetComponent<WSConnection>())
                .InSingletonScope();
            Bind<CoreEvents>().ToMethod(ctx => ctx.Kernel.Get<Core>().GetComponent<CoreEvents>()).InSingletonScope();
            Bind<int>().ToMethod(ctx => ctx.Kernel.Get<Core>().Id).Named("Id");
        }

    }

}
