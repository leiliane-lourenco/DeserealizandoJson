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
                var caracteristicaDinamica = new CaractetisticasDinamicas();

                foreach (var jsonThirdToken in jsonFirstToken.Children())
                {
                    if (jsonThirdToken.Path.Contains("Pedido"))
                    {
                        foreach (JProperty jsonId in jsonThirdToken)
                        {
                            if (jsonId.Path.Contains("Date"))
                            {
                                caracteristicaDinamica.NomeCampo = jsonId.Path;
                                caracteristicaDinamica.ValorCampo = jsonId.Value.ToString();
                                caracteristicasDinamicas.Add(caracteristicaDinamica);
                                Console.WriteLine(jsonId.Path);
                            }
                        }
                    }
                    foreach (JProperty jsonToken in jsonThirdToken.Children().Children().Children())
                    {
                        if (jsonToken.Path.Contains("Cliente"))
                        {
                            if (jsonToken.Path.Contains("Cliente.Endereco"))
                            {
                                foreach (var jsonTokenNew in jsonToken)
                                {
                                    foreach (JProperty pro in jsonTokenNew)
                                    {
                                        caracteristicaDinamica.NomeCampo = pro.Path;
                                        caracteristicaDinamica.ValorCampo = pro.Value.ToString();
                                        caracteristicasDinamicas.Add(caracteristicaDinamica);
                                        Console.WriteLine(pro.Path);
                                    }
                                }
                            }
                            else
                            {
                                caracteristicaDinamica.NomeCampo = jsonToken.Path;
                                caracteristicaDinamica.ValorCampo = jsonToken.Value.ToString();
                                caracteristicasDinamicas.Add(caracteristicaDinamica);
                                Console.WriteLine(jsonToken.Path);
                            }

                        }
                    }
                    foreach (JProperty jsonToken in jsonThirdToken.Children().Children().Children())
                    {
                        if (jsonToken.Path.Contains("Venda"))
                        {
                            if (jsonToken.Path.Contains("Itens"))
                            {
                                foreach (var jsonTokenVenda in jsonToken)
                                {
                                    foreach (var jsonTokenItens in jsonTokenVenda)
                                    {
                                        foreach (JProperty jsonTokenIten in jsonTokenItens)
                                        {
                                            caracteristicaDinamica.NomeCampo = jsonTokenIten.Path;
                                            caracteristicaDinamica.ValorCampo = jsonTokenIten.Value.ToString();
                                            caracteristicasDinamicas.Add(caracteristicaDinamica);
                                            Console.WriteLine(jsonTokenIten.Path);

                                        }
                                    }
                                }
                            }

                            else
                            {
                                caracteristicaDinamica.NomeCampo = jsonToken.Path;
                                caracteristicaDinamica.ValorCampo = jsonToken.Value.ToString();
                                caracteristicasDinamicas.Add(caracteristicaDinamica);
                                Console.WriteLine(jsonToken.Path);
                            }

                        }
                        if (jsonToken.Path.Contains("EnderecoEntrega"))
                        {
                            caracteristicaDinamica.NomeCampo = jsonToken.Path;
                            caracteristicaDinamica.ValorCampo = jsonToken.Value.ToString();
                            caracteristicasDinamicas.Add(caracteristicaDinamica);
                            Console.WriteLine(jsonToken.Path);


                        }


                    }
                }
            }
            var jsonCaracteristica = JsonConvert.SerializeObject(caracteristicasDinamicas);

            Console.ReadKey();    
          
        }
    }
}