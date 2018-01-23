using System.Collections.Generic;
using InquirerCS;

namespace ConsoleApp1
{
    public class ClientAPI
    {
        public string Id { get; set; }

        public bool IsActive { get; set; }

        public string AllowedOrigin { get; set; }

        public ApplicationType ApplicationType { get; set; }

        public string Name { get; set; }

        public int RefreshTokenLifeTime { get; set; }

        public string Secret { get; set; }
    }

    public enum ApplicationType
    {
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            SetClientActiveStatus();
        }

        public static void XXXX(string name)
        {
            string herp = string.Empty;
            Question.Prompt(Question.Input(name)).Then(ref herp);
        }

        private static void SetClientActiveStatus()
        {
            var clients = new List<ClientAPI>()
            {
                new ClientAPI() { Name = "Asdasd" } ,
                new ClientAPI() {  Name = "Asdasdasdasasdasdadasda" }
            };

            string herp = string.Empty;

            Question.Prompt(Question.Menu("ASdasda").AddOption("asdasda", () => { XXXX("1"); }));

            XXXX("1");
            Question.Prompt(Question.Input("2")).Then(x =>
            {
                Question.Prompt(Question.Input("2.1")).Then(answer =>
                {
                    XXXX("2.1.1");
                    XXXX("2.1.2");
                    XXXX("2.1.3");
                });
                Question.Prompt(Question.Input("2.2")).Then(answer =>
                {
                    XXXX("2.2.1");
                    XXXX("2.2.2");
                    XXXX("2.2.3");
                });
            });
            XXXX("3");
            XXXX("4");
            XXXX("5");
            XXXX("6");

            //Question.Prompt(() =>
            //{
            //    return Question.Input("Sadasd");
            //});

            //Question.Checkbox("Chose record to deactivate", clients)
            //.Page(10)
            //.WithDefaultValue(item => { return clients.Where(c => c.IsActive).Any(c => c.Id == item.Id); })
            //.WithConfirmation()
            //.WithConvertToString(item => { return $"{item.Name}"; }).Then(answer =>
            //{
            //    var toDiactivate = clients.Where(c => c.IsActive).Where(c => !answer.Any(a => a.Id == c.Id)).ToList();
            //    toDiactivate.ForEach(item =>
            //    {
            //        item.IsActive = false;
            //    });

            //    var toActivate = clients.Where(c => !c.IsActive).Where(c => answer.Any(a => a.Id == c.Id)).ToList();
            //    toActivate.ForEach(item =>
            //    {
            //        item.IsActive = true;
            //    });
            //});
        }

        private static void CreateNewClient()
        {
            var client = new ClientAPI();

            //Question.Ask()
            //.Then(() => { Question.Input("Id").Then(answer => client.Id = answer); })
            //.Then(() => { Question.Input("Name").WithDefaultValue(client.Id).Then(answer => client.Name = answer); })
            //.Then(() => { Question.Input("Allowed Origin").WithDefaultValue("*").Then(answer => client.AllowedOrigin = answer); })
            //.Then(() => { Question.Input<int>("Refresh Token Lifetime (hours)").WithValidation(answer => answer > 0, "answer > 0").Then(answer => client.RefreshTokenLifeTime = answer * 60); })
            //.Then(() => { Question.Confirm("Is Active").Then(answer => client.IsActive = answer); })
            //.Then(() =>
            //{
            //})
            //.Go();
        }
    }
}