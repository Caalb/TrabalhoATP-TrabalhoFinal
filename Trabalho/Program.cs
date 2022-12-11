using System;
using System.Collections.Generic;
using System.IO;

namespace Trabalho
{
    class Program
    {
        static StreamReader arq_entrada = new StreamReader("entrada.txt");
        static StreamWriter arq_saida = new StreamWriter("saida.txt");
        
        static void Main(string[] args)
        {
            //VARIAVEIS
            double Saldo = 0;
            int Menu = 0;
            int ContaTempo = 0;
            double ChequeEspecialInicial = 0;
            double ChequeEspecialAtual;

            //INICIALIZAÇÃO DE CONTA
            ChequeEspecialInicial = double.Parse(arq_entrada.ReadLine());
            ChequeEspecialAtual = ChequeEspecialInicial;

            //SWITCH
            while (Menu != 6)
            {
                if (ContaTempo % 5 == 0 && Saldo < 0)
                {
                    double percentualJuros = double.Parse(arq_entrada.ReadLine());
                    double saldoDevedor = Saldo * (percentualJuros / 100);
                    Saldo += saldoDevedor;
                    
                    arq_saida.WriteLine($"TAXA DE {percentualJuros} APLICADA NO SEU SALDO NEGATIVO. NOVO SALDO {Saldo}");
                }
                {
                    
                }
                Menu = int.Parse(arq_entrada.ReadLine());
                switch (Menu)
                {
                    case 1:
                        ConsultarSaldo(Saldo);
                        ContaTempo++;
                        break;

                    case 2:
                        Tuple<double,double> ValorSacado = RealizarSaque(Saldo, ChequeEspecialAtual);
                        Saldo = ValorSacado.Item1;
                        ChequeEspecialAtual = ValorSacado.Item2;
                        ContaTempo++;
                        break;

                    case 3:
                        double ValorDepositado = RealizarDeposito(Saldo);
                        Saldo = ValorDepositado;
                        ContaTempo++;
                        break;

                    case 4:
                        Tuple<double, double> ContaPaga = PagarConta(Saldo, ChequeEspecialAtual);
                        Saldo = ContaPaga.Item1;
                        ChequeEspecialAtual = ContaPaga.Item2;
                        ContaTempo++;
                        break;

                    case 5:
                        
                        break;
                    case 6:
                        arq_saida.WriteLine("PROGRAMA ENCERRADO.");
                        break;
                    default:
                        arq_saida.WriteLine("VALOR DE MENU INFORMADO INCORRETO");
                        break;  
                }
            }
            
            List<double> extratoBancoSaque = new List<double>();
            List<double> extratoBancoDeposito = new List<double>();
            List<double> extratoBancoPagarConta = new List<double>();
            
            arq_entrada.Close();
            arq_saida.Close();
        }

        public static List<double> AdicionaExtratoBancarioSaque(double lançamento)
        {
            List<double> extrato = new List<double>();
            if (extrato.Count == 10)
            {
                extrato.RemoveAt(extrato.Count - 1);
                extrato.Insert(0, lançamento);
            } else extrato.Add(lançamento);

            return extrato;
        }
        
        public static List<double> AdicionaExtratoBancarioDeposito(double lançamento)
        {
            List<double> extrato = new List<double>();
            if (extrato.Count == 10)
            {
                extrato.RemoveAt(extrato.Count - 1);
                extrato.Insert(0, lançamento);
            } else extrato.Add(lançamento);

            return extrato;
        }
        
        public static List<double> AdicionaExtratoBancarioPagarConta(double lançamento)
        {
            List<double> extrato = new List<double>();
            if (extrato.Count == 10)
            {
                extrato.RemoveAt(extrato.Count - 1);
                extrato.Insert(0, lançamento);
            } else extrato.Add(lançamento);

            return extrato;
        }

        public static void ConsultarSaldo(double Saldo) // PROCEDIMENTO DE MOSTRAR O SALDO
        {

            arq_saida.WriteLine($"SALDO DE {Saldo}");
        }

        public static Tuple<double, double> RealizarSaque(double Saldo, double ChequeEspecialAtual) // FUNÇÃO PARA SAQUE
        {
            // INFORMANDO O VALOR DE SAQUE
            double.TryParse(arq_entrada.ReadLine(), out double ValorSacado);

            // VALORES SACADOS FINAIS
            double SaldoSacado = 0;
            double ChequeEspecialSacado = 0;


            // CONDIÇÕES PARA O VALOR SACADO:
            if (ValorSacado > Saldo + ChequeEspecialAtual) // MAIOR QUE A SOMA DO SALDO E O CHEQUE ESPECIAL
            {
                arq_saida.WriteLine("IMPOSSIVEL DE REALIZAR O SAQUE");
                SaldoSacado = Saldo;
                ChequeEspecialSacado = ChequeEspecialAtual;

            }
            else if ((ValorSacado > Saldo) && (ValorSacado < ChequeEspecialAtual)) // MAIOR QUE O SALDO E MENOR QUE O CHEQUE ESPECIAL
            {
                arq_saida.WriteLine($"UTILIZOU {ValorSacado} DO CHEQUE ESPECIAL PARA SACAR");
                ChequeEspecialSacado = ChequeEspecialAtual - (ValorSacado - Saldo);

            }
            else // MENOR QUE O SALDO
            {
                SaldoSacado = Saldo - ValorSacado;
                arq_saida.WriteLine("SACOU VALOR DE: " + SaldoSacado);
                ChequeEspecialSacado = ChequeEspecialAtual;
            }
            // ATRIBUIÇÕES DOS VALORES DENTRO DO VETOR

            return Tuple.Create(SaldoSacado, ChequeEspecialSacado);
        }

        public static double RealizarDeposito(double Saldo) // FUNÇÃO DE DEPOSITO
        {
            double.TryParse(arq_entrada.ReadLine(), out double ValorDeposito);
            double SaldoComDeposito = 0;
            
            SaldoComDeposito = Saldo + ValorDeposito;

            return SaldoComDeposito; 
        }

        public static Tuple<double, double> PagarConta(double Saldo, double ChequeEspecialAtual)
        {
            double.TryParse(arq_entrada.ReadLine(), out double ValorConta);

            // VALORES PAGOS FINAIS
            double SaldoPago = 0;
            double ChequeEspecialPago = 0;


            // CONDIÇÕES PARA O PAGAMENTO:
            if (ValorConta > Saldo + ChequeEspecialAtual) // MAIOR QUE A SOMA DO SALDO E O CHEQUE ESPECIAL
            {
                arq_saida.WriteLine("IMPOSSIVEL DE REALIZAR O PAGAMENTO");
                ChequeEspecialPago = ChequeEspecialAtual;
                SaldoPago = Saldo;
            }
            else if ((ValorConta > Saldo) && (ValorConta < ChequeEspecialAtual)) // MAIOR QUE O SALDO E MENOR QUE O CHEQUE ESPECIAL
            {
                ChequeEspecialPago = ChequeEspecialAtual - (ValorConta - Saldo);
                arq_saida.WriteLine($"PAGOU CONTA NO VALOR DE {ValorConta} UTILIZANDO CHEQUE ESPECIAL");
            }
            else // MENOR QUE O SALDO
            {
                SaldoPago = Saldo - ValorConta;
                arq_saida.WriteLine($"PAGOU CONTA NO VALOR DE {ValorConta}");
                ChequeEspecialPago = ChequeEspecialAtual;
            }

            return Tuple.Create(SaldoPago, ChequeEspecialPago);
        } // FUNÇÃO PARA PAGAR CONTA
    }
}
