using System;
using System.Collections.Generic;

namespace StreamTV.Models
{
    public partial class Videos
    {
        public int Id { get; set; }
        public int? FkIdTelevisao { get; set; }
        public string Url { get; set; }

        public virtual Televisoes FkIdTelevisaoNavigation { get; set; }
    }
}
