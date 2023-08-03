using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vs_siderbar = Modelos.Modelos.vs_siderbar;

namespace ApegoContractual.Models
{
    public class Model_Sidebar
    {
        public vs_siderbar Sidebar { get; set; }
        public List<vs_siderbar> Subordinados { get; set; }

    }
}
