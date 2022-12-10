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
                        Tuple<double, double> ValorDepositado = RealizarDeposito(Saldo, ChequeEspecialInicial, ChequeEspecialAtual);
                        Saldo = ValorDepositado.Item1;
                        ChequeEspecialAtual = ValorDepositado.Item2;
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
                        break;
                    default:
                        Console.WriteLine("VALOR DE MENU INFORMADO INCORRETO");
                        break;  
                }
            }
            arq_entrada.Close();
            arq_saida.Close();
        }

        public static List<double> ExtratoBancario(double lançamento)
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

            Console.WriteLine("SALDO DE: " + Saldo);
            arq_saida.WriteLine($"SALDO DE {Saldo}");
        }

        public static Tuple<double, double> RealizarSaque(double Saldo, double ChequeEspecialAtual) // FUNÇÃO PARA SAQUE
        {
            // INFORMANDO O VALOR DE SAQUE
            double.TryParse(arq_entrada.ReadLine(), out double ValorSacado);
            Console.WriteLine("VALOR DO SAQUE: " + ValorSacado);

            // VALORES SACADOS FINAIS
            double SaldoSacado = 0;
            double ChequeEspecialSacado = 0;


            // CONDIÇÕES PARA O VALOR SACADO:
            if (ValorSacado > Saldo + ChequeEspecialAtual) // MAIOR QUE A SOMA DO SALDO E O CHEQUE ESPECIAL
            {
                Console.WriteLine("IMPOSSIVEL DE REALIZAR O SAQUE");
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

        public static Tuple<double, double> RealizarDeposito(double Saldo, double ChequeEspecialInicial, double ChequeEspecialAtual) // FUNÇÃO DE DEPOSITO
        {
            double.TryParse(arq_entrada.ReadLine(), out double ValorDeposito);
            Console.WriteLine("VALOR DO DEPOSITO: " + ValorDeposito);

            double SaldoComDeposito = 0;
            double UsoDoCheque; // VALOR PRA SACAR

            if (ChequeEspecialAtual != ChequeEspecialInicial)
            {
                UsoDoCheque = ChequeEspecialInicial - ChequeEspecialAtual;
                ValorDeposito = ValorDeposito - UsoDoCheque;

                if (ValorDeposito > 0)
                {
                    SaldoComDeposito = Saldo + ValorDeposito;
                    ChequeEspecialAtual = ChequeEspecialInicial;
                }
            }
            else
            {
                SaldoComDeposito = Saldo + ValorDeposito;
                arq_saida.WriteLine($"DEPOSITOU {SaldoComDeposito}");
            }

            return Tuple.Create(SaldoComDeposito, ChequeEspecialAtual);
        }

        public static Tuple<double, double> PagarConta(double Saldo, double ChequeEspecialAtual)
        {
            double.TryParse(arq_entrada.ReadLine(), out double ValorPago);
            Console.WriteLine("VALOR DA CONTA PAGA: " + ValorPago);

            // VALORES PAGOS FINAIS
            double SaldoPago = 0;
            double ChequeEspecialPago = 0;


            // CONDIÇÕES PARA O PAGAMENTO:
            if (ValorPago > Saldo + ChequeEspecialAtual) // MAIOR QUE A SOMA DO SALDO E O CHEQUE ESPECIAL
            {
                Console.WriteLine("IMPOSSIVEL DE REALIZAR O PAGAMENTO");
                ChequeEspecialPago = ChequeEspecialAtual;
                SaldoPago = Saldo;
            }
            else if ((ValorPago > Saldo) && (ValorPago < ChequeEspecialAtual)) // MAIOR QUE O SALDO E MENOR QUE O CHEQUE ESPECIAL
            {
                ChequeEspecialPago = ChequeEspecialAtual - (ValorPago - Saldo);
                arq_saida.WriteLine($"PAGOU CONTA NO VALOR DE {ValorPago} UTILIZANDO CHEQUE ESPECIAL");
            }
            else // MENOR QUE O SALDO
            {
                SaldoPago = Saldo - ValorPago;
                arq_saida.WriteLine($"PAGOU CONTA NO VALOR DE {ValorPago}");
                ChequeEspecialPago = ChequeEspecialAtual;
            }

            return Tuple.Create<double, double>(SaldoPago, ChequeEspecialPago);
        } // FUNÇÃO PARA PAGAR CONTA
    }
}
