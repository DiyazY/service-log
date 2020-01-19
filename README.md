# service-log
Logging as a service. It's a very simple logging service that saves only logs and gives an ability to scan them.
The service is following DomainDrivenDesign, CQRS, Repository patterns. It looks like overengineering, but I do believe that a project will grow and these things will be necessary.

## Using
For inspecting logs use Web UI, there is also searching form. 
Logs can be saved in several ways:

### Database
Certain workers (scrapers, consoles, whatever applications) can save logs directly to DB via sl.infrastructure assembly. The assembly has necessary validation logic to protect from invalid data.

### WebApi
Certain workers (scrapers, consoles, whatever applications) can send logs to the WebApi endpoint. There is a Swagger Api documentation, where models and methods are well described.

![image](https://user-images.githubusercontent.com/16912141/72686949-8082f800-3b0a-11ea-8e66-b9c1ea14c701.png)

## Examples

### Web UI
![image](https://user-images.githubusercontent.com/16912141/72686967-b7f1a480-3b0a-11ea-82be-b14d6331aec1.png)

![image](https://user-images.githubusercontent.com/16912141/72686995-ee2f2400-3b0a-11ea-90a2-e8cdde1f9080.png)

### Error on client side
![image](https://user-images.githubusercontent.com/16912141/72687078-b1176180-3b0b-11ea-84f8-a690ab576dfb.png)

## License

service-log is [MIT licensed](./LICENSE).