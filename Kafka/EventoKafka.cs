using System;
using System.Collections.Generic;

namespace RevisaoListasEx.Kafka
{
    public class EventoKafka
    {
        public Guid pedido_id { get; set; }
        public Guid cliente_id { get; set; }
        public Guid endereco_cliente_id { get; set; }
        public Guid endereco_entrega_id { get; set; }
        public List<CaractetisticasDinamicas> CaractetisticasDinamicas { get; set; }

    }

    public class CaractetisticasDinamicas
    {
        public string NomeCampo { get; set; }
        public string ValorCampo { get; set; }
    }
}
