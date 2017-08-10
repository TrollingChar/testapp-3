using Utils.Messenger;
using War.Generation;


namespace Messengers {

    public class LandGenProgressMessenger : Messenger<float> {}
    public class LandGenCompleteMessenger : Messenger<LandGen> {}

}
