using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpChat.Models
{
    internal class Message
    {
        public int Id { get; set; }
        public string? Text { get; set; }

        public DateTime Date { get; set; }

        public bool IsIncoming { get; set; } = false;

        public bool IsReceived { get; set; } = false;

        public Guid GuidId { get; } = Guid.NewGuid();

    }
}
