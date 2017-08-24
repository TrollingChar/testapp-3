using Attributes;


namespace Commands.Client {

    [ClientCommand(1)]
    public class EnterHubCommand : ClientCommand {

        //[Transfer]
        private byte _hubId;

    }

}
