
### Get Project Properties version number : 
 
 ```C#
  System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
```

### Get Key Value data from webconfig -appsetting
```C#
using System.Configuration;
var data = ConfigurationManager.AppSettings["MyKey"];
```

### Server problem 
```C#
System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
```
