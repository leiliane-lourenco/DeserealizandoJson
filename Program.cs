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

            #region MyRegion

            //var dadosVenda = JsonConvert.DeserializeObject<DadosVenda>(json);

            //var itemVenda1 = new VendaItem();
            //itemVenda1.Id = 1;
            //itemVenda1.Valor = 10;

            //var itemVenda2 = new VendaItem();
            //itemVenda2.Id = 2;
            //itemVenda2.Valor = 20; //40

            //var itemVenda3 = new VendaItem();
            //itemVenda3.Id = 3;
            //itemVenda3.Valor = 30;

            //var venda = new Venda();
            //venda.Itens.Add(itemVenda1);
            //venda.Itens.Add(itemVenda2);
            //venda.Itens.Add(itemVenda3);

            //var pedido = new Pedido();
            //pedido.Venda = venda;

            //foreach (var item in pedido.Venda.Itens)
            //{
            //    if (item.Valor == 20)
            //    {
            //        item.Valor = 40;
            //    }
            //    Console.WriteLine(item.Valor);
            //}


            // Criar listas de palavras
            // Adicionar palavras
            // Removam a primeira palavra
            // Removam a ultima palavra
            // Todas as palvras que comecem com a letra B
            // Todas as palvras que terminem com a letra A

            //List<string> listaDeDoces = new List<string>();
            //listaDeDoces.Add("Brigadeiro");
            //listaDeDoces.Add("Bejinho");
            //listaDeDoces.Add("Doce de leite");
            //listaDeDoces.Add("Paçoca");
            //listaDeDoces.Add("Bombom");
            //listaDeDoces.Add("Carolina");
            //listaDeDoces.Add("Pé de Moleque");

            //listaDeDoces.RemoveAt(0);

            //listaDeDoces.RemoveAt(listaDeDoces.Count - 1);

            //List<string> listaPalavrasInicalB = new List<string>();

            //foreach (string doces in listaDeDoces)
            //{
            //    if (doces.StartsWith('B'))
            //        listaPalavrasInicalB.Add(doces);
            //}

            //foreach (string palavraInicialB in listaPalavrasInicalB)
            //{
            //    listaDeDoces.Remove(palavraInicialB);
            //}

            //List<string> listaPalavrasFinalA = new List<string>();
            //foreach (string doces in listaDeDoces)
            //{
            //    if (doces.EndsWith('a') || doces.EndsWith('á'))
            //        listaPalavrasFinalA.Add(doces);
            //}

            //foreach (string palavraFinalA in listaPalavrasFinalA)
            //{
            //    listaDeDoces.Remove(palavraFinalA);
            //} 

            //Console.WriteLine("===========Lista atualizada=======");

            //foreach (string doces in listaDeDoces)
            //{
            //    Console.WriteLine(doces);  
            //}

            #endregion
        }
    }
}