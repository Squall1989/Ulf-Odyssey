
using Peer = ENet.Peer;

namespace UlfServer
{
    public class PlayerServer
    {
        private string _name;
        private readonly int _id;
        private readonly Peer peer;

        public PlayerServer(string name, int id, Peer peer)
        {
            _name = name;
            _id = id;
            this.peer = peer;
        }

        public string Name { get { return _name; } }
        public int Id { get { return _id; } }
        public Peer Peer { get { return peer; } }
    }
}
