using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UdpChat.Data;
using UdpChat.Models.MessageModels;

namespace UdpChat.Models
{
    internal class ServMessage: IMessage
    {
       // public Guid GuidId { get; set; }
      //  public DateTime CreationDate { get; set; } = DateTime.Now;
        public MethodKey MethodName { get; set; }

       // public IPAddress IpAddress { get; set; } = null!;

    }
}
