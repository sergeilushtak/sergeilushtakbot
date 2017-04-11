using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace sergeilushtakbot.banking
{
    public class banking
    {
        private static double _balance;
        private const double buck2quid = 0.6;
        
        
        public static string withdraw (string amount, string currency )
        {
            string responce;
            double amt;

            if (!Double.TryParse(amount, out amt))
            {
                responce = $"Please use digits to specify your amount. My banker can't understand \"{amount}\".";
            }
            else
            {
                if (currency == "pounds")
                    amt /= buck2quid;

                if (amt > _balance)
                    responce = $"Cannot withdraw {amount} {currency}. You don't have this much.";
                else if (amt <= 0)
                    responce = $"{amount} is an invalid ammount. No can do.";
                else
                {
                    _balance -= amt;
                    responce = $"Please collect your {amount} {currency}.";
                }
            }

            return responce;
        }

        public static string deposit (string amount, string currency)
        {
            string responce;
            double amt;
            
            if (!Double.TryParse(amount, out amt))
            {
                responce = $"Please use digits to specify your amount. My banker can't understand \"{amount}\".";
            }
            else
            {
                if (currency == "pounds")
                    amt /= buck2quid;

                if (amt < 0)
                    responce = $"{amount} is an invalid ammount. No can do.";
                else
                {
                    _balance += amt;
                    responce = $"{amount} {currency} deposited successfully.";
                }
            }        
            return responce;
        }

        public static string getBalance (string currency)
        {

            string responce;

            double balance_out = _balance;

            if (currency == "pounds")
                balance_out *= buck2quid;

            responce = $"Your current balance is {balance_out} {currency}.";

            return responce;
        }
 
    }
}