using System.Collections.Generic;
using System.IO;
using System.Linq;
using LogViewer.Web.Api.Models;

namespace LogViewer.Web.Api
{
    public class ClientDataExtractor
    {
        public List<ClientData> Extract(string path)
        {
            if (!File.Exists(path)) return new List<ClientData>();

            var dictionary = new Dictionary<string, int>();

            foreach (var line in File.ReadLines(path))
            {
                if (line.StartsWith("#")) continue;

                var splitted = line.Split(' ');

                if (splitted.Length < 9) continue;

                var clientIp = splitted[8];

                if (!dictionary.ContainsKey(clientIp))
                    dictionary.Add(clientIp, 0);
                else
                    dictionary[clientIp]++;
            }

            return dictionary
                .Select(kvp => new ClientData { ClientIp = kvp.Key, NumberOfCalls = kvp.Value })
                .OrderByDescending(kvp => kvp.NumberOfCalls)
                .ToList();

        }
    }
}
