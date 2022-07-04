
### Get Project Properties version number : 
 
 ```C#
  System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
```

### Get Key Value data from webconfig -appsetting
```C#
using System.Configuration;
var data = ConfigurationManager.AppSettings["MyKey"];
```
