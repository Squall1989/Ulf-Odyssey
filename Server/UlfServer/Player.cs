
using Peer = ENet.Peer;

namespace UlfServer
{
    public class PlayerServer
    {
        private string _name;
        private readonly ulong _id;
        private readonly Peer peer;

        public PlayerServer(string name, ulong id, Peer peer)
        {
            _name = name;
            _id = id;
            this.peer = peer;
        }

        public string Name { get { return _name; } }
        public ulong Id { get { return _id; } }
        public Peer Peer { get { return peer; } }
    }
}
