using System;

namespace RevisaoListasEx
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public Endereco Endereco { get; set; }

        public Cliente()
        {
            Id = Guid.NewGuid();
        }
    }

}
