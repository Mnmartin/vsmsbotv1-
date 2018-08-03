using System;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System.Collections.Generic;

namespace vsmsbotv1.Dialogs
{
    [Serializable]
    public class RootDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            //Show the title with background image and Details
            var message = context.MakeMessage();
            var attachment = GetHeroCard();
            message.Attachments.Add(attachment);
            await context.PostAsync(message);

            // Show the list of plan
            context.Wait(MessageReceivedAsync);
            //  return Task.CompletedTask;
        }

        private  async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;
            await context.PostAsync(" May I help you Request a meeting");
            context.Call(new AppointmentDialog(),this.AppointmentDialogresumeafter);
        }
        private async Task AppointmentDialogresumeafter(IDialogContext context, IAwaitable<object> result)
        {
            context.Wait(MessageReceivedAsync);

        }

        /// <summary>
        /// Design Title with Image and About US link
        /// </summary>
        /// <returns></returns>
        private static Attachment GetHeroCard()
        {
            var heroCard = new HeroCard
            {
                Title = "Visitor Security Management Service ",
                Subtitle = "Request Meeting",
                Text = "VSMS simplifies the Visitor Registration process by eliminating the mundane, unsecure aspects of how things are done today by incorporating the use of technology in efficient ways that enforces security, better communication, resulting in pleasant visitor experiences.",
                Images = new List<CardImage> { new CardImage("https://sandbox.vsms.tezzasolutions.com/index.PNG") },
                Buttons = new List<CardAction> { new CardAction(ActionTypes.OpenUrl, "Request meeting", value: "https://sandbox.vsms.tezzasolutions.com/") }
            };

            return heroCard.ToAttachment();
        }

        public virtual async Task ChildDialogComplete(IDialogContext context, IAwaitable<object> response)
        {
            await context.PostAsync("Thanks for select C# Corner bot for Annual Conference 2018 Registrion .");
            context.Done(this);
        }

       
    }
}