using System;

namespace RevisaoListasEx
{
    public class Endereco
    {
        public Guid Id { get; set; }
        public string Rua { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }

        public Endereco()
        {
            Id = Guid.NewGuid();
        }
    }
}
