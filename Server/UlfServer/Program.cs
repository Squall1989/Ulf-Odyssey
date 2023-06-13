using System;
using System.Collections.Generic;
using System.Text;

namespace UlfServer
{
    public class StartServer
    {
        private static ServerMain serverMain;
        public static void Main(string[] args)
        {
            serverMain = new ServerMain();
            serverMain.InitServer();
            serverMain.WaitPackage();
        }
    }
}
