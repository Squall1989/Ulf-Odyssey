
using System;
using Unity.Collections;
using Unity.Networking.Transport;

namespace Unity.Networking.Transport
{

    public static class ConnectExtension
    {
        public static int GetConnectionId(this NetworkConnection connection)
        {
            int idStartFrom = "NetworkConnection[id{".Length;
            string endStr = ",v";
            string idStr = connection.ToString();
            int idEndFrom = idStr.IndexOf(endStr);

            var idSubStr = idStr.Substring(idStartFrom -1, idEndFrom - idStartFrom +1);
            int id = Convert.ToInt32(idSubStr);
            return id;
        }
    }
}