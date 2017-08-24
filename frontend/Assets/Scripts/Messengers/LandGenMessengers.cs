using Battle.Generation;
using Utils.Messenger;


namespace Messengers {

    public class LandGenProgressMessenger : Messenger<float> {}


    public class LandGenCompleteMessenger : Messenger<LandGen> {}

}
