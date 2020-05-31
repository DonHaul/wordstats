# wordstats

.Net Core 3.1 Application that implements simple word stats API . 

## How to Start
1) Make sure There is a database connection. The project uses a localDB (see appsettings.json) but it can be connected to a remote database if needed. This can be done on VS19 through the SQL Server Explorer Window by, doing ```Add Database...``` in case its missing. The name must be ```WordsDB```
2) Run the ```InitializeDB.sql``` in order to create the necessary tables and entries. (This can be done through Visual Studio 2019)
3) To build and run open VS19 and make sure to use the PribV2 launchsettings profile to start, which will run it on ```http://localhost:8080```

Alternative, if there's no need for a persistent db, on the ```Startup.cs```, change the ConfigureServices() function for the following:
```
public void ConfigureServices(IServiceCollection services)
        {
            //Add context and use sqlserver
            services.AddDbContext<DocWordContext>(opt =>opt.UseInMemoryDatabase("DocWords"));

            services.AddControllers();
        }
```

## How to access
For Simplicity sake, ```http``` was used instead of the now standard ```https``` where SSL certificate is used.

Because of that, a normal browser can't acess the endpoints. So:
1) Install Postman or similar
2) Disable SSL by doing:  ```File```> ```Settings```> ```General```>```SSSL certificate verification```> OFF
3) (Optional) Import the ```PribDocs.postman_collection.json``` file to postman.
4) Use the api  the endpoints in [here](https://documenter.getpostman.com/view/3388796/SztEYS7B)

