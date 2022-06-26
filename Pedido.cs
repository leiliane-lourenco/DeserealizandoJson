using System;

namespace RevisaoListasEx
{
    public class Pedido
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Cliente Cliente { get; set; }
        public Venda Venda { get; set; }
        public Endereco EnderecoEntrega { get; set; }

        public Pedido()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
        }
    }
}
