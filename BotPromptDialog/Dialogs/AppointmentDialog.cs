using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace vsmsbotv1.Dialogs
{
    [Serializable]
    public class AppointmentDialog : IDialog<object>
    {
        string name;
        string email;
        string phone;
        string company;
        string plandetails;
        
        public AppointmentDialog(string plan)
        {
            plandetails = plan;
        }

        public AppointmentDialog()
        {
        }

        public async Task StartAsync(IDialogContext context)
        {
            context.Wait(MessageReceivedAsync);
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> activity)
        {
            var response = await activity;
            if (response.Text.ToLower().Contains("yes"))
            {
                PromptDialog.Text(
                    context: context,
                    resume: ResumeGetName,
                    prompt: "Please share your good name",
                    retry: "Sorry, I didn't understand that. Please try again."
                );
            }
            else
            {
                context.Done(this);
            }
            
        }

        public virtual async Task ResumeGetName(IDialogContext context, IAwaitable<string> Username)
        {
            string response = await Username;
            name = response; ;

            PromptDialog.Text(
                context: context,
                resume: ResumeGetEmail,
                prompt: "Please share your Email ID",
                retry: "Sorry, I didn't understand that. Please try again."
            );
        }

        public virtual async Task ResumeGetEmail(IDialogContext context, IAwaitable<string> UserEmail)
        {
            string response = await UserEmail;
            email = response; ;

            PromptDialog.Text(
                context: context,
                resume: ResumeGetPhone,
                prompt: "Please share your Mobile Number",
                retry: "Sorry, I didn't understand that. Please try again."
            );
        }
      

        public virtual async Task ResumeGetPhone(IDialogContext context, IAwaitable<string> mobile)
        {
            string response = await mobile;
            phone = response;

            await context.PostAsync(String.Format("Hello {0} ,Congratulation :) your request has been sent Successfullly completed with Name = {0} Email = {1} Mobile Number = {2} . You will get Confirmation email and SMS", name, email, phone, company));

            context.Done(this);
        }
    }
}