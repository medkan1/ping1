using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp2
{
    public class PingMonitor
    {
        public string IpAddress { get; set; }
        public List<PingReply> PingResults { get; set; }
        public double Jitter { get; set; }
        public double Latency { get; set; }
        public double PacketLoss { get; set; }

        public PingMonitor()
        {
            PingResults = new List<PingReply>();
        }

        public void SendPingRequests()
        {
            Ping ping = new Ping();
            PingOptions pingOptions = new PingOptions();
            pingOptions.DontFragment = true;

            foreach (PingReply pingReply in ping.SendPingAsync(IpAddress, pingOptions).Result)
            {
                PingResults.Add(pingReply);
            }
        }

        public void CalculateJitterLatencyAndPacketLoss()
        {
            // Calculate the average time difference between successive ping requests.
            double jitter = 0;
            for (int i = 1; i < PingResults.Count; i++)
            {
                jitter += PingResults[i].RoundtripTime.TotalMilliseconds - PingResults[i - 1].RoundtripTime.TotalMilliseconds;
            }
            jitter /= PingResults.Count - 1;

            // Calculate the average time it takes for a ping request to be sent and received.
            double latency = 0;
            foreach (PingReply pingReply in PingResults)
            {
                latency += pingReply.RoundtripTime.TotalMilliseconds;
            }
            latency /= PingResults.Count;

            // Calculate the percentage of ping requests that are not successful.
            double packetLoss = 0;
            foreach (PingReply pingReply in PingResults)
            {
                if (pingReply.Status != IPStatus.Success)
                {
                    packetLoss++;
                }
            }
            packetLoss /= PingResults.Count * 100;

            // Set the Jitter, Latency, and PacketLoss properties.
            Jitter = jitter;
            Latency = latency;
            PacketLoss = packetLoss;
        }
    }

}
