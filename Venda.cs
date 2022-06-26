using System;
using System.Collections.Generic;

namespace RevisaoListasEx
{
    public class Venda
    {
        public Guid Id { get; set; }
        public DateTime Data { get; set; }
        public List<VendaItem> Itens{ get; set; }

        public Venda()
        {
            Itens = new List<VendaItem>();
            Id = Guid.NewGuid();
            Data = DateTime.Now;
        }
    }
}
