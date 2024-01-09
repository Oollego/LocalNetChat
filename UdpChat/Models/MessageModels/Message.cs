using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UdpChat.Models.MessageModels;

namespace UdpChat.Models
{
    internal class Message: IMessage
    {
        #region 
        //public Guid GuidId { get; set; } = Guid.NewGuid();
        //public string Text { get; set; } = null!;
        //public DateTime Date { get; set; } = DateTime.Now;
        //public string FileName { get; set; } = null!;
        public long FileLength { get; set; } = 0;  //zdelat tolko get;
        public bool IsFileAdded { get; set; } = false;
        //public bool IsIncoming { get; set; } = false;
        #endregion



        public int Id { get; set; }
        public string Text { get; set; } = null!;

        //public Guid GuidId { get; set; }
        //public DateTime Date { get; set; } = DateTime.Now;
        public byte[] FileData { get; set; } = null!;
        public string FileName { get; set; } = null!;
        public bool IsIncoming { get; set; } = false;


        // public string IpAddress { get; set; } = null!;


        //  public DateTime Date { get; set; } = DateTime.Now;

        //  public IPAddress IpAddress { get; set; } = null!;


        public bool IsReceived { get; set; } = false;

        // public Guid GuidId { get; } = Guid.NewGuid();



    }
}
