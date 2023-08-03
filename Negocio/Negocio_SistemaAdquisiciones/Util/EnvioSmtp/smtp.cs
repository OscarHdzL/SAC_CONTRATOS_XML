using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeAdquisiciones.Util.EnvioSmtp
{
    public class smtp
    {
        public String Body { get; set; }
        public List<String> Email { get; set; }
        public String Asunto { get; set; }
    }
}
