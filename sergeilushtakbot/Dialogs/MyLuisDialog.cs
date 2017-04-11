using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;

namespace sergeilushtakbot.Dialogs
{
    
//    [LuisModel("6d0f4173-266f-46e5-a167-a9b3cebaba18", "b8b5dd5244b94f1fa0c30c637e5a68f3")]
    [LuisModel("45bbe791-fa6d-4390-a115-b3f2dba9c9e2", "b8b5dd5244b94f1fa0c30c637e5a68f3")]
    [Serializable]

    public class MyLuisDialog : LuisDialog<object>
    {
        public const string Entity_Number = "builtin.number";
        public const string Entity_Pounds = "pounds";


        [LuisIntent("")]
        public async Task HandleNone (IDialogContext context, LuisResult result)
        {
            await context.PostAsync("Sorry, didn't quite catch that.");

            context.Wait(MessageReceived);
        }

        [LuisIntent("balance")]
        public async Task GiveBalance (IDialogContext context, LuisResult result)
        {
            string currency = getCurrency (result);
          
            string reply = banking.banking.getBalance(currency);
            await context.PostAsync(reply);

            context.Wait(MessageReceived);
        }

        [LuisIntent("withdraw")]
        public async Task Withdraw (IDialogContext context, LuisResult result)
        {
            string amount;
            string reply;

            if (!getAmount(result, out amount))
            {
                reply = "No amount specified for withdrawal.";
            }
            else
            {
                string currency = getCurrency(result);

                reply = banking.banking.withdraw (amount, currency);
            }

            await context.PostAsync(reply);

            context.Wait(MessageReceived);
        }

        [LuisIntent("deposit")]
        public async Task Deposit (IDialogContext context, LuisResult result)
        {
            string amount;
            string reply;

            if (!getAmount (result, out amount))
            {
                reply = "No amount specified for deposit.";
            }
            else
            {
                string currency = getCurrency (result);

                reply = banking.banking.deposit(amount, currency);
            }
 
            await context.PostAsync(reply);

            context.Wait(MessageReceived);
        }

        private string getCurrency (LuisResult result)
        {
            string currency;

            EntityRecommendation quid; 

            if (result.TryFindEntity(Entity_Pounds, out quid))
                currency = "pounds";
            else
                currency = "dollars";

            return currency;
        }

        private bool getAmount(LuisResult result, out string amount)
        {
            EntityRecommendation entity_amount;
            amount = String.Empty;

            if (!result.TryFindEntity(Entity_Number, out entity_amount))
                return false;
            else
            {
                amount = entity_amount.Entity;
                return true;
            }
        }

     
    }

}