
### Get Project Properties version number : 
 
 ```C#
  System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
```

### Get Key Value data from webconfig -appsetting
```C#
using System.Configuration;
var data = ConfigurationManager.AppSettings["MyKey"];

> Asp.Net Core
var ConnectionStrings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("ConnectionStrings")["DBConnection"];
```

### Server problem 
```C#
System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
```

### Crystal Report Limit 
```cmd
https://stackoverflow.com/questions/32438110/crystal-reports-maximum-report-processing-jobs-limit-reached
  run -> regedit 
  goto this address -> Computer\HKEY_LOCAL_MACHINE\SOFTWARE\SAP BusinessObjects\Crystal Reports for .NET Framework 4.0\Report Application Server\InprocServer
  set value 1000 in PrintJobLimit
```

### HttpClient Api Call 
```c#
     public OrderResponse GetCapillaryOrderInfoV2(string orderId)
        {
            OrderResponse resobj = new OrderResponse();
            string apiMethod = "Order/V2/" + merchantId + "/" + orderId + "";
            string apiUrl = APISendUrl + apiMethod;

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/x-www-form-urlencoded");
                client.DefaultRequestHeaders.TryAddWithoutValidation("PublicKey", PublicKey);
                client.DefaultRequestHeaders.TryAddWithoutValidation("AuthToken", AuthorizationToken);
                client.DefaultRequestHeaders.TryAddWithoutValidation("MerchantId", merchantId);
                using (var response = client.GetAsync(apiUrl))
                {
                    if (response.Result.IsSuccessStatusCode)
                    {
                        string jsonString = response.Result.Content.ReadAsStringAsync().Result;
                        resobj = JsonConvert.DeserializeObject<OrderResponse>(jsonString);
                        return resobj;
                    }
                    else
                    {
                        throw new Exception(response.Result.Content.ReadAsStringAsync().Result);
                    }
                }

            }
        }
```

### Js Time submit in C# get DateTime
```C#
       DateTime fdate = DateTime.ParseExact(sdate + " " + stime, "dd-MMM-yyyy HHmmss", CultureInfo.InvariantCulture);
       DateTime tdate = DateTime.ParseExact(edate + " " + etime, "dd-MMM-yyyy HHmmss", CultureInfo.InvariantCulture);

       function getTimeString(time) {
            return time.getHours().toString().padStart(2, '0') + "" + time.getMinutes().toString().padStart(2, '0') + "" + time.getSeconds().toString().padStart(2, '0');
        }
```

