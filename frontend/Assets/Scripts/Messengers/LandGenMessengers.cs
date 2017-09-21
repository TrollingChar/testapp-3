using System;
using Battle.Generation;
using Utils.Messenger;


namespace Messengers {

    [Obsolete] public class LandGenProgressMessenger : Messenger<float> {}
    [Obsolete] public class LandGenCompleteMessenger : Messenger<LandGen> {}

}
