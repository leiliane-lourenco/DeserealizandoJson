using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RevisaoListasEx.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace RevisaoListasEx
{
    class Program
    {

        private static string ObterDoJson(string chaveJson, JObject json)
        {
            return json.SelectToken(chaveJson).Value<string>();
        }

        static void Main(string[] args)
        {
            var json = System.IO.File.ReadAllText("Json/dados-venda.json");

            var jsonObject = JObject.Parse(json);

            var pedidoId = ObterDoJson("Pedido.Id", jsonObject);
            var clienteId = ObterDoJson("Pedido.Cliente.Id", jsonObject);
            var enderecoClienteId = ObterDoJson("Pedido.Cliente.Endereco.Id", jsonObject);
            var enderecoEntregaId = ObterDoJson("Pedido.EnderecoEntrega.Id", jsonObject);

            var caracteristicasDinamicas = new List<CaractetisticasDinamicas>();

            foreach (var jsonFirstToken in jsonObject.Children<JProperty>())
            {
                var date = new CaractetisticasDinamicas();
                var clienteNome = new CaractetisticasDinamicas();
                var clienteTelefone = new CaractetisticasDinamicas();
                var endereçoRua = new CaractetisticasDinamicas();
                var enderecoNumero = new CaractetisticasDinamicas();
                var enderecoComplemento = new CaractetisticasDinamicas();
                var enderecoCep = new CaractetisticasDinamicas();
                var vendaData = new CaractetisticasDinamicas();
                var vendaId = new CaractetisticasDinamicas();
                var vendaItemId = new CaractetisticasDinamicas();
                var vendaItemDescricao = new CaractetisticasDinamicas();
                var vendaItemValor = new CaractetisticasDinamicas();
                var vendaItemQtd = new CaractetisticasDinamicas();
                var entregaRua = new CaractetisticasDinamicas();
                var entregaNumero = new CaractetisticasDinamicas();
                var entregaComplemento = new CaractetisticasDinamicas();
                var entregaCep = new CaractetisticasDinamicas();

                foreach (var jsonSecondToken in jsonFirstToken.Children())
                {
                    foreach (JProperty jsonId in jsonSecondToken)
                    {
                        if (jsonId.Path.Contains("Date"))
                        {
                            date.NomeCampo = jsonId.Path;
                            date.ValorCampo = jsonId.Value.ToString();
                            caracteristicasDinamicas.Add(date);
                        }
                    }

                    foreach (JProperty jsonToken in jsonSecondToken.Children().Children().Children())
                    {
                        if (jsonToken.Path.Contains("Cliente"))
                        {
                            if (jsonToken.Path.Contains("Nome"))
                            {
                                clienteNome.NomeCampo = jsonToken.Path;
                                clienteNome.ValorCampo = jsonToken.Value.ToString();
                                caracteristicasDinamicas.Add(clienteNome);

                            }
                            if (jsonToken.Path.Contains("Telefone"))
                            {
                                clienteTelefone.NomeCampo = jsonToken.Path;
                                clienteTelefone.ValorCampo = jsonToken.Value.ToString();
                                caracteristicasDinamicas.Add(clienteTelefone);

                            }
                            if (jsonToken.Path.Contains("Endereco"))
                            {
                                foreach (var jsonTokenNew in jsonToken)
                                {
                                    foreach (JProperty pro in jsonTokenNew)
                                    {
                                        if (pro.Path.Contains("Rua"))
                                        {
                                            endereçoRua.NomeCampo = pro.Path;
                                            endereçoRua.ValorCampo = pro.Value.ToString();
                                            caracteristicasDinamicas.Add(endereçoRua);
                                        }
                                        if (pro.Path.Contains("Numero"))
                                        {
                                            enderecoNumero.NomeCampo = pro.Path;
                                            enderecoNumero.ValorCampo = pro.Value.ToString();
                                            caracteristicasDinamicas.Add(enderecoNumero);
                                        }
                                        if (pro.Path.Contains("Complemento"))
                                        {
                                            enderecoComplemento.NomeCampo = pro.Path;
                                            enderecoComplemento.ValorCampo = pro.Value.ToString();
                                            caracteristicasDinamicas.Add(enderecoComplemento);
                                        }
                                        if (pro.Path.Contains("Cep"))
                                        {
                                            enderecoCep.NomeCampo = pro.Path;
                                            enderecoCep.ValorCampo = pro.Value.ToString();
                                            caracteristicasDinamicas.Add(enderecoCep);
                                        }
                                    }
                                }
                            }

                        }

                        if (jsonToken.Path.Contains("Venda"))
                        {
                            if (jsonToken.Path.Contains("Id"))
                            {
                                vendaId.NomeCampo = jsonToken.Path;
                                vendaId.ValorCampo = jsonToken.Value.ToString();
                                caracteristicasDinamicas.Add(vendaId);

                            }
                            if (jsonToken.Path.Contains("Data"))
                            {
                                vendaData.NomeCampo = jsonToken.Path;
                                vendaData.ValorCampo = jsonToken.Value.ToString();
                                caracteristicasDinamicas.Add(vendaData);

                            }
                            if (jsonToken.Path.Contains("Itens"))
                            {
                                vendaItemId.NomeCampo = jsonToken.Path;
                                vendaItemId.ValorCampo = jsonToken.Value.ToString();
                                caracteristicasDinamicas.Add(vendaItemId);
                            }
                        }

                        if (jsonToken.Path.Contains("EnderecoEntrega"))
                        {
                            if (jsonToken.Path.Contains("Rua"))
                            {
                                entregaRua.NomeCampo = jsonToken.Path;
                                entregaRua.ValorCampo = jsonToken.Value.ToString();
                                caracteristicasDinamicas.Add(entregaRua);
                            }
                            if (jsonToken.Path.Contains("Numero"))
                            {
                                entregaNumero.NomeCampo = jsonToken.Path;
                                entregaNumero.ValorCampo = jsonToken.Value.ToString();
                                caracteristicasDinamicas.Add(entregaNumero);
                            }
                            if (jsonToken.Path.Contains("Complemento"))
                            {
                                entregaComplemento.NomeCampo = jsonToken.Path;
                                entregaComplemento.ValorCampo = jsonToken.Value.ToString();
                                caracteristicasDinamicas.Add(entregaComplemento);
                            }
                            if (jsonToken.Path.Contains("Cep"))
                            {
                                entregaCep.NomeCampo = jsonToken.Path;
                                entregaCep.ValorCampo = jsonToken.Value.ToString();
                                caracteristicasDinamicas.Add(entregaCep);
                            }

                        }
                    }
                }
            }
            var jsonCaracteristica = JsonConvert.SerializeObject(caracteristicasDinamicas);

            Console.ReadKey();


        }
    }
}