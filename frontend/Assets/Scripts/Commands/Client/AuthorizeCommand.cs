using Attributes;


namespace Commands.Client {

    [ClientCommand(0)]
    public class AuthorizeCommand : ClientCommand {

        private string _ip;
        private string _id;

    }

}
