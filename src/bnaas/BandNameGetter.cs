using System;
using System.Security.Cryptography.X509Certificates;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using System.Collections.Generic;

namespace bnaas
{
    public class BandNameGetter
    {
        private static readonly string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
        private static string ApplicationName = "bnaas";

        public List<string> Get()
        {
            var result = new List<string>();

            // https://developers.google.com/api-client-library/dotnet/guide/aaa_oauth#service-account
            var serviceAccountEmail = "googlesheetsapiaccess@bananas-224205.iam.gserviceaccount.com";
            var certificate = new X509Certificate2(@"key.p12", "notasecret", X509KeyStorageFlags.Exportable);

            var credential = new ServiceAccountCredential(
               new ServiceAccountCredential.Initializer(serviceAccountEmail)
               {
                    Scopes = Scopes
               }.FromCertificate(certificate));

            var service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            var spreadsheetId = "1spb2eP83629c8H8TxTCsZ3cYKh8882HhkIbmZ1feWgQ";
            var range = "Sheet1!B2:B1000";
            var request = service.Spreadsheets.Values.Get(spreadsheetId, range);

            var response = request.Execute();
            var values = response.Values;

            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    result.Add(row[0].ToString());
                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }

            return result;
        }
    }
}
