using System;

namespace Trabalho
{
    class Program
    {
        static void Main(string[] args)
        {
            //VARIAVEIS
            double Saldo = 0;
            int Menu = 6;
            int ContaTempo = 0;
            double ChequeEspecialInicial = 0;
            double ChequeEspecialAtual;

            //INICIALIZAÇÃO DE CONTA
            ChequeEspecialInicial = double.Parse(Console.ReadLine());
            ChequeEspecialAtual = ChequeEspecialInicial;

            //SWITCH
            switch (Menu)
            {
                case 1:
                    ConsultarSaldo(Saldo);
                    ContaTempo++;
                    break;

                case 2:
                    double[] ValorSacado = RealizarSaque(Saldo, ChequeEspecialAtual);
                    Saldo = ValorSacado[0];
                    ChequeEspecialAtual = ValorSacado[1];
                    ContaTempo++;
                    break;

                case 3:
                    double[] ValorDepositado = RealizarDeposito(Saldo, ChequeEspecialInicial, ChequeEspecialAtual);
                    Saldo = ValorDepositado[0];
                    ChequeEspecialAtual = ValorDepositado[1];
                    ContaTempo++;
                    break;

                case 4:
                    double[] ContaPaga = PagarConta(Saldo, ChequeEspecialAtual);
                    Saldo = ContaPaga[0];
                    ChequeEspecialAtual = ContaPaga[1];
                    ContaTempo++;
                    break;

                case 5:

                    break;
                case 6:

                    break;

            }

        }
        public static void ConsultarSaldo(double Saldo) // PROCEDIMENTO DE MOSTRAR O SALDO
        {

            Console.WriteLine(Saldo);
        }

        public static double[] RealizarSaque(double Saldo, double ChequeEspecialAtual) // FUNÇÃO PARA SAQUE
        {
            // INFORMANDO O VALOR DE SAQUE
            Console.WriteLine("VALOR DO SAQUE: ");
            double.TryParse(Console.ReadLine(), out double ValorSacado);

            // VALORES SACADOS FINAIS
            double SaldoSacado = 0;
            double ChequeEspecialSacado = 0;

            //VETOR DE RETORNO
            double[] SaldoEChequeEspecial = new double[1];


            // CONDIÇÕES PARA O VALOR SACADO:
            if (ValorSacado > Saldo + ChequeEspecialAtual) // MAIOR QUE A SOMA DO SALDO E O CHEQUE ESPECIAL
            {
                Console.WriteLine("IMPOSSIVEL DE REALIZAR O SAQUE");
                SaldoSacado = Saldo;
                ChequeEspecialSacado = ChequeEspecialAtual;

            }
            else if ((ValorSacado > Saldo) && (ValorSacado < ChequeEspecialAtual)) // MAIOR QUE O SALDO E MENOR QUE O CHEQUE ESPECIAL
            {

                ChequeEspecialSacado = ChequeEspecialAtual - (ValorSacado - Saldo);

            }
            else // MENOR QUE O SALDO
            {
                SaldoSacado = Saldo - ValorSacado;
                ChequeEspecialSacado = ChequeEspecialAtual;
            }
            // ATRIBUIÇÕES DOS VALORES DENTRO DO VETOR
            SaldoEChequeEspecial[0] = SaldoSacado;
            SaldoEChequeEspecial[1] = ChequeEspecialSacado;

            return SaldoEChequeEspecial;
        }

        public static double[] RealizarDeposito(double Saldo, double ChequeEspecialInicial, double ChequeEspecialAtual) // FUNÇÃO DE DEPOSITO
        {
            Console.WriteLine("VALOR DO DEPOSITO: ");
            double.TryParse(Console.ReadLine(), out double ValorDeposito);

            // VETOR DE RETORNO
            double[] SaldoEChequeEspecial = new double[1];
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
            }

            SaldoEChequeEspecial[0] = SaldoComDeposito;
            SaldoEChequeEspecial[1] = ChequeEspecialAtual;


            return SaldoEChequeEspecial;
        }

        public static double[] PagarConta(double Saldo, double ChequeEspecialAtual)
        {
            Console.WriteLine("VALOR DA CONTA PAGA: ");
            double.TryParse(Console.ReadLine(), out double ValorPago);

            // VALORES PAGOS FINAIS
            double SaldoPago = 0;
            double ChequeEspecialPago = 0;

            //VETOR DE RETORNO
            double[] SaldoEChequeEspecial = new double[1];


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

            }
            else // MENOR QUE O SALDO
            {
                SaldoPago = Saldo - ValorPago;
                ChequeEspecialPago = ChequeEspecialAtual;
            }
            // ATRIBUIÇÕES DOS VALORES DENTRO DO VETOR
            SaldoEChequeEspecial[0] = SaldoPago;
            SaldoEChequeEspecial[1] = ChequeEspecialPago;

            return SaldoEChequeEspecial;




        } // FUNÇÃO PARA PAGAR CONTA
    }

}
}